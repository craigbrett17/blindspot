using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Blindspot.ViewModels;

namespace Blindspot.Commands
{
    public class AddToPlaylistCommand : HotkeyCommandBase
    {
        private BufferListCollection _buffers;

        public AddToPlaylistCommand(BufferListCollection buffers)
        {
            _buffers = buffers;
        }
        
        public override string Key
        {
            get { return "add_to_playlist"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var item = _buffers.CurrentList.CurrentItem;
            if (!(item is TrackBufferItem))
                return;

            var dialog = new AddToPlaylistWindow();
            dialog.ShowDialog();
            if (dialog.DialogResult != DialogResult.OK)
                return;
        }
    }
}