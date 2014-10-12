using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blindspot.Core.Models;

namespace Blindspot.ViewModels
{
    public class YoutubeTrackBufferItem : BufferItem
    {
        public YoutubeTrack Model { get; set; }

        public YoutubeTrackBufferItem(YoutubeTrack track)
        {
            this.Model = track;
        }

        public override string ToString()
        {
            return Model.Title;
        }
    }
}