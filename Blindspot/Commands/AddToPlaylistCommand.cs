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
    public class AddToPlaylistCommand : HotkeyCommandBase
    {
        private BufferListCollection _buffers;
        private ISpotifyClient spotify = SpotifyClient.Instance;
        private IOutputManager output = OutputManager.Instance;

        public AddToPlaylistCommand(BufferListCollection buffers)
        {
            _buffers = buffers;
        }

        public override string Key
        {
            get { return "add_to_playlist"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var trackItem = _buffers.CurrentList.CurrentItem as TrackBufferItem;
            if (trackItem == null) return;

            var dialog = new AddToPlaylistWindow();
            dialog.ShowDialog();
            if (dialog.DialogResult != DialogResult.OK)
                return;

            IntPtr playlistPointer = IntPtr.Zero;
            if (dialog.ShouldAddNewPlaylist)
            {
                var newPlaylistPointer = spotify.CreateNewPlaylist(dialog.NewPlaylistName);
                if (newPlaylistPointer == IntPtr.Zero)
                {
                    MessageBox.Show(StringStore.PlaylistNotCreated, StringStore.UnexpectedErrorOccurred, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                playlistPointer = newPlaylistPointer;
            }
            else
            {
                playlistPointer = dialog.ExistingPlaylistPointer;
            }

            var response = spotify.AddTrackToPlaylist(trackItem.Model.TrackPtr, playlistPointer);
            if (!response.IsError)
            {
                output.OutputMessageWithDelay(StringStore.TrackAdded, 1000);
            }
            else
            {
                MessageBox.Show(response.Message, StringStore.UnableToAddTrackToPlaylist, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}