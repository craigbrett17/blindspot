using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libspotifydotnet;
using System.Runtime.InteropServices;
using Blindspot.Helpers;

namespace Blindspot.Controllers
{
    public class Search : IDisposable
    {
        public string Query { get; private set; }
        public SearchType Type { get; set; }
        public string DidYouMean { get; private set; }
        public bool IsLoaded { get; private set; }
        public List<IntPtr> Tracks { get; set; }
        public List<IntPtr> Artists { get; set; }
        public List<IntPtr> Albums { get; set; }
        
        private search_complete_cb_delegate searchCompleteDelegate;

        private IntPtr _browsePtr;
        private bool _disposed, _searchReleased;
        private int numResults = 50;

        public Search(string query, SearchType typeIn)
        {
            this.Query = query;
            this.Type = typeIn;
        }

        public bool BeginBrowse()
        {
            try
            {
                this.searchCompleteDelegate = new search_complete_cb_delegate(SearchCompleted);
                IntPtr callbackPtr = Marshal.GetFunctionPointerForDelegate(searchCompleteDelegate);

                switch (this.Type)
                {
                    case SearchType.Track:
                        _browsePtr = libspotify.sp_search_create(Session.GetSessionPtr(), this.Query, 0, numResults, 0, 0, 0, 0, 0, 0, sp_search_type.SP_SEARCH_STANDARD, callbackPtr, IntPtr.Zero);
                        break;
                    case SearchType.Artist:
                        _browsePtr = libspotify.sp_search_create(Session.GetSessionPtr(), this.Query, 0, 0, 0, 0, 0, numResults, 0, 0, sp_search_type.SP_SEARCH_STANDARD, callbackPtr, IntPtr.Zero);
                        break;
                    case SearchType.Album:
                        _browsePtr = libspotify.sp_search_create(Session.GetSessionPtr(), this.Query, 0, 0, 0, numResults, 0, 0, 0, 0, sp_search_type.SP_SEARCH_STANDARD, callbackPtr, IntPtr.Zero);
                        break;
                    default:
                        throw new InvalidOperationException("No search type set");
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteDebug("Search.BeginBrowse() failed: {0}", ex.Message);
                return false;
            }
        }

        private void SearchCompleted(IntPtr searchPtr, IntPtr userDataPtr)
        {
            try
            {
                libspotify.sp_error error = libspotify.sp_search_error(searchPtr);
                if (error != libspotify.sp_error.OK)
                {
                    Logger.WriteDebug("Search browse failed: {0}", libspotify.sp_error_message(error));
                    this.IsLoaded = true;
                    return;
                }
                this.DidYouMean = Functions.PtrToString(libspotify.sp_search_did_you_mean(_browsePtr));
                if (this.Type == SearchType.Track)
                {
                    LoadTracks(); 
                }
                this.IsLoaded = true;
            }
            finally
            {
                safeReleaseSearch();
            }
        }

        private void LoadTracks()
        {
            int numtracks = libspotify.sp_search_num_tracks(_browsePtr);
            List<IntPtr> trackPtrs = new List<IntPtr>();
            for (int i = 0; i < numtracks; i++)
            {
                trackPtrs.Add(libspotify.sp_search_track(_browsePtr, i));
            }
            Tracks = trackPtrs;
        }

        #region IDisposable Members

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Search()
        {
            dispose(false);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && _searchReleased == false)
                {
                    safeReleaseSearch();
                }
                _disposed = true;
            }
        }
        #endregion

        private void safeReleaseSearch()
        {
            if (this._browsePtr != IntPtr.Zero)
            {
                try
                {
                    // release the search object
                    libspotify.sp_search_release(_browsePtr);
                    _searchReleased = true;
                }
                catch { }
            }
        }
    }

    public enum SearchType
    {
        Track,
        Artist,
        Album
    }
}