﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Blindspot.Core;
using Blindspot.Core.Models;
using Blindspot.Helpers;
using Blindspot.ViewModels;

namespace Blindspot.Commands
{
    public class NewSearchCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private OutputManager _output;

        public NewSearchCommand(BufferListCollection buffersIn)
        {
            buffers = buffersIn;
            _output = OutputManager.Instance;
        }

        public override string Key
        {
            get { return "new_search"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            SearchWindow searchDialog = new SearchWindow();
            searchDialog.ShowDialog();
            if (searchDialog.DialogResult != DialogResult.OK)
            {
                return; // cancelled search
            }
            string searchText = searchDialog.SearchText;
            var searchType = searchDialog.Type;
            searchDialog.Dispose();
            if (searchType == SearchType.Track)
            {
                _output.OutputMessage(StringStore.Searching, false);
                var spotify = SpotifyClient.Instance;
                var search = spotify.SearchTracks(searchText);
                buffers.Add(new SearchBufferList(search));
                buffers.CurrentListIndex = buffers.Count - 1;
                var searchBuffer = buffers.CurrentList;
                _output.OutputMessage(searchBuffer.ToString(), false);
                var tracks = search.Tracks;
                if (tracks == null || tracks.Count == 0)
                {
                    if (search != null && !String.IsNullOrEmpty(search.DidYouMean))
                    {
                        searchBuffer.Add(new BufferItem("No search results. Did you mean: " + search.DidYouMean));
                    }
                    else
                    {
                        searchBuffer.Add(new BufferItem(StringStore.NoSearchResults));
                    }
                }
                else
                {
                    _output.OutputMessage(tracks.Count + " " + StringStore.SearchResults, false);
                    foreach (IntPtr pointer in tracks)
                    {
                        searchBuffer.Add(new TrackBufferItem(new Track(pointer)));
                    }
                }
            }
            else if (searchType == SearchType.Artist)
            {
                MessageBox.Show("Not implemented yet! Boo to the developers!", StringStore.Oops, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (searchType == SearchType.Album)
            {
                MessageBox.Show("Not implemented yet! Boo to the developers!", StringStore.Oops, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
