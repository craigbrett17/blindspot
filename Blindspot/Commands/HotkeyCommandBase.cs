using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Blindspot.Commands
{
    /// <summary>
    /// An abstract class to inherit commands from
    /// </summary>
    public abstract class HotkeyCommandBase
    {
        /// <summary>
        /// The key for the command. This should directly reference the name given to the command in the hotkeys files
        /// </summary>
        public abstract string Key { get; }
        /// <summary>
        /// The command's execution logic
        /// </summary>
        /// <param name="sender">The object that sent this command</param>
        /// <param name="e">Any additional event info</param>
        public abstract void Execute(object sender, HandledEventArgs e);
    }
}