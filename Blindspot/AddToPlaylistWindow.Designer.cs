namespace Blindspot
{
    partial class AddToPlaylistWindow
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
            this.newOrExistingBox = new System.Windows.Forms.GroupBox();
            this.existingPlaylistBox = new System.Windows.Forms.RadioButton();
            this.newPlaylistBox = new System.Windows.Forms.RadioButton();
            this.newPlaylistLabel = new System.Windows.Forms.Label();
            this.newPlaylistTextbox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.existingPlaylistsDropdownLabel = new System.Windows.Forms.Label();
            this.existingPlaylistsDropdownBox = new System.Windows.Forms.ComboBox();
            this.newOrExistingBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // newOrExistingBox
            // 
            this.newOrExistingBox.Controls.Add(this.existingPlaylistBox);
            this.newOrExistingBox.Controls.Add(this.newPlaylistBox);
            this.newOrExistingBox.Location = new System.Drawing.Point(0, 0);
            this.newOrExistingBox.Name = "newOrExistingBox";
            this.newOrExistingBox.Size = new System.Drawing.Size(330, 50);
            this.newOrExistingBox.TabIndex = 0;
            this.newOrExistingBox.TabStop = false;
            // 
            // existingPlaylistBox
            // 
            this.existingPlaylistBox.AutoSize = true;
            this.existingPlaylistBox.Location = new System.Drawing.Point(125, 18);
            this.existingPlaylistBox.Name = "existingPlaylistBox";
            this.existingPlaylistBox.Size = new System.Drawing.Size(140, 24);
            this.existingPlaylistBox.TabIndex = 1;
            this.existingPlaylistBox.Text = "&Existing playlist";
            this.existingPlaylistBox.UseVisualStyleBackColor = true;
            this.existingPlaylistBox.CheckedChanged += new System.EventHandler(this.existingPlaylistBox_CheckedChanged);
            // 
            // newPlaylistBox
            // 
            this.newPlaylistBox.AutoSize = true;
            this.newPlaylistBox.Checked = true;
            this.newPlaylistBox.Location = new System.Drawing.Point(3, 18);
            this.newPlaylistBox.Name = "newPlaylistBox";
            this.newPlaylistBox.Size = new System.Drawing.Size(116, 24);
            this.newPlaylistBox.TabIndex = 0;
            this.newPlaylistBox.TabStop = true;
            this.newPlaylistBox.Text = "&New playlist";
            this.newPlaylistBox.UseVisualStyleBackColor = true;
            this.newPlaylistBox.CheckedChanged += new System.EventHandler(this.newPlaylistBox_CheckedChanged);
            // 
            // newPlaylistLabel
            // 
            this.newPlaylistLabel.AutoSize = true;
            this.newPlaylistLabel.Location = new System.Drawing.Point(10, 190);
            this.newPlaylistLabel.Name = "newPlaylistLabel";
            this.newPlaylistLabel.Size = new System.Drawing.Size(136, 20);
            this.newPlaylistLabel.TabIndex = 4;
            this.newPlaylistLabel.Text = "New Playlist n&ame";
            // 
            // newPlaylistTextbox
            // 
            this.newPlaylistTextbox.Location = new System.Drawing.Point(10, 225);
            this.newPlaylistTextbox.Name = "newPlaylistTextbox";
            this.newPlaylistTextbox.Size = new System.Drawing.Size(250, 26);
            this.newPlaylistTextbox.TabIndex = 5;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(50, 290);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 6;
            this.addButton.Text = "&Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(130, 290);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // existingPlaylistsDropdownLabel
            // 
            this.existingPlaylistsDropdownLabel.AutoSize = true;
            this.existingPlaylistsDropdownLabel.Location = new System.Drawing.Point(10, 70);
            this.existingPlaylistsDropdownLabel.Name = "existingPlaylistsDropdownLabel";
            this.existingPlaylistsDropdownLabel.Size = new System.Drawing.Size(65, 20);
            this.existingPlaylistsDropdownLabel.TabIndex = 2;
            this.existingPlaylistsDropdownLabel.Text = "Playlists";
            // 
            // existingPlaylistsDropdownBox
            // 
            this.existingPlaylistsDropdownBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.existingPlaylistsDropdownBox.DropDownWidth = 350;
            this.existingPlaylistsDropdownBox.Enabled = false;
            this.existingPlaylistsDropdownBox.FormattingEnabled = true;
            this.existingPlaylistsDropdownBox.Location = new System.Drawing.Point(10, 100);
            this.existingPlaylistsDropdownBox.Name = "existingPlaylistsDropdownBox";
            this.existingPlaylistsDropdownBox.Size = new System.Drawing.Size(250, 28);
            this.existingPlaylistsDropdownBox.TabIndex = 3;
            // 
            // AddToPlaylistWindow
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(328, 345);
            this.Controls.Add(this.existingPlaylistsDropdownBox);
            this.Controls.Add(this.existingPlaylistsDropdownLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.newPlaylistTextbox);
            this.Controls.Add(this.newPlaylistLabel);
            this.Controls.Add(this.newOrExistingBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddToPlaylistWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add to playlist";
            this.Load += new System.EventHandler(this.AddToPlaylistWindow_Load);
            this.newOrExistingBox.ResumeLayout(false);
            this.newOrExistingBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox newOrExistingBox;
        private System.Windows.Forms.RadioButton existingPlaylistBox;
        private System.Windows.Forms.RadioButton newPlaylistBox;
        private System.Windows.Forms.Label newPlaylistLabel;
        private System.Windows.Forms.TextBox newPlaylistTextbox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label existingPlaylistsDropdownLabel;
        private System.Windows.Forms.ComboBox existingPlaylistsDropdownBox;
    }
}