using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blindspot.Core;
using Blindspot.Core.Models;

namespace Blindspot.ViewModels
{
    public class PlaylistBufferList : BufferList, IDisposable
    {
        public Playlist Model { get; set; }
        private ISpotifyClient spotify = SpotifyClient.Instance;
        
        public PlaylistBufferList(string nameIn)
        {
            this.Name = nameIn;
        }

        public PlaylistBufferList(Playlist playlistIn)
        {
            this.Name = playlistIn.Name;
            this.Model = playlistIn;
            SetupEvents();
        }

        private void SetupEvents()
        {
            this.Model.OnTrackAdded += new Playlist.tracks_added_delegate(TrackAddedToPlaylist);
            this.Model.OnTrackRemoved += new Playlist.tracks_removed_delegate(TrackRemovedFromPlaylist);
        }

        private void TrackAddedToPlaylist(IntPtr playlistPtr, IntPtr tracksPtr, int num_tracks, int position, IntPtr userDataPtr)
        {
            // not sure where to put the function to get the pointers[] from the pointer, alternative suggestions welcome
            var trackPointers = spotify.GetTrackPointersFromPtrToPtrArray(tracksPtr, num_tracks);
            var tracks = trackPointers.Select(trackPointer => new Track(trackPointer));
            var trackItems = tracks.Select(track => new TrackBufferItem(track));
            
            this.InsertRange(position, trackItems);
        }

        private void TrackRemovedFromPlaylist(IntPtr playlistPtr, IntPtr tracksPtr, int num_tracks, IntPtr userDataPtr)
        {
            var trackPointers = spotify.GetTrackPointersFromPtrToPtrArray(tracksPtr, num_tracks);
            // remove all tracks that have the given pointers
            this.RemoveAll(item =>
            {
                var tbi = item as TrackBufferItem;
                if (tbi == null) return false; // it's not a match as it's not even a track

                return trackPointers.Any(pointer => pointer == tbi.Model.TrackPtr);
            });
        }

        public void Dispose()
        {
            if (Model != null)
                Model.Dispose();
        }
    }
}