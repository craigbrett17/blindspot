using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libspotifydotnet;
using System.Runtime.InteropServices;

namespace Blindspot.Controllers
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
        { }

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

        public IntPtr LastSearch { get; set; }

        public List<Track> SearchTracks(string query)
        {
            throw new NotImplementedException();
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

        private IntPtr GetSession()
        {
            var session = Session.GetSessionPtr();
            if (session == IntPtr.Zero)
            {
                throw new InvalidOperationException("No session is currently active");
            }
            return session;
        }
    }
}