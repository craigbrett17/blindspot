using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Blindspot.Core.Models;

namespace Blindspot
{
    public partial class ItemDetailsWindow : Form
    {
        private object item; // the item as an object

        public ItemDetailsWindow(object itemIn)
        {
            InitializeComponent();
            item = itemIn;
        }

        private void ItemDetailsWindow_Load(object sender, EventArgs e)
        {
            LoadCorrectWindowConfigurationForTypeOfItem();
        }

        private void LoadCorrectWindowConfigurationForTypeOfItem()
        {
            if (item is Album)
            {
                LoadAlbumDetails((Album)item);
            }
            else if (item is Artist)
            {
                LoadArtistDetails((Artist)item);
            }
            else if (item is Track)
            {
                LoadTrackDetails((Track)item);
            }
            else if (item is PlaylistContainer.PlaylistInfo)
            {
                LoadPlaylistDetails((PlaylistContainer.PlaylistInfo)item);
            }
            else
            {
                throw new NotImplementedException(String.Format("Details for items of type {0} not yet implemented", item.GetType()));
            }
        }
        
        private void LoadArtistDetails(Artist artist)
        {
            throw new NotImplementedException();
        }

        private void LoadAlbumDetails(Album album)
        {
            throw new NotImplementedException();
        }

        private void LoadTrackDetails(Track track)
        {
            trackInfoPanel.Visible = true;
            track_titleBox.Text = track.Name;
            track_artistsBox.Text = String.Join(", ", track.Artists);
            track_albumBox.Text = track.Album.Name;
        }

        private void LoadPlaylistDetails(PlaylistContainer.PlaylistInfo playlistInfo)
        {
            playlistInfoPanel.Visible = true;
            playlist_nameBox.Text = playlistInfo.Name;
            playlist_numTracksBox.Text = playlistInfo.NumberOfTracks.ToString();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}