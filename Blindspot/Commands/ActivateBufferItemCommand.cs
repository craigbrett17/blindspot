using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.Core;
using Blindspot.Core.Models;
using Blindspot.Helpers;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;
using NotifyIcon = System.Windows.Forms.NotifyIcon;

namespace Blindspot.Commands
{
    // one of our biggest commands, handles what happens when a buffer item is selected
    public class ActivateBufferItemCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private PlaybackManager playbackManager;
        
        public ActivateBufferItemCommand(BufferListCollection buffersIn, PlaybackManager pbManagerIn)
        {
            buffers = buffersIn;
            playbackManager = pbManagerIn;
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
            else
            {
                ScreenReader.SayString(String.Format("{0} {1}", item.ToString(), StringStore.ItemActivated), false);
            }
        }

        private void ActivateTrackItem(BufferItem item)
        {
            var tbi = item as TrackBufferItem;
            var playQueue = buffers[0];
            if (playbackManager.PlayingTrackItem != null && tbi.Model.TrackPtr == playbackManager.PlayingTrackItem.Model.TrackPtr)
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
                    playQueue.RemoveRange(0, indexOfChosenTrack);
                }
            }
            else
            {
                playQueue.Clear();
                playQueue.Add(tbi);
                if (buffers.CurrentList is PlaylistBufferList) // add the remaining playlist to the queue
                {
                    var playlist = buffers.CurrentList as PlaylistBufferList;
                    int indexOfTrack = playlist.CurrentItemIndex;
                    for (int index = indexOfTrack + 1; index < playlist.Count; index++)
                    {
                        playQueue.Add(playlist[index]);
                    }
                }
            }
            playQueue.CurrentItemIndex = 0;
        }

        private void LoadPlaylist(BufferItem item)
        {
            PlaylistBufferItem pbi = item as PlaylistBufferItem;
            ScreenReader.SayString(StringStore.LoadingPlaylist, false);
            buffers.Add(new PlaylistBufferList(pbi.Model.Name));
            buffers.CurrentListIndex = buffers.Count - 1;
            var playlistBuffer = buffers.CurrentList;
            ScreenReader.SayString(playlistBuffer.ToString(), false);
            using (var playlist = SpotifyController.GetPlaylist(pbi.Model.Pointer, true))
            {
                ScreenReader.SayString(String.Format("{0} {1}", playlist.TrackCount, StringStore.TracksLoaded), false);
                var tracks = playlist.GetTracks();
                tracks.ForEach(t =>
                {
                    playlistBuffer.Add(new TrackBufferItem(t));
                });
            }
        }

        private void TogglePlayPause(bool isPaused)
        {
            if (!isPaused)
            {
                // Session.Pause();
                playbackManager.Pause();
                ScreenReader.SayString(StringStore.Paused);
            }
            else
            {
                // Session.Play();
                playbackManager.Play();
                ScreenReader.SayString(StringStore.Playing);
            }
        }
        
        private void PlayNewTrackBufferItem(TrackBufferItem item)
        {
            var response = Session.LoadPlayer(item.Model.TrackPtr);
            if (response.IsError)
            {
                ScreenReader.SayString(StringStore.UnableToPlayTrack + response.Message, false);
                return;
            }
            Session.Play();
            playbackManager.PlayingTrackItem = item;
            playbackManager.fullyDownloaded = false;
            playbackManager.Play();
        }

        private void ClearCurrentlyPlayingTrack()
        {
            if (playbackManager.PlayingTrackItem != null)
            {
                playbackManager.Stop();
                Session.UnloadPlayer();
            }
        }

    }
}