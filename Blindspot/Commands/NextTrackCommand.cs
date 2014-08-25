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
    public class NextTrackCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private PlaybackManager playbackManager;
        
        public NextTrackCommand(BufferListCollection buffersIn, PlaybackManager pbManagerIn)
        {
            buffers = buffersIn;
            playbackManager = pbManagerIn;
        }
        
        public override string Key
        {
            get { return "next_track"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var playQueue = buffers[0];
            if (playQueue.Count > 0)
            {
                playbackManager.AddCurrentTrackToPreviousTracks();
                ClearCurrentlyPlayingTrack();
                playQueue.RemoveAt(0);

                if (playQueue.Count > 0)
                {
                    var nextTrack = playQueue[0] as TrackBufferItem;
                    if (nextTrack == null) return;

                    PlayNewTrackBufferItem(nextTrack);
                    playQueue.CurrentItemIndex = 0;
                }
            }
        }

        private void PlayNewTrackBufferItem(TrackBufferItem item)
        {
            var response = Session.LoadPlayer(item.Model.TrackPtr);
            var output = OutputManager.Instance;
            if (response.IsError)
            {
                output.OutputMessage(StringStore.UnableToPlayTrack + response.Message, false);
                return;
            }
            Session.Play();
            playbackManager.PlayingTrackItem = item;
            playbackManager.fullyDownloaded = false;
            playbackManager.Play();
            var settings = UserSettings.Instance;
            output.OutputTrackItem(playbackManager.PlayingTrackItem,
                    settings.OutputTrackChangesGraphically, settings.OutputTrackChangesWithSpeech);
        }

        private void ClearCurrentlyPlayingTrack()
        {
            if (playbackManager.PlayingTrackItem != null)
            {
                playbackManager.Stop();
                Session.UnloadPlayer();
                playbackManager.PlayingTrackItem = null;
            }
        }

    }
}