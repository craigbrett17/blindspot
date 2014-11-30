using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libspotifydotnet;
using System.Runtime.InteropServices;
using System.Threading;
using Blindspot.Core.Models;

namespace Blindspot.Core
{
    /// <summary>
    /// A singleton client to handle interaction between Spotify and the application
    /// </summary>
    /// <remarks>
    /// This should eventually replace the SpotifyController class borrowed from the Jamcast Spotify plugin
    /// </remarks>
    public class SpotifyClient : ISpotifyClient
    {
        private SpotifyClient()
        {
            this.RequestTimeout = 15;
        }

        private static SpotifyClient _instance;
        public static SpotifyClient Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpotifyClient();
                }
                return _instance;
            }
        }

        public int RequestTimeout { get; set; }
        public Search LastSearch { get; set; }

        public Search Search(string query, SearchType searchType)
        {
            Search search = new Search(query, searchType);
            return GetMoreResultsFromSearch(search);
        }

        public Search GetMoreResultsFromSearch(Search search)
        {
            search.BeginBrowse();
            bool loadedInTime = WaitFor(() => search.IsLoaded, RequestTimeout);
            if (!loadedInTime)
            {
                Logger.WriteDebug("Browsing search results timed out");
                return null;
            }
            return search;
        }
        
        public bool IsRunning
        {
            get { throw new NotImplementedException(); }
        }

        public bool Login(byte[] appkey, string username, string password)
        {
            throw new NotImplementedException();
        }

        public void Initialise()
        {
            throw new NotImplementedException();
        }

        public void ShutDown()
        {
            throw new NotImplementedException();
        }

        public int GetUserCountry()
        {
            throw new NotImplementedException();
        }

        public List<PlaylistContainer.PlaylistInfo> GetAllSessionPlaylists()
        {
            throw new NotImplementedException();
        }

        public List<PlaylistContainer.PlaylistInfo> GetPlaylists(PlaylistContainer.PlaylistInfo playlist)
        {
            throw new NotImplementedException();
        }

        public Playlist GetPlaylist(IntPtr playlistPtr, bool needTracks)
        {
            throw new NotImplementedException();
        }

        public IntPtr[] GetAlbumTracks(IntPtr albumPtr)
        {
            throw new NotImplementedException();
        }

        public IntPtr[] GetArtistAlbums(IntPtr artistPtr)
        {
            throw new NotImplementedException();
        }

        public PlaylistContainer GetUserPlaylists(IntPtr userPtr)
        {
            throw new NotImplementedException();
        }

        public IntPtr GetUserCanonicalNamePtr(IntPtr userPtr)
        {
            throw new NotImplementedException();
        }

        public string GetUserDisplayName(IntPtr userPtr)
        {
            throw new NotImplementedException();
        }

        public void Session_OnLoggedIn(IntPtr obj)
        {
            throw new NotImplementedException();
        }

        public void Session_OnNotifyMainThread(IntPtr sessionPtr)
        {
            throw new NotImplementedException();
        }

        public void SetPrivateSession(bool enable)
        {
            var session = GetSession();
            libspotify.sp_session_set_private_session(session, enable);
        }

        public SpotifyError GetLoginError()
        {
            return (SpotifyError)Session.LoginError;
        }

        private IntPtr GetSession()
        {
            var session = Session.GetSessionPtr();
            if (session == IntPtr.Zero)
            {
                throw new InvalidOperationException("No session is currently active");
            }
            return session;
        }

        // ok, I haven't been able to think of a better way of doing this yet, well done Jamcast
        private static bool WaitFor(Func<bool> t, int timeout)
        {
            DateTime start = DateTime.Now;
            while (DateTime.Now.Subtract(start).Seconds < timeout)
            {
                if (t.Invoke())
                {
                    return true;
                }
                Thread.Sleep(10);
            }
            return false;
        }
        
    }
}