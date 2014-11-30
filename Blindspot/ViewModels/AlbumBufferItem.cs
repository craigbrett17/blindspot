using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blindspot.Core.Models;

namespace Blindspot.ViewModels
{
    public class AlbumBufferItem : BufferItem
    {
        public Album Model { get; set; }

        public AlbumBufferItem(Album album)
        {
            this.Model = album;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Model.Name, StringStore.By, Model.Artist);
        }
    }
}