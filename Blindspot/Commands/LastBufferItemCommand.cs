using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using Blindspot.Helpers;

namespace Blindspot.Commands
{
    public class LastBufferItemCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private IOutputManager _output;

        public LastBufferItemCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
            _output = OutputManager.Instance;
        }

        public override string Key
        {
            get { return "last_buffer_item"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            buffers.CurrentList.LastItem();
            _output.OutputMessage(buffers.CurrentList.CurrentItem.ToString(), navigationDirection: NavigationDirection.Down);
        }
    }
}