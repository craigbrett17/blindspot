using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.Helpers;
using Blindspot.ViewModels;

namespace Blindspot.Commands
{
    public class NextBufferItemCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;

        public NextBufferItemCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
        }

        public override string Key
        {
            get { return "next_buffer_item"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            buffers.CurrentList.NextItem();
            var output = OutputManager.Instance;
            output.OutputMessage(buffers.CurrentList.CurrentItem.ToString(), true, NavigationDirection.Down);
        }
    }
}