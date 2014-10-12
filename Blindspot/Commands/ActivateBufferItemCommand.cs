using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Blindspot.Core;
using Blindspot.Core.Models;
using Blindspot.Helpers;
using Blindspot.Playback;
using Blindspot.ViewModels;
using NotifyIcon = System.Windows.Forms.NotifyIcon;

namespace Blindspot.Commands
{
    // one of our biggest commands, handles what happens when a buffer item is selected
    public class ActivateBufferItemCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private PlaybackManager playbackManager;
        private IOutputManager _output;
        private WebBrowser _youtubePlayer;
        private string _currentYoutubeId = null;
        
        public ActivateBufferItemCommand(BufferListCollection buffersIn, PlaybackManager pbManagerIn, WebBrowser browserIn)
        {
            buffers = buffersIn;
            playbackManager = pbManagerIn;
            _output = OutputManager.Instance;
            _youtubePlayer = browserIn;
        }
        
        public override string Key
        {
            get { return "activate_buffer_item"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var item = buffers.CurrentList.CurrentItem;
            if (item is TrackBufferItem)
            {
                ActivateTrackItem(item);
            }
            else if (item is YoutubeTrackBufferItem)
            {
                ActivateYoutubeTrackItem(item);
            }
            else if (item is PlaylistBufferItem)
            {
                LoadPlaylist(item);
            }
            else if (item is AlbumBufferItem)
            {
                LoadAlbum(item);
            }
            else
            {
                _output.OutputMessage(String.Format("{0} {1}", item.ToString(), StringStore.ItemActivated), false);
            }
        }
        
        private void ActivateTrackItem(BufferItem item)
        {
            var tbi = item as TrackBufferItem;
            var playQueue = buffers[0];
            if (playbackManager.PlayingTrack != null && tbi.Model.TrackPtr == playbackManager.PlayingTrack.TrackPtr)
            {
                TogglePlayPause(playbackManager.IsPaused);
                return;
            }
            ClearCurrentlyPlayingTrack();
            PlayNewTrackBufferItem(tbi);
            if (buffers.CurrentListIndex == 0 && playQueue.Contains(tbi)) // if they've picked it from the play queue
            {
                int indexOfChosenTrack = playQueue.IndexOf(tbi);
                if (indexOfChosenTrack > 0)
                {
                    var skippedTracks = playQueue.Take(indexOfChosenTrack).Cast<TrackBufferItem>().Select(i => i.Model);
                    playbackManager.PutTracksIntoPreviousTracks(skippedTracks);
                    playQueue.RemoveRange(0, indexOfChosenTrack);
                }
            }
            else
            {
                playQueue.Clear();
                playbackManager.ClearPreviousTracks();
                playQueue.Add(tbi);
                if (buffers.CurrentList is PlaylistBufferList || buffers.CurrentList is AlbumBufferList) // add the remaining playlist or album to the queue
                {
                    var tracklist = buffers.CurrentList;
                    int indexOfTrack = tracklist.CurrentItemIndex;
                    if (indexOfTrack > 0)
                    {
                        var preceedingTracks = tracklist.Take(indexOfTrack).Cast<TrackBufferItem>().Select(i => i.Model);
                        playbackManager.PutTracksIntoPreviousTracks(preceedingTracks);
                    }
                    for (int index = indexOfTrack + 1; index < tracklist.Count; index++)
                    {
                        playQueue.Add(tracklist[index]);
                    }
                }
            }
            playQueue.CurrentItemIndex = 0;
        }

        private void LoadPlaylist(BufferItem item)
        {
            PlaylistBufferItem pbi = item as PlaylistBufferItem;
            _output.OutputMessage(StringStore.LoadingPlaylist, false);
            buffers.Add(new PlaylistBufferList(pbi.Model.Name));
            buffers.CurrentListIndex = buffers.Count - 1;
            var playlistBuffer = buffers.CurrentList;
            _output.OutputMessage(playlistBuffer.ToString(), false);
            using (var playlist = SpotifyController.GetPlaylist(pbi.Model.Pointer, true))
            {
                _output.OutputMessage(String.Format("{0} {1}", playlist.TrackCount, StringStore.TracksLoaded), false);
                var tracks = playlist.GetTracks();
                tracks.ForEach(t =>
                {
                    playlistBuffer.Add(new TrackBufferItem(t));
                });
            }
        }

        private void LoadAlbum(BufferItem item)
        {
            AlbumBufferItem abi = item as AlbumBufferItem;
            _output.OutputMessage(StringStore.LoadingAlbum, false);
            buffers.Add(new AlbumBufferList(abi.Model.Name));
            buffers.CurrentListIndex = buffers.Count - 1;
            var albumBuffer = buffers.CurrentList;
            _output.OutputMessage(albumBuffer.ToString(), false);
            var tracks = SpotifyController.GetAlbumTracks(abi.Model.AlbumPtr).ToList();
            _output.OutputMessage(String.Format("{0} {1}", tracks.Count, StringStore.TracksLoaded), false);
            tracks.ForEach(t =>
            {
                albumBuffer.Add(new TrackBufferItem(new Track(t)));
            });
        }

        private void ActivateYoutubeTrackItem(BufferItem item)
        {
            var tbi = item as YoutubeTrackBufferItem;
            var playQueue = buffers[0];
            if (_currentYoutubeId != null && tbi.Model.Id == _currentYoutubeId)
            {
                ToggleYoutubePlayPause(playbackManager.IsPaused);
                return;
            }
            ClearCurrentlyPlayingTrack();
            PlayNewYoutubeTrackBufferItem(tbi);
            if (buffers.CurrentListIndex == 0 && playQueue.Contains(tbi)) // if they've picked it from the play queue
            {
                int indexOfChosenTrack = playQueue.IndexOf(tbi);
                if (indexOfChosenTrack > 0)
                {
                    var skippedTracks = playQueue.Take(indexOfChosenTrack).Cast<TrackBufferItem>().Select(i => i.Model);
                    playbackManager.PutTracksIntoPreviousTracks(skippedTracks);
                    playQueue.RemoveRange(0, indexOfChosenTrack);
                }
            }
            else
            {
                playQueue.Clear();
                playbackManager.ClearPreviousTracks();
                playQueue.Add(tbi);
            }
            playQueue.CurrentItemIndex = 0;
        }

        private void ToggleYoutubePlayPause(bool p)
        {
            _output.OutputMessage("Pausing");
        }

        private void TogglePlayPause(bool isPaused)
        {
            if (!isPaused)
            {
                // Session.Pause();
                playbackManager.Pause();
                _output.OutputMessage(StringStore.Paused);
            }
            else
            {
                // Session.Play();
                playbackManager.Play();
                _output.OutputMessage(StringStore.Playing);
            }
        }
        
        private void PlayNewTrackBufferItem(TrackBufferItem item)
        {
            var response = Session.LoadPlayer(item.Model.TrackPtr);
            if (response.IsError)
            {
                _output.OutputMessage(StringStore.UnableToPlayTrack + response.Message, false);
                return;
            }
            Session.Play();
            playbackManager.PlayingTrack = item.Model;
            playbackManager.fullyDownloaded = false;
            playbackManager.Play();
        }

        private void PlayNewYoutubeTrackBufferItem(YoutubeTrackBufferItem item)
        {
            _youtubePlayer.Navigate(new Uri("http://www.youtube.com/watch?v=" + item.Model.Id + "&autoplay=true&html5=true"));
            _currentYoutubeId = item.Model.Id;
        }

        private void ClearCurrentlyPlayingTrack()
        {
            if (playbackManager.PlayingTrack != null)
            {
                playbackManager.Stop();
                Session.UnloadPlayer();
            }
            if (_youtubePlayer != null)
            {
                _youtubePlayer.Hide();
            }
        }

    }
}