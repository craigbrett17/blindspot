using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class FirstBufferItemCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;

        public FirstBufferItemCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
        }

        public override string Key
        {
            get { return "first_buffer_item"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            buffers.CurrentList.FirstItem();
            ScreenReader.SayString(buffers.CurrentList.CurrentItem.ToString());
        }
    }
}