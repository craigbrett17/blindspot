using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.Helpers;
using Blindspot.ViewModels;

namespace Blindspot.Commands
{
    public class AddNextInQueueCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private IOutputManager _output;

        public AddNextInQueueCommand(BufferListCollection buffersIn)
        {
            this.buffers = buffersIn;
            _output = OutputManager.Instance;
        }

        public override string Key
        {
            get { return "add_next_in_queue"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var item = buffers.CurrentList.CurrentItem;
            var playQueue = buffers[0];
            if (item is TrackBufferItem)
            {
                if (!playQueue.Any())
                    playQueue.Add(item);
                else
                    playQueue.Insert(1, item);
                _output.OutputMessage(StringStore.AddedToQueue, true);
            }
            else
            {

            }
        }

    }
}
