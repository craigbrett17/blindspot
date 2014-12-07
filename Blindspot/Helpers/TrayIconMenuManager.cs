using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Blindspot.Core;
using Blindspot.ViewModels;

namespace Blindspot.Helpers
{
    public class TrayIconMenuManager
    {
        private BufferListCollection _buffers;
        private Dictionary<string, Commands.HotkeyCommandBase> _commands;
        private NotifyIcon _trayIcon;
        
        public TrayIconMenuManager(BufferListCollection buffers, Dictionary<string, Blindspot.Commands.HotkeyCommandBase> commands, NotifyIcon trayIcon)
        {
            _buffers = buffers;
            _commands = commands;
            _trayIcon = trayIcon;
        }

        public void BuildContextMenu()
        {
            _trayIcon.ContextMenuStrip.Items.AddRange(globalTrayIconMenuItems);
        }
        
        private ToolStripItem[] globalTrayIconMenuItems
        {
            get
            {
                return new ToolStripItem[]
                {
                    MakeCommandMenuItem(StringStore.TrayIconOptionsMenuItemText, "options_dialog"),
                    new ToolStripMenuItem(StringStore.TrayIconHelpMenuItemText, null, new ToolStripItem[]
                    {
                        MakeCommandMenuItem(StringStore.TrayIconGettingStartedMenuItemText, "show_getting_started"),
                        MakeCommandMenuItem(StringStore.TrayIconHotkeyListMenuItemText, "show_hotkey_list"),
                        MakeCommandMenuItem(StringStore.TrayIconAboutMenuItemText, "show_about_window"),
                    }),
                    MakeCommandMenuItem(StringStore.TrayIconExitMenuItemText, "close_blindspot"),
                };
            }
        }

        private ToolStripMenuItem MakeCommandMenuItem(string text, string commandKey)
        {
            if (_commands.ContainsKey(commandKey))
                return new ToolStripMenuItem(text, null, new EventHandler((sender, e) => _commands[commandKey].Execute(this, null)));
            else
            {
                Logger.WriteDebug("No such command {0}", commandKey);
                return null;
            }
        }
    }
}