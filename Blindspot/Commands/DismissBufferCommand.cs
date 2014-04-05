using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class DismissBufferCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;

        public DismissBufferCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
        }

        public override string Key
        {
            get { return "dismiss_buffer"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var currentBuffer = buffers.CurrentList;
            if (!currentBuffer.IsDismissable)
            {
                ScreenReader.SayString(String.Format("{0} {1}", StringStore.CannotDismissBuffer, currentBuffer.Name));
            }
            else
            {
                buffers.PreviousList();
                buffers.Remove(currentBuffer);
                ScreenReader.SayString(buffers.CurrentList.ToString());
            }
            // if it's a buffer with a search or other unmanaged resources, dispose it
            if (currentBuffer is IDisposable)
            {
                ((IDisposable)currentBuffer).Dispose();
            }
        }

    }
}
