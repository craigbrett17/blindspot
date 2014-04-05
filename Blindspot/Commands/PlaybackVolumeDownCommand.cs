using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blindspot.Helpers;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class PlaybackVolumeDownCommand : HotkeyCommandBase
    {
        private PlaybackManager playbackManager;

        public PlaybackVolumeDownCommand(PlaybackManager managerIn)
        {
            playbackManager = managerIn;
        }

        public override string Key
        {
            get { return "playback_volume_down"; }
        }

        public override void Execute(object sender, System.ComponentModel.HandledEventArgs e)
        {
            playbackManager.VolumeDown(0.05f);
            ScreenReader.SayString(StringStore.Quieter);
        }
    }
}
