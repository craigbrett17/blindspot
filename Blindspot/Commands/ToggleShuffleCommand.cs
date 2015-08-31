using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.Helpers;

namespace Blindspot.Commands
{
    public class ToggleShuffleCommand : HotkeyCommandBase
    {
        public override string Key
        {
            get { return "toggle_shuffle"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var options = UserSettings.Instance;
            var shuffling = options.Shuffle;
            // negate the current shuffling setting to switch it
            options.Shuffle = !shuffling;

            AnnounceShuffleChange(options.Shuffle);
        }

        private void AnnounceShuffleChange(bool isShuffling)
        {
            var output = OutputManager.Instance;
            var message = (isShuffling)
                ? StringStore.ShuffleOn
                : StringStore.ShuffleOff;
            output.OutputMessage(message);
        }
    }
}