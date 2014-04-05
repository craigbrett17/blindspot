using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class PreviousBufferCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;

        public PreviousBufferCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
        }

        public override string Key
        {
            get { return "previous_buffer"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            buffers.PreviousList();
            ScreenReader.SayString(buffers.CurrentList.ToString());
        }
    }
}