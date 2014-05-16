using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.Helpers;
using Blindspot.ViewModels;

namespace Blindspot.Commands
{
    public class FirstBufferItemCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private IOutputManager _output;

        public FirstBufferItemCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
            _output = OutputManager.Instance;
        }

        public override string Key
        {
            get { return "first_buffer_item"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            buffers.CurrentList.FirstItem();
            _output.OutputMessage(buffers.CurrentList.CurrentItem.ToString(), navigationDirection: NavigationDirection.Up);
        }
    }
}