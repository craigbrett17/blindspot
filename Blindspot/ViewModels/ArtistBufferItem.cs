using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blindspot.Core.Models;

namespace Blindspot.ViewModels
{
    public class ArtistBufferItem : BufferItem
    {
        public Artist Model { get; set; }

        public ArtistBufferItem(Artist artist)
        {
            this.Model = artist;
        }

        public override string ToString()
        {
            return Model.Name;
        }
    }
}