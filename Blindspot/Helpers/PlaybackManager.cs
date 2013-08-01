using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using System.IO;
using System.Threading;
using libspotifydotnet;
using Blindspot.Controllers;

namespace Blindspot.Helpers
{
    public class PlaybackManager : IDisposable
    {
        private enum StreamingPlaybackState
        {
            Stopped,
            Playing,
            Paused,
            Buffering
        }

        private ByteGateKeeper gatekeeper;
        const int secondsToBuffer = 3;
        private volatile float volume = 1f;
        private volatile StreamingPlaybackState playbackState;
        public bool fullyDownloaded { get; set; }
        private BufferedWaveProvider bufferedWaveProvider;
        private IWavePlayer waveOut;
        private VolumeWaveProvider16 volumeProvider;
        private System.Windows.Forms.Timer timer1;
        
        public delegate void PlaybackManagerErrorHandler(string message);
        public event PlaybackManagerErrorHandler OnError;
        public event Action OnPlaybackStopped;

        public PlaybackManager()
        {
            this.timer1 = new System.Windows.Forms.Timer();
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            gatekeeper = new ByteGateKeeper();
        }

        public void AddBytesToPlayingStream(byte[] bytes)
        {
            gatekeeper.QueueBytes(bytes);
        }

        public void Play()
        {
            if (playbackState == StreamingPlaybackState.Stopped)
            {
                playbackState = StreamingPlaybackState.Buffering;
                if (this.bufferedWaveProvider != null)
                {
                    this.bufferedWaveProvider.ClearBuffer();
                }
                ThreadPool.QueueUserWorkItem(new WaitCallback(GetStreaming), null);
                timer1.Enabled = true;
            }
            else if (playbackState == StreamingPlaybackState.Paused)
            {
                playbackState = StreamingPlaybackState.Buffering;
            }
        }

        public void Pause()
        {
            if (playbackState == StreamingPlaybackState.Playing || playbackState == StreamingPlaybackState.Buffering)
            {
                waveOut.Pause();
                Logger.WriteDebug(String.Format("User requested Pause, waveOut.PlaybackState={0}", waveOut.PlaybackState));
                playbackState = StreamingPlaybackState.Paused;
            }
        }

        public void Stop()
        {
            if (playbackState != StreamingPlaybackState.Stopped)
            {
                this.playbackState = StreamingPlaybackState.Stopped;
                if (waveOut != null)
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
                // n.b. streaming thread may not yet have exited
                Thread.Sleep(500);
                gatekeeper.Clear();
                this.bufferedWaveProvider = null;
                timer1.Enabled = false;
                if (OnPlaybackStopped != null)
                {
                    OnPlaybackStopped();
                }
            }
        }

        public void VolumeUp(float amount)
        {
            if (volume + amount <= 1f)
            {
                this.volume += amount;
            }
            else
            {
                this.volume = 1f;
            }
            if (this.volumeProvider != null)
            {
                this.volumeProvider.Volume = volume;
            }
        }

        public void VolumeDown(float amount)
        {
            if (volume - amount >= 0f)
            {
                this.volume -= amount;
            }
            else
            {
                this.volume = 0;
            }
            if (this.volumeProvider != null)
            {
                this.volumeProvider.Volume = volume;
            }
        }

        // modified from NAudio sample code but working with incoming bytes
        private void GetStreaming(object state)
        {
            this.fullyDownloaded = false;

            try
            {
                do
                {
                    if (bufferedWaveProvider == null)
                    {
                        this.bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44100, 2));
                        this.bufferedWaveProvider.BufferDuration = TimeSpan.FromSeconds(20); // allow us to get well ahead of ourselves
                        Logger.WriteDebug("Creating buffered wave provider");
                        this.gatekeeper.MinimumSampleSize = bufferedWaveProvider.WaveFormat.AverageBytesPerSecond * secondsToBuffer;
                    }
                    if (bufferedWaveProvider != null && bufferedWaveProvider.BufferLength - bufferedWaveProvider.BufferedBytes < bufferedWaveProvider.WaveFormat.AverageBytesPerSecond / 4)
                    {
                        Logger.WriteDebug("Buffer getting full, taking a break");
                        Thread.Sleep(500);
                    }
                    // do we have at least double the buffered sample's size in free space, just in case
                    else if (bufferedWaveProvider.BufferLength - bufferedWaveProvider.BufferedBytes > bufferedWaveProvider.WaveFormat.AverageBytesPerSecond * (secondsToBuffer * 2))
                    {
                        var sample = gatekeeper.Read();
                        if (sample != null)
                        {
                            bufferedWaveProvider.AddSamples(sample, 0, sample.Length);
                        }
                    }
                } while (playbackState != StreamingPlaybackState.Stopped);
                Logger.WriteDebug("Playback stopped");
            }
            finally
            {
                // no post-processing work here, right?
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (playbackState != StreamingPlaybackState.Stopped)
            {
                if (this.waveOut == null && this.bufferedWaveProvider != null)
                {
                    Logger.WriteDebug("Creating WaveOut Device");
                    this.waveOut = CreateWaveOut();
                    waveOut.PlaybackStopped += waveOut_PlaybackStopped;
                    this.volumeProvider = new VolumeWaveProvider16(bufferedWaveProvider);
                    this.volumeProvider.Volume = this.volume;
                    waveOut.Init(volumeProvider);
                }
                if (bufferedWaveProvider != null)
                {
                    var bufferedSeconds = bufferedWaveProvider.BufferedDuration.TotalSeconds;
                    // make it stutter less if we buffer up a decent amount before playing
                    if (bufferedSeconds < 0.5 && this.playbackState == StreamingPlaybackState.Playing && !this.fullyDownloaded)
                    {
                        this.playbackState = StreamingPlaybackState.Buffering;
                        waveOut.Pause();
                        Logger.WriteDebug(String.Format("Paused to buffer, waveOut.PlaybackState={0}", waveOut.PlaybackState));
                    }
                    else if (bufferedSeconds > secondsToBuffer && this.playbackState == StreamingPlaybackState.Buffering)
                    {
                        waveOut.Play();
                        Logger.WriteDebug(String.Format("Started playing, waveOut.PlaybackState={0}", waveOut.PlaybackState));
                        this.playbackState = StreamingPlaybackState.Playing;
                    }
                    else if (this.fullyDownloaded && bufferedSeconds < secondsToBuffer)
                    {
                        // put the last bytes in the buffer if there's only spare bytes left
                        if (gatekeeper.IsSlidingStreamEmpty && gatekeeper.HasSpareBytes)
                        {
                            var sample = gatekeeper.ReadSpareBytes();
                            Logger.WriteDebug("{0} bytes left as spare bytes, adding to the end", sample.Length);
                            bufferedWaveProvider.AddSamples(sample, 0, sample.Length);
                        }
                        // if there's no spare bytes and the stream is empty, stop
                        else if (bufferedSeconds == 0 && gatekeeper.IsSlidingStreamEmpty)
                        {
                            Logger.WriteDebug("Reached end of stream");
                            Stop();
                        }
                    }
                }
            }
        }

        private IWavePlayer CreateWaveOut()
        {
            return new WaveOut();
        }

        private void waveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            Logger.WriteDebug("Playback Stopped");
            if (e.Exception != null && OnError != null)
            {
                OnError(e.Exception.Message);
            }
        }

        public void Dispose()
        {
            Stop();
            timer1.Dispose();
            gatekeeper.Dispose();
        }
    }
}