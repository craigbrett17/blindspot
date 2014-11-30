using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blindspot.Commands
{
    public class BrowseGettingStartedCommand : HotkeyCommandBase
    {
        public override string Key
        {
            get { return "show_getting_started"; }
        }

        public override void Execute(object sender, System.ComponentModel.HandledEventArgs e)
        {
            System.Diagnostics.Process.Start("https://blindspot.codeplex.com/wikipage?title=Getting%20Started");
        }
    }
}