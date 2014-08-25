using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.Helpers;

namespace Blindspot.Commands
{
    public class MediaPlayPauseCommand : HotkeyCommandBase
    {
        private PlaybackManager playbackManager;

        public MediaPlayPauseCommand(PlaybackManager managerIn)
        {
            playbackManager = managerIn;
        }

        public override string Key
        {
            get { return "media_play_pause"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            if (!playbackManager.IsPaused)
            {
                // Session.Pause();
                playbackManager.Pause();
            }
            else
            {
                // Session.Play();
                playbackManager.Play();
            }
        }

    }
}