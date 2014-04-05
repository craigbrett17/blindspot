using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class PreviousBufferItemCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;

        public PreviousBufferItemCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
        }

        public override string Key
        {
            get { return "previous_buffer_item"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            buffers.CurrentList.PreviousItem();
            ScreenReader.SayString(buffers.CurrentList.CurrentItem.ToString());
        }
    }
}