namespace Blindspot
{
    partial class SearchWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchWindow));
            this.typeBox = new System.Windows.Forms.GroupBox();
            this.albumButton = new System.Windows.Forms.RadioButton();
            this.artistButton = new System.Windows.Forms.RadioButton();
            this.trackButton = new System.Windows.Forms.RadioButton();
            this.searchTextLabel = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.typeBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // typeBox
            // 
            resources.ApplyResources(this.typeBox, "typeBox");
            this.typeBox.Controls.Add(this.albumButton);
            this.typeBox.Controls.Add(this.artistButton);
            this.typeBox.Controls.Add(this.trackButton);
            this.typeBox.Name = "typeBox";
            this.typeBox.TabStop = false;
            // 
            // albumButton
            // 
            resources.ApplyResources(this.albumButton, "albumButton");
            this.albumButton.Name = "albumButton";
            this.albumButton.TabStop = true;
            this.albumButton.UseVisualStyleBackColor = true;
            // 
            // artistButton
            // 
            resources.ApplyResources(this.artistButton, "artistButton");
            this.artistButton.Name = "artistButton";
            this.artistButton.TabStop = true;
            this.artistButton.UseVisualStyleBackColor = true;
            // 
            // trackButton
            // 
            resources.ApplyResources(this.trackButton, "trackButton");
            this.trackButton.Checked = true;
            this.trackButton.Name = "trackButton";
            this.trackButton.TabStop = true;
            this.trackButton.UseVisualStyleBackColor = true;
            // 
            // searchTextLabel
            // 
            resources.ApplyResources(this.searchTextLabel, "searchTextLabel");
            this.searchTextLabel.Name = "searchTextLabel";
            // 
            // searchTextBox
            // 
            resources.ApplyResources(this.searchTextBox, "searchTextBox");
            this.searchTextBox.Name = "searchTextBox";
            // 
            // searchButton
            // 
            resources.ApplyResources(this.searchButton, "searchButton");
            this.searchButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.searchButton.Name = "searchButton";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // SearchWindow
            // 
            this.AcceptButton = this.searchButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ControlBox = false;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.searchTextLabel);
            this.Controls.Add(this.typeBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchWindow";
            this.typeBox.ResumeLayout(false);
            this.typeBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox typeBox;
        private System.Windows.Forms.RadioButton albumButton;
        private System.Windows.Forms.RadioButton artistButton;
        private System.Windows.Forms.RadioButton trackButton;
        private System.Windows.Forms.Label searchTextLabel;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button closeButton;
    }
}