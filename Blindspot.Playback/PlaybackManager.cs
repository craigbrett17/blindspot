using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using System.IO;
using System.Threading;
using Logger = Blindspot.Core.Logger;
using Blindspot.Core.Models;

namespace Blindspot.Playback
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
        
        const int secondsToBuffer = 3;
        private volatile float volume = 1f;
        private volatile StreamingPlaybackState playbackState;
        public bool fullyDownloaded { get; set; }
        private BufferedWaveProvider bufferedWaveProvider;
        private IWavePlayer waveOut;
        private VolumeWaveProvider16 volumeProvider;
        private System.Timers.Timer timer1;
        private Stack<Track> _previousTracks { get; set; }
        private Track _playingTrackItem;
        public Track PlayingTrack
        {
            get { return _playingTrackItem; }
            set
            {
                _playingTrackItem = value;
                if (OnPlayingTrackChanged != null)
                    OnPlayingTrackChanged();
            }
        }
        private Guid _playbackDeviceID;
        public Guid PlaybackDeviceID
        {
            get { return _playbackDeviceID; }
            set
            {
                _playbackDeviceID = value;
                if (waveOut != null)
                {
                    waveOut.Pause(); // so we don't lose our place in the track
                    // make new replacement WaveOut for new device
                    var newWaveOut = new DirectSoundOut(_playbackDeviceID);
                    if (bufferedWaveProvider != null && volumeProvider != null)
                        newWaveOut.Init(volumeProvider);

                    // destroy the old WaveOut and replace it with our new one
                    StopAndDisposeWaveOut();
                    waveOut = newWaveOut;

                    if (playbackState == StreamingPlaybackState.Playing)
                        waveOut.Play();
                }
            }
        }
		private bool DirectSound { get; set; }

        public delegate void PlaybackManagerErrorHandler(string message);
        public event PlaybackManagerErrorHandler OnError;
        public event Action OnEndOfTrack;
        public event Action OnPlaybackStopped;
        public event Action OnPlayingTrackChanged;

        public PlaybackManager()
        {
            this.timer1 = new System.Timers.Timer();
            this.timer1.Interval = 250;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            _previousTracks = new Stack<Track>();
        }

        public PlaybackManager(Guid playbackDeviceId)
            : this()
        {
            _playbackDeviceID = playbackDeviceId;
        }

		public PlaybackManager(Guid playbackDeviceId, bool useDirectSound)
			: this(playbackDeviceId)
		{
			DirectSound = useDirectSound;
		}

        public void AddBytesToPlayingStream(byte[] bytes)
        {
            if (bufferedWaveProvider != null)
                bufferedWaveProvider.AddSamples(bytes, 0, bytes.Length);
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
                StopAndDisposeWaveOut();
                // n.b. streaming thread may not yet have exited
                Thread.Sleep(500);
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
                        this.bufferedWaveProvider.BufferDuration = TimeSpan.FromMinutes(20); // allow us to get well ahead of ourselves
                        Logger.WriteDebug("Creating buffered wave provider");
                    }
                    // in case its still null
                    if (bufferedWaveProvider == null)
                        continue;

                    var freeBufferBytes = bufferedWaveProvider.BufferLength - bufferedWaveProvider.BufferedBytes;
                    if (freeBufferBytes < bufferedWaveProvider.WaveFormat.AverageBytesPerSecond / 4)
                    {
                        Logger.WriteDebug("Buffer getting full, taking a break");
                        Thread.Sleep(450);
                    }
                    Thread.Sleep(250);
                } while (playbackState != StreamingPlaybackState.Stopped);
                Logger.WriteDebug("Playback stopped");
            }
            finally
            {
                // no post-processing work here, right?
            }
        }

        private void timer1_Tick(object sender, System.Timers.ElapsedEventArgs e)
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
                    // stop when right at the end
                    else if (this.fullyDownloaded && bufferedSeconds == 0)
                    {
                        Logger.WriteDebug("Reached end of stream");
                        Stop();

                        // handle end of track logic
                        if (OnEndOfTrack != null)
                            OnEndOfTrack();
                    }
                }
            }
        }

        private IWavePlayer CreateWaveOut()
        {
			if (this.DirectSound)
            return new DirectSoundOut(PlaybackDeviceID);
			else
				return new WaveOutEvent();
        }

        private void StopAndDisposeWaveOut()
        {
            if (waveOut == null) return;
            waveOut.Stop();
            waveOut.Dispose();
            waveOut = null;
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
        }

        public bool IsPaused
        {
            get { return playbackState == StreamingPlaybackState.Paused; }
        }

        public void AddCurrentTrackToPreviousTracks()
        {
            if (PlayingTrack != null)
                _previousTracks.Push(PlayingTrack);
        }

        public Track GetPreviousTrack()
        {
            if (_previousTracks.Any())
            {
                return _previousTracks.Pop();
            }
            return null;
        }

        public void PutTracksIntoPreviousTracks(IEnumerable<Track> items)
        {
            foreach (var item in items)
            {
                PutTrackIntoPreviousTrack(item);
            }
        }

        public void PutTrackIntoPreviousTrack(Track item)
        {
            _previousTracks.Push(item);
        }

        public bool HasPreviousTracks
        {
            get { return _previousTracks.Any(); }
        }

        public void ClearPreviousTracks()
        {
            _previousTracks.Clear();
        }
    }
}