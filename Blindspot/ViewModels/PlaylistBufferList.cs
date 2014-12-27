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
            var trackPointers = Functions.GetPointerArrayFromPointer(tracksPtr, num_tracks);
            var tracks = trackPointers.Select(trackPointer => new Track(trackPointer));
            var trackItems = tracks.Select(track => new TrackBufferItem(track));
            
            this.InsertRange(position, trackItems);
        }

        private void TrackRemovedFromPlaylist(IntPtr playlistPtr, IntPtr tracksPtr, int num_tracks, IntPtr userDataPtr)
        {
            var trackIndexes = Functions.GetIntArrayFromPointer(tracksPtr, num_tracks);
            // have this list in reverse order, so we can remove without effecting other track indexes
            // in this program we're probably removing one at a time, but helps for interactions from outside to be able to handle more
            trackIndexes = trackIndexes.OrderByDescending(t => t).ToArray();
            foreach (int index in trackIndexes)
            {
                // decide what to do about removing these tracks from the Model, too
                this.RemoveAt(index);
            }
            this.CurrentItemIndex = (trackIndexes.Min() > this.Count - 1)
                ? this.Count - 1
                : trackIndexes.Min();
        }

        public void Dispose()
        {
            if (Model != null)
                Model.Dispose();
        }
    }
}