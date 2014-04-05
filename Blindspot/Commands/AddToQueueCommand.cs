using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class AddToQueueCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;

        public AddToQueueCommand(BufferListCollection buffersIn)
        {
            this.buffers = buffersIn;
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
                ScreenReader.SayString(StringStore.AddedToQueue, true);
            }
            else
            {

            }
        }

    }
}
