using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blindspot.Controllers;

namespace Blindspot.ViewModels
{
    public class TrackBufferItem : BufferItem
    {
        public Track Model { get; set; }

        public TrackBufferItem(Track trackIn)
        {
            this.Model = trackIn;
        }

        public override string ToString()
        {
            if (Model == null)
            {
                return "No track information. This is embarrassing. ";
            }
            return Model.ToString();
        }
    }
}
