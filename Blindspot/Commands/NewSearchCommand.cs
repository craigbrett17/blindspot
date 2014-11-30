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
                SearchForArtists(searchText);
            }
            else if (searchType == SearchType.Album)
            {
                SearchForAlbums(searchText);
            }
        }
        
        private void SearchForTracks(string searchText)
        {
            _output.OutputMessage(StringStore.Searching, false);
            var search = spotify.Search(searchText, SearchType.Track);
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
            Search search = spotify.Search(searchText, SearchType.Album);
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

        private void SearchForArtists(string searchText)
        {
            _output.OutputMessage(StringStore.Searching, false);
            Search search = spotify.Search(searchText, SearchType.Artist);
            var searchBuffer = CreateSearchBuffer(search);
            var artists = search.Artists;
            if (artists == null || artists.Count == 0)
            {
                OutputNoSearchResultsToSearchBuffer(search, searchBuffer);
            }
            else
            {
                _output.OutputMessage(artists.Count + " " + StringStore.SearchResults, false);
                foreach (IntPtr pointer in artists)
                {
                    searchBuffer.Add(new ArtistBufferItem(new Artist(pointer)));
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
