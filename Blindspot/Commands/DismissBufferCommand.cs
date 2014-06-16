using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using Blindspot.Helpers;

namespace Blindspot.Commands
{
    public class DismissBufferCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private IOutputManager _output;

        public DismissBufferCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
            _output = OutputManager.Instance;
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
                _output.OutputMessage(String.Format("{0} {1}", StringStore.CannotDismissBuffer, currentBuffer.Name));
            }
            else
            {
                buffers.PreviousList();
                buffers.Remove(currentBuffer);
                _output.OutputBufferListState(buffers, NavigationDirection.Left);
            }
            // if it's a buffer with a search or other unmanaged resources, dispose it
            if (currentBuffer is IDisposable)
            {
                ((IDisposable)currentBuffer).Dispose();
            }
        }

    }
}
