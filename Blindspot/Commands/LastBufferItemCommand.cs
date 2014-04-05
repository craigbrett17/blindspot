using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class LastBufferItemCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;

        public LastBufferItemCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
        }

        public override string Key
        {
            get { return "last_buffer_item"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            buffers.CurrentList.LastItem();
            ScreenReader.SayString(buffers.CurrentList.CurrentItem.ToString());
        }
    }
}