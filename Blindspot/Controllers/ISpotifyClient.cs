using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blindspot.Controllers
{
    public interface ISpotifyClient
    {
        bool IsRunning { get; }
        bool Login(byte[] appkey, string username, string password);
        void Initialise();
        void ShutDown();
        int GetUserCountry();
        List<PlaylistContainer.PlaylistInfo> GetAllSessionPlaylists();
        List<PlaylistContainer.PlaylistInfo> GetPlaylists(PlaylistContainer.PlaylistInfo playlist);
        Playlist GetPlaylist(IntPtr playlistPtr, bool needTracks);
        IntPtr[] GetAlbumTracks(IntPtr albumPtr);
        IntPtr[] GetArtistAlbums(IntPtr artistPtr);
        PlaylistContainer GetUserPlaylists(IntPtr userPtr);
        IntPtr GetUserCanonicalNamePtr(IntPtr userPtr);
        string GetUserDisplayName(IntPtr userPtr);
        void Session_OnLoggedIn(IntPtr obj);
        void Session_OnNotifyMainThread(IntPtr sessionPtr);
        IntPtr LastSearch { get; }
        void SetPrivateSession(bool enable);
    }
}