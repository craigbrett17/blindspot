using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NotifyIcon = System.Windows.Forms.NotifyIcon;

namespace Blindspot.Commands
{
    public class ShowContextMenuCommand : HotkeyCommandBase
    {
        private NotifyIcon _notifyIcon;

        public ShowContextMenuCommand(NotifyIcon icon)
        {
            _notifyIcon = icon;
        }
        
        public override string Key
        {
            get { return "show_context_menu"; }
        }

        public override void Execute(object sender, System.ComponentModel.HandledEventArgs e)
        {
            MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(_notifyIcon, null);
        }
    }
}