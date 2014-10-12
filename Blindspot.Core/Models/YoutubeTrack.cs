using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.YouTube.v3.Data;

namespace Blindspot.Core.Models
{
    public class YoutubeTrack
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }

        public YoutubeTrack(SearchResult resultIn)
        {
            this.Title = resultIn.Snippet.Title;
            this.Id = resultIn.Id.VideoId;
            this.Description = resultIn.Snippet.Description;
        }
    }
}