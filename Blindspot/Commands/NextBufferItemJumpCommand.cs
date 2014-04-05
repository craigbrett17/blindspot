using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class NextBufferItemJumpCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;

        public NextBufferItemJumpCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
        }

        public override string Key
        {
            get { return "next_buffer_item_jump"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            buffers.CurrentList.NextJump();
            ScreenReader.SayString(buffers.CurrentList.CurrentItem.ToString());
        }
    }
}