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

            if (dialog.ShouldAddNewPlaylist)
            {
                
            }
            else
            {
                var response = spotify.AddTrackToPlaylist(trackItem.Model.TrackPtr, dialog.ExistingPlaylistPointer);
                if (!response.IsError)
                {
                    output.OutputMessage("Track added");
                }
                else
                {
                    MessageBox.Show(response.Message, "Unable to add track to playlist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}