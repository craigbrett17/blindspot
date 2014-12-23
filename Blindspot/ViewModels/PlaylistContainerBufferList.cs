using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blindspot.Core;
using Blindspot.Core.Models;

namespace Blindspot.ViewModels
{
    public class PlaylistContainerBufferList : BufferList, IDisposable
    {
        private PlaylistContainer _model;
        public PlaylistContainer Model
        {
            get { return _model; }
            set
            {
                _model = value;
                SetupEvents();
            }
        }

        private ISpotifyClient spotify = SpotifyClient.Instance;
        
        public PlaylistContainerBufferList(string nameIn)
        {
            this.Name = nameIn;
        }

        public PlaylistContainerBufferList(string nameIn, PlaylistContainer playlistContainerIn)
                : this(nameIn)
        {
            this.Model = playlistContainerIn;
            SetupEvents();
        }

        public PlaylistContainerBufferList(string nameIn, bool isDisposable)
            : base(nameIn, isDisposable)
        { }

        private void SetupEvents()
        {
            this.Model.OnPlaylistAdded += new PlaylistContainer.playlist_added_delegate(PlaylistAddedToContainer);
            this.Model.OnPlaylistRemoved += new PlaylistContainer.playlist_removed_delegate(PlaylistRemovedFromContainer);
        }

        // look into this later, check that this event doesn't fire every time a playlist is added to any container
        // check API docs, it holds the key to many secrets
        private void PlaylistAddedToContainer(IntPtr containerPtr, IntPtr playlistPtr, int position, IntPtr userDataPtr)
        {
            this.Insert(position, new PlaylistBufferItem(new PlaylistContainer.PlaylistInfo(playlistPtr)));
        }

        private void PlaylistRemovedFromContainer(IntPtr containerPtr, IntPtr playlistPtr, int position, IntPtr userDataPtr)
        {
            if (this.CurrentItemIndex == position)
                this.CurrentItemIndex = 0;
            this.RemoveAt(position);
        }

        public void Dispose()
        {
            if (Model != null)
                Model.Dispose();
        }
    }
}