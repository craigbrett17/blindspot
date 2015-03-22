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
            var output = OutputManager.Instance;
			var settings = UserSettings.Instance;
			var response = Session.LoadPlayer(item.Model.TrackPtr);
			if (response.IsError && !settings.SkipUnplayableTracks)
            {
                output.OutputMessage(StringStore.UnableToPlayTrack + response.Message, false);
                return;
            }
			if (response.IsError && settings.SkipUnplayableTracks)
			{
				// move to next track again
				Execute(this, new HandledEventArgs());
				return;
			}
            Session.Play();
            playbackManager.PlayingTrack = item.Model;
            playbackManager.fullyDownloaded = false;
            playbackManager.Play();
            output.OutputTrackModel(playbackManager.PlayingTrack,
                    settings.OutputTrackChangesGraphically, settings.OutputTrackChangesWithSpeech);
        }

        private void ClearCurrentlyPlayingTrack()
        {
            if (playbackManager.PlayingTrack != null)
            {
                playbackManager.Stop();
                Session.UnloadPlayer();
                playbackManager.PlayingTrack = null;
            }
        }

    }
}