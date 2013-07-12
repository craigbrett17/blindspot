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
            this.typeBox = new System.Windows.Forms.GroupBox();
            this.albumButton = new System.Windows.Forms.RadioButton();
            this.artistButton = new System.Windows.Forms.RadioButton();
            this.titleButton = new System.Windows.Forms.RadioButton();
            this.searchTextLabel = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.typeBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // typeBox
            // 
            this.typeBox.Controls.Add(this.albumButton);
            this.typeBox.Controls.Add(this.artistButton);
            this.typeBox.Controls.Add(this.titleButton);
            this.typeBox.Location = new System.Drawing.Point(0, 0);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(300, 60);
            this.typeBox.TabIndex = 0;
            this.typeBox.TabStop = false;
            this.typeBox.Text = "Search type";
            // 
            // albumButton
            // 
            this.albumButton.AutoSize = true;
            this.albumButton.Location = new System.Drawing.Point(155, 25);
            this.albumButton.Name = "albumButton";
            this.albumButton.Size = new System.Drawing.Size(65, 21);
            this.albumButton.TabIndex = 2;
            this.albumButton.TabStop = true;
            this.albumButton.Text = "A&lbum";
            this.albumButton.UseVisualStyleBackColor = true;
            // 
            // artistButton
            // 
            this.artistButton.AutoSize = true;
            this.artistButton.Location = new System.Drawing.Point(80, 25);
            this.artistButton.Name = "artistButton";
            this.artistButton.Size = new System.Drawing.Size(58, 21);
            this.artistButton.TabIndex = 1;
            this.artistButton.TabStop = true;
            this.artistButton.Text = "A&rtist";
            this.artistButton.UseVisualStyleBackColor = true;
            // 
            // titleButton
            // 
            this.titleButton.AutoSize = true;
            this.titleButton.Checked = true;
            this.titleButton.Location = new System.Drawing.Point(10, 25);
            this.titleButton.Name = "titleButton";
            this.titleButton.Size = new System.Drawing.Size(53, 21);
            this.titleButton.TabIndex = 0;
            this.titleButton.TabStop = true;
            this.titleButton.Text = "&Title";
            this.titleButton.UseVisualStyleBackColor = true;
            // 
            // searchTextLabel
            // 
            this.searchTextLabel.AutoSize = true;
            this.searchTextLabel.Location = new System.Drawing.Point(5, 70);
            this.searchTextLabel.Name = "searchTextLabel";
            this.searchTextLabel.Size = new System.Drawing.Size(84, 17);
            this.searchTextLabel.TabIndex = 1;
            this.searchTextLabel.Text = "S&earch Text";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(5, 90);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(200, 23);
            this.searchTextBox.TabIndex = 2;
            // 
            // searchButton
            // 
            this.searchButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.searchButton.Location = new System.Drawing.Point(30, 130);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "&Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(110, 130);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "&Cancel";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // SearchWindow
            // 
            this.AcceptButton = this.searchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(234, 191);
            this.ControlBox = false;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.searchTextLabel);
            this.Controls.Add(this.typeBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blindspot Search";
            this.typeBox.ResumeLayout(false);
            this.typeBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox typeBox;
        private System.Windows.Forms.RadioButton albumButton;
        private System.Windows.Forms.RadioButton artistButton;
        private System.Windows.Forms.RadioButton titleButton;
        private System.Windows.Forms.Label searchTextLabel;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button closeButton;
    }
}