using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blindspot.Controllers;

namespace Blindspot.ViewModels
{
    public class PlaylistBufferItem : BufferItem
    {
        public PlaylistContainer.PlaylistInfo Model { get; set; }
        
        public PlaylistBufferItem(PlaylistContainer.PlaylistInfo info)
        {
            this.Model = info;
        }

        public override string ToString()
        {
            return Model.Name;
        }
    }
}
