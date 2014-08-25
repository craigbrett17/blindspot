using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blindspot.Core;
using Blindspot.Core.Models;

namespace Blindspot.ViewModels
{
    public class TrackBufferItem : BufferItem
    {
        public Track Model { get; set; }
        const int _truncationLength = 51;

        public TrackBufferItem(Track trackIn)
        {
            this.Model = trackIn;
        }

        public override string ToString()
        {
            if (Model == null)
            {
                return StringStore.NoTrackInformation;
            }
            string artistString = GetArtistNames();
            return String.Format("{0} {1} {2}", Model.Name, StringStore.By, artistString);
        }

        public string GetArtistNames()
        {
            string artistString = "Unknown";
            var _artists = Model.Artists;
            if (_artists.Length == 1)
            {
                artistString = _artists.First();
            }
            else if (_artists.Length > 1)
            {
                artistString = String.Join(", ", _artists, 0, _artists.Length - 1) + String.Format(" {0} {1}", StringStore.And, _artists.LastOrDefault());
            }
            return artistString;
        }

        public string ToTruncatedString()
        {
            var originalString = this.ToString();
            if (originalString.Length <= _truncationLength)
                return originalString;

            var titleAndFirstArtist = String.Format("{0} {1} {2}", Model.Name, StringStore.By, Model.Artists.First());
            if (titleAndFirstArtist.Length <= _truncationLength)
                return titleAndFirstArtist;

            if (Model.Name.Length <= _truncationLength)
                return Model.Name;

            return Model.Name.Substring(0, _truncationLength);
        }


    }
}