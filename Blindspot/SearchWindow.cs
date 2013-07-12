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
        public enum SearchType
        {
            Title,
            Artist,
            Album
        }
        
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
                MessageBox.Show("Please enter some search text to search for", "No search text", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            this.SearchText = searchTextBox.Text;
            if (titleButton.Checked)
            {
                this.Type = SearchType.Title; 
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
