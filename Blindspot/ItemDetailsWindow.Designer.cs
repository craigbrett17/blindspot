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
            this.trackInfoPanel.AutoScroll = true;
            this.trackInfoPanel.Controls.Add(this.track_albumBox);
            this.trackInfoPanel.Controls.Add(this.track_albumLabel);
            this.trackInfoPanel.Controls.Add(this.track_artistsBox);
            this.trackInfoPanel.Controls.Add(this.track_artistsLabel);
            this.trackInfoPanel.Controls.Add(this.track_titleBox);
            this.trackInfoPanel.Controls.Add(this.track_titleLabel);
            this.trackInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackInfoPanel.Location = new System.Drawing.Point(0, 0);
            this.trackInfoPanel.Name = "trackInfoPanel";
            this.trackInfoPanel.Size = new System.Drawing.Size(512, 270);
            this.trackInfoPanel.TabIndex = 0;
            this.trackInfoPanel.Visible = false;
            // 
            // track_albumBox
            // 
            this.track_albumBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.track_albumBox.Location = new System.Drawing.Point(200, 90);
            this.track_albumBox.Name = "track_albumBox";
            this.track_albumBox.ReadOnly = true;
            this.track_albumBox.Size = new System.Drawing.Size(300, 16);
            this.track_albumBox.TabIndex = 7;
            // 
            // track_albumLabel
            // 
            this.track_albumLabel.AutoSize = true;
            this.track_albumLabel.Location = new System.Drawing.Point(10, 90);
            this.track_albumLabel.Name = "track_albumLabel";
            this.track_albumLabel.Size = new System.Drawing.Size(51, 17);
            this.track_albumLabel.TabIndex = 6;
            this.track_albumLabel.Text = "A&lbum:";
            // 
            // track_artistsBox
            // 
            this.track_artistsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.track_artistsBox.Location = new System.Drawing.Point(200, 50);
            this.track_artistsBox.Name = "track_artistsBox";
            this.track_artistsBox.ReadOnly = true;
            this.track_artistsBox.Size = new System.Drawing.Size(300, 16);
            this.track_artistsBox.TabIndex = 5;
            // 
            // track_artistsLabel
            // 
            this.track_artistsLabel.AutoSize = true;
            this.track_artistsLabel.Location = new System.Drawing.Point(10, 50);
            this.track_artistsLabel.Name = "track_artistsLabel";
            this.track_artistsLabel.Size = new System.Drawing.Size(51, 17);
            this.track_artistsLabel.TabIndex = 4;
            this.track_artistsLabel.Text = "&Artists:";
            // 
            // track_titleBox
            // 
            this.track_titleBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.track_titleBox.Location = new System.Drawing.Point(200, 10);
            this.track_titleBox.Name = "track_titleBox";
            this.track_titleBox.ReadOnly = true;
            this.track_titleBox.Size = new System.Drawing.Size(300, 16);
            this.track_titleBox.TabIndex = 3;
            // 
            // track_titleLabel
            // 
            this.track_titleLabel.AutoSize = true;
            this.track_titleLabel.Location = new System.Drawing.Point(10, 10);
            this.track_titleLabel.Name = "track_titleLabel";
            this.track_titleLabel.Size = new System.Drawing.Size(39, 17);
            this.track_titleLabel.TabIndex = 2;
            this.track_titleLabel.Text = "&Title:";
            // 
            // artistInfoPanel
            // 
            this.artistInfoPanel.AutoScroll = true;
            this.artistInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.artistInfoPanel.Location = new System.Drawing.Point(0, 270);
            this.artistInfoPanel.Name = "artistInfoPanel";
            this.artistInfoPanel.Size = new System.Drawing.Size(512, 270);
            this.artistInfoPanel.TabIndex = 2;
            this.artistInfoPanel.Visible = false;
            // 
            // albumInfoPanel
            // 
            this.albumInfoPanel.AutoScroll = true;
            this.albumInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.albumInfoPanel.Location = new System.Drawing.Point(0, 540);
            this.albumInfoPanel.Name = "albumInfoPanel";
            this.albumInfoPanel.Size = new System.Drawing.Size(512, 270);
            this.albumInfoPanel.TabIndex = 3;
            this.albumInfoPanel.Visible = false;
            // 
            // playlistInfoPanel
            // 
            this.playlistInfoPanel.AutoScroll = true;
            this.playlistInfoPanel.Controls.Add(this.playlist_numTracksBox);
            this.playlistInfoPanel.Controls.Add(this.playlist_numTracksLabel);
            this.playlistInfoPanel.Controls.Add(this.playlist_nameBox);
            this.playlistInfoPanel.Controls.Add(this.playlist_nameLabel);
            this.playlistInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.playlistInfoPanel.Location = new System.Drawing.Point(0, 810);
            this.playlistInfoPanel.Name = "playlistInfoPanel";
            this.playlistInfoPanel.Size = new System.Drawing.Size(512, 270);
            this.playlistInfoPanel.TabIndex = 4;
            this.playlistInfoPanel.Visible = false;
            // 
            // playlist_numTracksBox
            // 
            this.playlist_numTracksBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playlist_numTracksBox.Location = new System.Drawing.Point(200, 50);
            this.playlist_numTracksBox.Name = "playlist_numTracksBox";
            this.playlist_numTracksBox.ReadOnly = true;
            this.playlist_numTracksBox.Size = new System.Drawing.Size(300, 16);
            this.playlist_numTracksBox.TabIndex = 8;
            // 
            // playlist_numTracksLabel
            // 
            this.playlist_numTracksLabel.AutoSize = true;
            this.playlist_numTracksLabel.Location = new System.Drawing.Point(10, 50);
            this.playlist_numTracksLabel.Name = "playlist_numTracksLabel";
            this.playlist_numTracksLabel.Size = new System.Drawing.Size(120, 17);
            this.playlist_numTracksLabel.TabIndex = 7;
            this.playlist_numTracksLabel.Text = "Number of &tracks:";
            // 
            // playlist_nameBox
            // 
            this.playlist_nameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playlist_nameBox.Location = new System.Drawing.Point(200, 10);
            this.playlist_nameBox.Name = "playlist_nameBox";
            this.playlist_nameBox.ReadOnly = true;
            this.playlist_nameBox.Size = new System.Drawing.Size(300, 16);
            this.playlist_nameBox.TabIndex = 6;
            // 
            // playlist_nameLabel
            // 
            this.playlist_nameLabel.AutoSize = true;
            this.playlist_nameLabel.Location = new System.Drawing.Point(10, 10);
            this.playlist_nameLabel.Name = "playlist_nameLabel";
            this.playlist_nameLabel.Size = new System.Drawing.Size(49, 17);
            this.playlist_nameLabel.TabIndex = 5;
            this.playlist_nameLabel.Text = "&Name:";
            // 
            // userInfoPanel
            // 
            this.userInfoPanel.AutoScroll = true;
            this.userInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.userInfoPanel.Location = new System.Drawing.Point(0, 1080);
            this.userInfoPanel.Name = "userInfoPanel";
            this.userInfoPanel.Size = new System.Drawing.Size(512, 270);
            this.userInfoPanel.TabIndex = 5;
            this.userInfoPanel.Visible = false;
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(220, 285);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Cl&ose";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // ItemDetailsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 321);
            this.ControlBox = false;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.userInfoPanel);
            this.Controls.Add(this.playlistInfoPanel);
            this.Controls.Add(this.albumInfoPanel);
            this.Controls.Add(this.artistInfoPanel);
            this.Controls.Add(this.trackInfoPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemDetailsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item Details";
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