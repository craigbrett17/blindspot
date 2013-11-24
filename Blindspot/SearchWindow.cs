using Blindspot.Core;
using Blindspot.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Blindspot
{
    public partial class SearchWindow : Form
    {
        public string SearchText { get; set; }
        public SearchType Type { get; set; }

        public SearchWindow()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text.Length == 0)
            {
                MessageBox.Show(StringStore.PleaseEnterSomeSearchTextToSearchFor, StringStore.NoSearchText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            this.SearchText = searchTextBox.Text;
            if (titleButton.Checked)
            {
                this.Type = SearchType.Track; 
            }
            else if (artistButton.Checked)
            {
                this.Type = SearchType.Artist;
            }
            else if (albumButton.Checked)
            {
                this.Type = SearchType.Album;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
