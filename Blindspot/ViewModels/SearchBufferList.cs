using Blindspot.Core;
using Blindspot.Core.Models;
using ScreenReaderAPIWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blindspot.ViewModels
{
    public class SearchBufferList : BufferList, IDisposable
    {
        private Search _search;

        public SearchBufferList(Search searchIn)
        {
            _search = searchIn;
            Name = searchIn.Query;
        }

        public SearchBufferList(Search searchIn, bool isDismissableIn)
            : this(searchIn)
        {
            this.IsDismissable = isDismissableIn;
        }

        public override void NextItem()
        {
            if (this.Count > 0 && CurrentItemIndex == this.Count - 1)
            {
                GetNextSetOfResults();
            }
            base.NextItem();
        }

        private void GetNextSetOfResults()
        {
            _search = SpotifyClient.Instance.GetMoreResultsFromSearch(_search);
            switch (_search.Type)
            {
                case SearchType.Track:
                    AddNewTracks();
                    break;
                case SearchType.Artist:
                    AddNewAartists();
                    break;
                case SearchType.Album:
                    AddNewAlbums();
                    break;
                default:
                    break;
            }
        }

        private void AddNewTracks()
        {
            if (_search.Tracks == null || _search.Tracks.Count == 0) return; // no new search results
            _search.Tracks.ForEach(pointer =>
            {
                this.Add(new TrackBufferItem(new Track(pointer)));
            });
        }

        private void AddNewAartists()
        {
            if (_search.Artists == null || _search.Artists.Count == 0) return;
            _search.Artists.ForEach(pointer =>
            {
                this.Add(new ArtistBufferItem(new Artist(pointer)));
            });
        }

        private void AddNewAlbums()
        {
            if (_search.Albums == null || _search.Albums.Count == 0) return;
            _search.Albums.ForEach(pointer =>
            {
                this.Add(new AlbumBufferItem(new Album(pointer)));
            });
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}: {2} {3} {4} {5}", StringStore.SearchFor, _search.Query, this.CurrentItemIndex + 1, StringStore.Of, this.Count, StringStore.Items);
        }

        public void Dispose()
        {
            _search.Dispose();
        }
    }
}