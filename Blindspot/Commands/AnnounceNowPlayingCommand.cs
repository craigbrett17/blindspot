using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.Helpers;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class AnnounceNowPlayingCommand : HotkeyCommandBase
    {
        private PlaybackManager playbackManager;

        public AnnounceNowPlayingCommand(PlaybackManager managerIn)
        {
            playbackManager = managerIn;
        }

        public override string Key
        {
            get { return "announce_now_playing"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            if (playbackManager.PlayingTrackItem != null)
            {
                ScreenReader.SayString(playbackManager.PlayingTrackItem.ToString());
            }
            else
            {
                ScreenReader.SayString(StringStore.NoTrackCurrentlyBeingPlayed);
            }
        }

    }
}
