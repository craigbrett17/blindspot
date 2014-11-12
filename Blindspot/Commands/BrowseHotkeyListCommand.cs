using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blindspot.Commands
{
    public class BrowseHotkeyListCommand : HotkeyCommandBase
    {
        public override string Key
        {
            get { return "show_hotkey_list"; }
        }

        public override void Execute(object sender, System.ComponentModel.HandledEventArgs e)
        {
            System.Diagnostics.Process.Start("https://blindspot.codeplex.com/wikipage?title=hotkey%20list");
        }
    }
}