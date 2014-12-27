using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Blindspot.Core;
using Blindspot.Helpers;
using Blindspot.ViewModels;

namespace Blindspot.Commands
{
    public class RemoveFromPlaylistCommand : HotkeyCommandBase
    {
        private BufferListCollection _buffers;
        private ISpotifyClient spotify = SpotifyClient.Instance;
        private IOutputManager output = OutputManager.Instance;

        public RemoveFromPlaylistCommand(BufferListCollection buffers)
        {
            _buffers = buffers;
        }

        public override string Key
        {
            get { return "remove_from_playlist"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var trackItem = _buffers.CurrentList.CurrentItem as TrackBufferItem;
            var playlistList = _buffers.CurrentList as PlaylistBufferList;
            if (trackItem == null || playlistList == null) return;
            var trackIndex = playlistList.CurrentItemIndex;
            
            var response = spotify.RemoveTrackFromPlaylist(trackIndex, playlistList.Model.Pointer);
            if (!response.IsError)
            {
                output.OutputMessage(StringStore.TrackRemoved);
            }
            else
            {
                MessageBox.Show(response.Message, StringStore.UnableToRemoveTrack, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}