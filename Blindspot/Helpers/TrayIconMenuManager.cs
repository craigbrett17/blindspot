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
            var currentList = _buffers.CurrentList;
            var currentItem = _buffers.CurrentList.CurrentItem;

            if (currentList is PlaylistBufferList)
                AddPlaylistListMenuItems();
            else if (currentList is AlbumBufferList)
                AddAlbumListMenuItems();
            else if (currentList is SearchBufferList)
                AddSearchListMenuItems();

            if (currentItem is PlaylistBufferItem)
                AddPlaylistItemMenuItems();
            else if (currentItem is AlbumBufferItem)
                AddAlbumItemMenuItems();
            else if (currentItem is TrackBufferItem)
                AddTrackItemMenuItems();

            _trayIcon.ContextMenuStrip.Items.AddRange(globalTrayIconMenuItems);
        }

        private void AddSearchListMenuItems()
        {
            
        }

        private void AddAlbumListMenuItems()
        {
            
        }

        private void AddPlaylistListMenuItems()
        {
            
        }

        private void AddTrackItemMenuItems()
        {
            _trayIcon.ContextMenuStrip.Items.Add(MakeCommandMenuItem("Add to playlist", "add_to_playlist"));
        }

        private void AddAlbumItemMenuItems()
        {
            
        }

        private void AddPlaylistItemMenuItems()
        {
            
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