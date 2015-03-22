using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        
        public ActivateBufferItemCommand(BufferListCollection buffersIn, PlaybackManager pbManagerIn)
        {
            buffers = buffersIn;
            playbackManager = pbManagerIn;
            _output = OutputManager.Instance;
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
            else if (item is PlaylistBufferItem)
            {
                LoadPlaylist(item);
            }
            else if (item is AlbumBufferItem)
            {
                LoadAlbum(item);
            }
            else if (item is ArtistBufferItem)
            {
                LoadArtist(item);
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
			PlayNewTrackBufferItem(tbi);
			playQueue.CurrentItemIndex = 0;
        }

        private void LoadPlaylist(BufferItem item)
        {
            PlaylistBufferItem pbi = item as PlaylistBufferItem;
            _output.OutputMessage(StringStore.LoadingPlaylist, false);
            var playlist = SpotifyController.GetPlaylist(pbi.Model.Pointer, true);
            buffers.Add(new PlaylistBufferList(playlist));
            buffers.CurrentListIndex = buffers.Count - 1;
            var playlistBuffer = buffers.CurrentList;
            _output.OutputMessage(playlistBuffer.ToString(), false);
            _output.OutputMessage(String.Format("{0} {1}", playlist.TrackCount, StringStore.TracksLoaded), false);
            var tracks = playlist.GetTracks();
            tracks.ForEach(t =>
            {
                playlistBuffer.Add(new TrackBufferItem(t));
            });
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

        private void LoadArtist(BufferItem item)
        {
            ArtistBufferItem abi = item as ArtistBufferItem;
            _output.OutputMessage(StringStore.LoadingArtistAlbums, false);
            buffers.Add(new ArtistBufferList(abi.Model.Name));
            buffers.CurrentListIndex = buffers.Count - 1;
            var artistBuffer = buffers.CurrentList;
            _output.OutputMessage(artistBuffer.ToString(), false);
            var albums = SpotifyController.GetArtistAlbums(abi.Model.ArtistPtr).ToList();
            _output.OutputMessage(String.Format("{0} {1}", albums.Count, StringStore.SearchResults), false);
            albums.ForEach(a =>
            {
                artistBuffer.Add(new AlbumBufferItem(new Album(a)));
            });
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
			if (response.IsError && !UserSettings.Instance.SkipUnplayableTracks)
			{
				_output.OutputMessage(StringStore.UnableToPlayTrack + response.Message, false);
				return;
			}
			if (response.IsError && UserSettings.Instance.SkipUnplayableTracks)
			{
				playbackManager.AddCurrentTrackToPreviousTracks();
				playbackManager.PlayingTrack = null;
				buffers[0].RemoveAt(0);
				PlayNextQueuedTrack();
				return; // don't carry on with this, as it got handled in a recursive call
			}
            Session.Play();
            playbackManager.PlayingTrack = item.Model;
            playbackManager.fullyDownloaded = false;
            playbackManager.Play();
        }

        private void ClearCurrentlyPlayingTrack()
        {
            if (playbackManager.PlayingTrack != null)
            {
                playbackManager.Stop();
                Session.UnloadPlayer();
            }
        }

		private void PlayNextQueuedTrack()
		{
			var playQueue = buffers[0];
			if (playQueue.Count > 0)
			{
				var nextBufferItem = playQueue[0] as TrackBufferItem;
				PlayNewTrackBufferItem(nextBufferItem);
				playQueue.CurrentItemIndex = 0;
			}
		}
		
    }
}