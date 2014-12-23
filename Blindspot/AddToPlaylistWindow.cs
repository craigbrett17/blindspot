using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Blindspot.Core;

namespace Blindspot
{
    public partial class AddToPlaylistWindow : Form
    {
        private ISpotifyClient spotify = SpotifyClient.Instance;
        public bool ShouldAddNewPlaylist { get { return newPlaylistBox.Checked; } }
        public string NewPlaylistName { get { return newPlaylistTextbox.Text; } }
        public IntPtr ExistingPlaylistPointer { get { return (IntPtr)existingPlaylistsDropdownBox.SelectedValue; } }

        public AddToPlaylistWindow()
        {
            InitializeComponent();
        }

        private void AddToPlaylistWindow_Load(object sender, EventArgs e)
        {
            var playlists = spotify.GetAllSessionPlaylists()
                .Where(p => p.UserCanContribute).ToArray();

            existingPlaylistsDropdownBox.ValueMember = "Pointer";
            existingPlaylistsDropdownBox.DisplayMember = "Name";
            existingPlaylistsDropdownBox.DataSource = playlists;
            existingPlaylistsDropdownBox.SelectedIndex = 0;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (ShouldAddNewPlaylist && newPlaylistTextbox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a name for your new playlist", "Invalid playlist name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void newPlaylistBox_CheckedChanged(object sender, EventArgs e)
        {
            existingPlaylistsDropdownBox.Enabled = false;
            newPlaylistTextbox.Enabled = true;
        }

        private void existingPlaylistBox_CheckedChanged(object sender, EventArgs e)
        {
            existingPlaylistsDropdownBox.Enabled = true;
            newPlaylistTextbox.Enabled = false;
        }
    }
}