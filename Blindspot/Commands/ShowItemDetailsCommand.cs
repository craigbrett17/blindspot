using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class ShowItemDetailsCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;

        public ShowItemDetailsCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
        }
        
        public override string Key
        {
            get { return "item_details"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            BufferItem item = buffers.CurrentList.CurrentItem;
            object model = null;
            if (item is TrackBufferItem) model = ((TrackBufferItem)item).Model;
            else if (item is PlaylistBufferItem) model = ((PlaylistBufferItem)item).Model;
            if (model != null)
            {
                ItemDetailsWindow detailsView = new ItemDetailsWindow(model);
                detailsView.ShowDialog();
            }
            else
            {
                ScreenReader.SayString("View details is not a valid action for this type of item", true);
            }
        }

    }
}
