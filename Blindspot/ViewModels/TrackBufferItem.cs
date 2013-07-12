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
            StringBuilder outString = new StringBuilder();
            if (Model == null)
            {
                outString.Append("No track information. This is embarrassing. ");
                return outString.ToString();
            }
            outString.Append(Model.Name);
            if (Model.Album != null || !String.IsNullOrEmpty(Model.Album.Artist))
            {
                outString.Append(" by ");
                outString.Append(Model.Album.Artist);
            }
            return outString.ToString();
        }
    }
}
