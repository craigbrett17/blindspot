using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.Helpers;
using Blindspot.ViewModels;

namespace Blindspot.Commands
{
    public class AddToQueueCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private IOutputManager _output;

        public AddToQueueCommand(BufferListCollection buffersIn)
        {
            this.buffers = buffersIn;
            _output = OutputManager.Instance;
        }
        
        public override string Key
        {
            get { return "add_to_queue"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var item = buffers.CurrentList.CurrentItem;
            var playQueue = buffers[0];
            if (item is TrackBufferItem)
            {
                playQueue.Add(item);
                _output.OutputMessage(StringStore.AddedToQueue, true);
            }
            else
            {

            }
        }

    }
}
