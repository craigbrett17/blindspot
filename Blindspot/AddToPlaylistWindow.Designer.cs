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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddToPlaylistWindow));
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
            resources.ApplyResources(this.newOrExistingBox, "newOrExistingBox");
            this.newOrExistingBox.Name = "newOrExistingBox";
            this.newOrExistingBox.TabStop = false;
            // 
            // existingPlaylistBox
            // 
            resources.ApplyResources(this.existingPlaylistBox, "existingPlaylistBox");
            this.existingPlaylistBox.Name = "existingPlaylistBox";
            this.existingPlaylistBox.UseVisualStyleBackColor = true;
            this.existingPlaylistBox.CheckedChanged += new System.EventHandler(this.existingPlaylistBox_CheckedChanged);
            // 
            // newPlaylistBox
            // 
            resources.ApplyResources(this.newPlaylistBox, "newPlaylistBox");
            this.newPlaylistBox.Checked = true;
            this.newPlaylistBox.Name = "newPlaylistBox";
            this.newPlaylistBox.TabStop = true;
            this.newPlaylistBox.UseVisualStyleBackColor = true;
            this.newPlaylistBox.CheckedChanged += new System.EventHandler(this.newPlaylistBox_CheckedChanged);
            // 
            // newPlaylistLabel
            // 
            resources.ApplyResources(this.newPlaylistLabel, "newPlaylistLabel");
            this.newPlaylistLabel.Name = "newPlaylistLabel";
            // 
            // newPlaylistTextbox
            // 
            resources.ApplyResources(this.newPlaylistTextbox, "newPlaylistTextbox");
            this.newPlaylistTextbox.Name = "newPlaylistTextbox";
            // 
            // addButton
            // 
            resources.ApplyResources(this.addButton, "addButton");
            this.addButton.Name = "addButton";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // existingPlaylistsDropdownLabel
            // 
            resources.ApplyResources(this.existingPlaylistsDropdownLabel, "existingPlaylistsDropdownLabel");
            this.existingPlaylistsDropdownLabel.Name = "existingPlaylistsDropdownLabel";
            // 
            // existingPlaylistsDropdownBox
            // 
            this.existingPlaylistsDropdownBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.existingPlaylistsDropdownBox.DropDownWidth = 350;
            resources.ApplyResources(this.existingPlaylistsDropdownBox, "existingPlaylistsDropdownBox");
            this.existingPlaylistsDropdownBox.FormattingEnabled = true;
            this.existingPlaylistsDropdownBox.Name = "existingPlaylistsDropdownBox";
            // 
            // AddToPlaylistWindow
            // 
            this.AcceptButton = this.addButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
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