using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blindspot.Helpers;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class PlaybackVolumeUpCommand : HotkeyCommandBase
    {
        private PlaybackManager playbackManager;

        public PlaybackVolumeUpCommand(PlaybackManager managerIn)
        {
            playbackManager = managerIn;
        }

        public override string Key
        {
            get { return "playback_volume_up"; }
        }

        public override void Execute(object sender, System.ComponentModel.HandledEventArgs e)
        {
            playbackManager.VolumeUp(0.05f);
            ScreenReader.SayString(StringStore.Louder);
        }
    }
}
