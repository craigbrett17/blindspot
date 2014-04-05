using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class NextBufferCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;

        public NextBufferCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
        }

        public override string Key
        {
            get { return "next_buffer"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            buffers.NextList();
            ScreenReader.SayString(buffers.CurrentList.ToString());
        }
    }
}