namespace Blindspot
{
    partial class ItemDetailsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemDetailsWindow));
            this.trackInfoPanel = new System.Windows.Forms.Panel();
            this.track_albumBox = new System.Windows.Forms.TextBox();
            this.track_albumLabel = new System.Windows.Forms.Label();
            this.track_artistsBox = new System.Windows.Forms.TextBox();
            this.track_artistsLabel = new System.Windows.Forms.Label();
            this.track_titleBox = new System.Windows.Forms.TextBox();
            this.track_titleLabel = new System.Windows.Forms.Label();
            this.artistInfoPanel = new System.Windows.Forms.Panel();
            this.albumInfoPanel = new System.Windows.Forms.Panel();
            this.playlistInfoPanel = new System.Windows.Forms.Panel();
            this.playlist_numTracksBox = new System.Windows.Forms.TextBox();
            this.playlist_numTracksLabel = new System.Windows.Forms.Label();
            this.playlist_nameBox = new System.Windows.Forms.TextBox();
            this.playlist_nameLabel = new System.Windows.Forms.Label();
            this.userInfoPanel = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.trackInfoPanel.SuspendLayout();
            this.playlistInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackInfoPanel
            // 
            resources.ApplyResources(this.trackInfoPanel, "trackInfoPanel");
            this.trackInfoPanel.Controls.Add(this.track_albumBox);
            this.trackInfoPanel.Controls.Add(this.track_albumLabel);
            this.trackInfoPanel.Controls.Add(this.track_artistsBox);
            this.trackInfoPanel.Controls.Add(this.track_artistsLabel);
            this.trackInfoPanel.Controls.Add(this.track_titleBox);
            this.trackInfoPanel.Controls.Add(this.track_titleLabel);
            this.trackInfoPanel.Name = "trackInfoPanel";
            // 
            // track_albumBox
            // 
            resources.ApplyResources(this.track_albumBox, "track_albumBox");
            this.track_albumBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.track_albumBox.Name = "track_albumBox";
            this.track_albumBox.ReadOnly = true;
            // 
            // track_albumLabel
            // 
            resources.ApplyResources(this.track_albumLabel, "track_albumLabel");
            this.track_albumLabel.Name = "track_albumLabel";
            // 
            // track_artistsBox
            // 
            resources.ApplyResources(this.track_artistsBox, "track_artistsBox");
            this.track_artistsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.track_artistsBox.Name = "track_artistsBox";
            this.track_artistsBox.ReadOnly = true;
            // 
            // track_artistsLabel
            // 
            resources.ApplyResources(this.track_artistsLabel, "track_artistsLabel");
            this.track_artistsLabel.Name = "track_artistsLabel";
            // 
            // track_titleBox
            // 
            resources.ApplyResources(this.track_titleBox, "track_titleBox");
            this.track_titleBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.track_titleBox.Name = "track_titleBox";
            this.track_titleBox.ReadOnly = true;
            // 
            // track_titleLabel
            // 
            resources.ApplyResources(this.track_titleLabel, "track_titleLabel");
            this.track_titleLabel.Name = "track_titleLabel";
            // 
            // artistInfoPanel
            // 
            resources.ApplyResources(this.artistInfoPanel, "artistInfoPanel");
            this.artistInfoPanel.Name = "artistInfoPanel";
            // 
            // albumInfoPanel
            // 
            resources.ApplyResources(this.albumInfoPanel, "albumInfoPanel");
            this.albumInfoPanel.Name = "albumInfoPanel";
            // 
            // playlistInfoPanel
            // 
            resources.ApplyResources(this.playlistInfoPanel, "playlistInfoPanel");
            this.playlistInfoPanel.Controls.Add(this.playlist_numTracksBox);
            this.playlistInfoPanel.Controls.Add(this.playlist_numTracksLabel);
            this.playlistInfoPanel.Controls.Add(this.playlist_nameBox);
            this.playlistInfoPanel.Controls.Add(this.playlist_nameLabel);
            this.playlistInfoPanel.Name = "playlistInfoPanel";
            // 
            // playlist_numTracksBox
            // 
            resources.ApplyResources(this.playlist_numTracksBox, "playlist_numTracksBox");
            this.playlist_numTracksBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playlist_numTracksBox.Name = "playlist_numTracksBox";
            this.playlist_numTracksBox.ReadOnly = true;
            // 
            // playlist_numTracksLabel
            // 
            resources.ApplyResources(this.playlist_numTracksLabel, "playlist_numTracksLabel");
            this.playlist_numTracksLabel.Name = "playlist_numTracksLabel";
            // 
            // playlist_nameBox
            // 
            resources.ApplyResources(this.playlist_nameBox, "playlist_nameBox");
            this.playlist_nameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playlist_nameBox.Name = "playlist_nameBox";
            this.playlist_nameBox.ReadOnly = true;
            // 
            // playlist_nameLabel
            // 
            resources.ApplyResources(this.playlist_nameLabel, "playlist_nameLabel");
            this.playlist_nameLabel.Name = "playlist_nameLabel";
            // 
            // userInfoPanel
            // 
            resources.ApplyResources(this.userInfoPanel, "userInfoPanel");
            this.userInfoPanel.Name = "userInfoPanel";
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // ItemDetailsWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ControlBox = false;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.userInfoPanel);
            this.Controls.Add(this.playlistInfoPanel);
            this.Controls.Add(this.albumInfoPanel);
            this.Controls.Add(this.artistInfoPanel);
            this.Controls.Add(this.trackInfoPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemDetailsWindow";
            this.Load += new System.EventHandler(this.ItemDetailsWindow_Load);
            this.trackInfoPanel.ResumeLayout(false);
            this.trackInfoPanel.PerformLayout();
            this.playlistInfoPanel.ResumeLayout(false);
            this.playlistInfoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel trackInfoPanel;
        private System.Windows.Forms.TextBox track_albumBox;
        private System.Windows.Forms.Label track_albumLabel;
        private System.Windows.Forms.TextBox track_artistsBox;
        private System.Windows.Forms.Label track_artistsLabel;
        private System.Windows.Forms.TextBox track_titleBox;
        private System.Windows.Forms.Label track_titleLabel;
        private System.Windows.Forms.Panel artistInfoPanel;
        private System.Windows.Forms.Panel albumInfoPanel;
        private System.Windows.Forms.Panel playlistInfoPanel;
        private System.Windows.Forms.TextBox playlist_numTracksBox;
        private System.Windows.Forms.Label playlist_numTracksLabel;
        private System.Windows.Forms.TextBox playlist_nameBox;
        private System.Windows.Forms.Label playlist_nameLabel;
        private System.Windows.Forms.Panel userInfoPanel;
        private System.Windows.Forms.Button closeButton;
    }
}