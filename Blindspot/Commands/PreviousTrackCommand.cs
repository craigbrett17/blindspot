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

namespace Blindspot.Commands
{
    public class PreviousTrackCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private PlaybackManager playbackManager;

        public PreviousTrackCommand(BufferListCollection buffersIn, PlaybackManager pbManagerIn)
        {
            buffers = buffersIn;
            playbackManager = pbManagerIn;
        }

        public override string Key
        {
            get { return "previous_track"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var playQueue = buffers[0];
            ClearCurrentlyPlayingTrack();

            if (playbackManager.HasPreviousTracks)
            {
                var nextTrack = playbackManager.GetPreviousTrack();
                var nextTrackItem = new TrackBufferItem(nextTrack);
                playQueue.Insert(0, nextTrackItem);
                PlayNewTrackBufferItem(nextTrackItem);
                playQueue.CurrentItemIndex = 0;
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
				// try skipping back to the previous track
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