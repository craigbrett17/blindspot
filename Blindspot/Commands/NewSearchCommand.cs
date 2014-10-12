using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Blindspot.Core;
using Blindspot.Core.Models;
using Blindspot.Helpers;
using Blindspot.ViewModels;

namespace Blindspot.Commands
{
    public class NewSearchCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private OutputManager _output;
        private SpotifyClient spotify = SpotifyClient.Instance;
        
        public NewSearchCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
            _output = OutputManager.Instance;
        }

        public override string Key
        {
            get { return "new_search"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            SearchWindow searchDialog = new SearchWindow();
            searchDialog.ShowDialog();
            if (searchDialog.DialogResult != DialogResult.OK)
            {
                return; // cancelled search
            }
            string searchText = searchDialog.SearchText;
            var searchType = searchDialog.Type;
            searchDialog.Dispose();
            if (searchType == SearchType.Track)
            {
                SearchForTracks(searchText);
            }
            else if (searchType == SearchType.Artist)
            {
                // MessageBox.Show("Not implemented yet! Boo to the developers!", StringStore.Oops, MessageBoxButtons.OK, MessageBoxIcon.Error);
                SearchYoutube(searchText);
            }
            else if (searchType == SearchType.Album)
            {
                SearchForAlbums(searchText);
            }
            else if (searchType == SearchType.YoutubeTrack)
            {
                SearchYoutube(searchText);
            }
        }
        
        private void SearchForTracks(string searchText)
        {
            _output.OutputMessage(StringStore.Searching, false);
            var search = spotify.SearchTracks(searchText);
            var searchBuffer = CreateSearchBuffer(search);
            var tracks = search.Tracks;
            if (tracks == null || tracks.Count == 0)
            {
                OutputNoSearchResultsToSearchBuffer(search, searchBuffer);
            }
            else
            {
                _output.OutputMessage(tracks.Count + " " + StringStore.SearchResults, false);
                foreach (IntPtr pointer in tracks)
                {
                    searchBuffer.Add(new TrackBufferItem(new Track(pointer)));
                }
                _output.OutputBufferListState(buffers, NavigationDirection.Right);
            }
        }

        private void SearchForAlbums(string searchText)
        {
            _output.OutputMessage(StringStore.Searching, false);
            Search search = spotify.SearchAlbums(searchText);
            var searchBuffer = CreateSearchBuffer(search);
            var albums = search.Albums;
            if (albums == null || albums.Count == 0)
            {
                OutputNoSearchResultsToSearchBuffer(search, searchBuffer);
            }
            else
            {
                _output.OutputMessage(albums.Count + " " + StringStore.SearchResults, false);
                foreach (IntPtr pointer in albums)
                {
                    searchBuffer.Add(new AlbumBufferItem(new Album(pointer)));
                }
                _output.OutputBufferListState(buffers, NavigationDirection.Right);
            }
        }

        private void SearchYoutube(string searchText)
        {
            _output.OutputMessage(StringStore.Searching, false);
            var tracks = YoutubeClient.Instance.SearchVideos(searchText);
            var searchBuffer = new PlaylistBufferList(String.Format("{0}: {1}", StringStore.SearchFor, searchText));
            buffers.Add(searchBuffer);
            buffers.CurrentListIndex = buffers.Count - 1;
            if (tracks == null || tracks.Count() == 0)
            {
                OutputNoSearchResultsToSearchBuffer(null, searchBuffer);
            }
            else
            {
                _output.OutputMessage(tracks.Count() + " " + StringStore.SearchResults, false);
                foreach (YoutubeTrack track in tracks)
                {
                    searchBuffer.Add(new YoutubeTrackBufferItem(track));
                }
                _output.OutputBufferListState(buffers, NavigationDirection.Right);
            }
        }

        private static void OutputNoSearchResultsToSearchBuffer(Search search, BufferList searchBuffer)
        {
            if (search != null && !String.IsNullOrEmpty(search.DidYouMean))
            {
                searchBuffer.Add(new BufferItem("No search results. Did you mean: " + search.DidYouMean));
            }
            else
            {
                searchBuffer.Add(new BufferItem(StringStore.NoSearchResults));
            }
        }

        private BufferList CreateSearchBuffer(Search search)
        {
            buffers.Add(new SearchBufferList(search));
            buffers.CurrentListIndex = buffers.Count - 1;
            var searchBuffer = buffers.CurrentList;
            return searchBuffer;
        }

    }
}
