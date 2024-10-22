﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.Helpers;
using Blindspot.Playback;

namespace Blindspot.Commands
{
    public class AnnounceNowPlayingCommand : HotkeyCommandBase
    {
        private PlaybackManager playbackManager;
        private IOutputManager _output;

        public AnnounceNowPlayingCommand(PlaybackManager managerIn)
        {
            playbackManager = managerIn;
            _output = OutputManager.Instance;
        }

        public override string Key
        {
            get { return "announce_now_playing"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            if (playbackManager.PlayingTrack != null)
            {
                _output.OutputTrackModel(playbackManager.PlayingTrack);
            }
            else
            {
                _output.OutputMessage(StringStore.NoTrackCurrentlyBeingPlayed);
            }
        }

    }
}
