using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blindspot.Core.Models;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.YouTube;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Blindspot.Core
{
    public interface IYoutubeClient
    {
        IEnumerable<YoutubeTrack> SearchVideos(string query, int maxResults = 50);
    }
    
    public class YoutubeClient : IYoutubeClient
    {
        private const string YoutubeKey = "AIzaSyBNfuXu5NOuYRplHsD6o6IWU-h8SBrJ3LQ";
        private static YoutubeClient _instance;
        public static YoutubeClient Instance
        {
            get { return _instance ?? (_instance = new YoutubeClient()); }
            set { _instance = value; }
        }

        private YouTubeService _service;

        private YoutubeClient()
        {
            _service = new YouTubeService(new BaseClientService.Initializer()
            {
                ApplicationName = "Blindspot",
                ApiKey = YoutubeKey
            });
        }
        
        public IEnumerable<YoutubeTrack> SearchVideos(string query, int maxResults = 50)
        {
            var list = _service.Search.List("snippet");
            list.Q = query;
            list.MaxResults = maxResults;
            list.Type = "youtube#video";
            var results = list.Execute().Items;
            foreach (var result in results)
            {
                yield return new YoutubeTrack(result);
            }
        }
    }
}