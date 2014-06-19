namespace Blindspot
{
    partial class OptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
            this.tabHolder = new System.Windows.Forms.TabControl();
            this.generalPage = new System.Windows.Forms.TabPage();
            this.autoUpdateTypeBox = new System.Windows.Forms.ComboBox();
            this.autoUpdateTypeLabel = new System.Windows.Forms.Label();
            this.autoUpdateEnabledBox = new System.Windows.Forms.CheckBox();
            this.languageBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.keyboardPage = new System.Windows.Forms.TabPage();
            this.keyboardDescriptionBox = new System.Windows.Forms.TextBox();
            this.keyboardDescriptionLabel = new System.Windows.Forms.Label();
            this.keyboardLayoutBox = new System.Windows.Forms.ComboBox();
            this.keyboardLayoutLabel = new System.Windows.Forms.Label();
            this.keyboardPageDescription = new System.Windows.Forms.Label();
            this.editHotkeysButton = new System.Windows.Forms.Button();
            this.speechPage = new System.Windows.Forms.TabPage();
            this.screenReaderSapiFallbackBox = new System.Windows.Forms.CheckBox();
            this.screenReaderTrackChangeBox = new System.Windows.Forms.CheckBox();
            this.screenReaderBox = new System.Windows.Forms.CheckBox();
            this.visualPage = new System.Windows.Forms.TabPage();
            this.visualOutputTimeBox = new System.Windows.Forms.NumericUpDown();
            this.visualOutputTimeLabel = new System.Windows.Forms.Label();
            this.visualDisplayTrackChangesBox = new System.Windows.Forms.CheckBox();
            this.visualOutputBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.tabHolder.SuspendLayout();
            this.generalPage.SuspendLayout();
            this.keyboardPage.SuspendLayout();
            this.speechPage.SuspendLayout();
            this.visualPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.visualOutputTimeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabHolder
            // 
            resources.ApplyResources(this.tabHolder, "tabHolder");
            this.tabHolder.Controls.Add(this.generalPage);
            this.tabHolder.Controls.Add(this.keyboardPage);
            this.tabHolder.Controls.Add(this.speechPage);
            this.tabHolder.Controls.Add(this.visualPage);
            this.tabHolder.Name = "tabHolder";
            this.tabHolder.SelectedIndex = 0;
            // 
            // generalPage
            // 
            resources.ApplyResources(this.generalPage, "generalPage");
            this.generalPage.Controls.Add(this.autoUpdateTypeBox);
            this.generalPage.Controls.Add(this.autoUpdateTypeLabel);
            this.generalPage.Controls.Add(this.autoUpdateEnabledBox);
            this.generalPage.Controls.Add(this.languageBox);
            this.generalPage.Controls.Add(this.languageLabel);
            this.generalPage.Name = "generalPage";
            this.generalPage.UseVisualStyleBackColor = true;
            // 
            // autoUpdateTypeBox
            // 
            resources.ApplyResources(this.autoUpdateTypeBox, "autoUpdateTypeBox");
            this.autoUpdateTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.autoUpdateTypeBox.FormattingEnabled = true;
            this.autoUpdateTypeBox.Items.AddRange(new object[] {
            resources.GetString("autoUpdateTypeBox.Items"),
            resources.GetString("autoUpdateTypeBox.Items1")});
            this.autoUpdateTypeBox.Name = "autoUpdateTypeBox";
            // 
            // autoUpdateTypeLabel
            // 
            resources.ApplyResources(this.autoUpdateTypeLabel, "autoUpdateTypeLabel");
            this.autoUpdateTypeLabel.Name = "autoUpdateTypeLabel";
            // 
            // autoUpdateEnabledBox
            // 
            resources.ApplyResources(this.autoUpdateEnabledBox, "autoUpdateEnabledBox");
            this.autoUpdateEnabledBox.Name = "autoUpdateEnabledBox";
            this.autoUpdateEnabledBox.UseVisualStyleBackColor = true;
            this.autoUpdateEnabledBox.CheckedChanged += new System.EventHandler(this.autoUpdateEnabledBox_CheckedChanged);
            // 
            // languageBox
            // 
            resources.ApplyResources(this.languageBox, "languageBox");
            this.languageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageBox.FormattingEnabled = true;
            this.languageBox.Name = "languageBox";
            // 
            // languageLabel
            // 
            resources.ApplyResources(this.languageLabel, "languageLabel");
            this.languageLabel.Name = "languageLabel";
            // 
            // keyboardPage
            // 
            resources.ApplyResources(this.keyboardPage, "keyboardPage");
            this.keyboardPage.Controls.Add(this.keyboardDescriptionBox);
            this.keyboardPage.Controls.Add(this.keyboardDescriptionLabel);
            this.keyboardPage.Controls.Add(this.keyboardLayoutBox);
            this.keyboardPage.Controls.Add(this.keyboardLayoutLabel);
            this.keyboardPage.Controls.Add(this.keyboardPageDescription);
            this.keyboardPage.Controls.Add(this.editHotkeysButton);
            this.keyboardPage.Name = "keyboardPage";
            this.keyboardPage.UseVisualStyleBackColor = true;
            // 
            // keyboardDescriptionBox
            // 
            resources.ApplyResources(this.keyboardDescriptionBox, "keyboardDescriptionBox");
            this.keyboardDescriptionBox.Name = "keyboardDescriptionBox";
            this.keyboardDescriptionBox.ReadOnly = true;
            // 
            // keyboardDescriptionLabel
            // 
            resources.ApplyResources(this.keyboardDescriptionLabel, "keyboardDescriptionLabel");
            this.keyboardDescriptionLabel.Name = "keyboardDescriptionLabel";
            // 
            // keyboardLayoutBox
            // 
            resources.ApplyResources(this.keyboardLayoutBox, "keyboardLayoutBox");
            this.keyboardLayoutBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.keyboardLayoutBox.FormattingEnabled = true;
            this.keyboardLayoutBox.Name = "keyboardLayoutBox";
            this.keyboardLayoutBox.SelectionChangeCommitted += new System.EventHandler(this.keyboardLayoutBox_SelectionChangeCommitted);
            // 
            // keyboardLayoutLabel
            // 
            resources.ApplyResources(this.keyboardLayoutLabel, "keyboardLayoutLabel");
            this.keyboardLayoutLabel.Name = "keyboardLayoutLabel";
            // 
            // keyboardPageDescription
            // 
            resources.ApplyResources(this.keyboardPageDescription, "keyboardPageDescription");
            this.keyboardPageDescription.Name = "keyboardPageDescription";
            // 
            // editHotkeysButton
            // 
            resources.ApplyResources(this.editHotkeysButton, "editHotkeysButton");
            this.editHotkeysButton.Name = "editHotkeysButton";
            this.editHotkeysButton.UseVisualStyleBackColor = true;
            this.editHotkeysButton.Click += new System.EventHandler(this.editHotkeysButton_Click);
            // 
            // speechPage
            // 
            resources.ApplyResources(this.speechPage, "speechPage");
            this.speechPage.Controls.Add(this.screenReaderSapiFallbackBox);
            this.speechPage.Controls.Add(this.screenReaderTrackChangeBox);
            this.speechPage.Controls.Add(this.screenReaderBox);
            this.speechPage.Name = "speechPage";
            this.speechPage.UseVisualStyleBackColor = true;
            // 
            // screenReaderSapiFallbackBox
            // 
            resources.ApplyResources(this.screenReaderSapiFallbackBox, "screenReaderSapiFallbackBox");
            this.screenReaderSapiFallbackBox.Name = "screenReaderSapiFallbackBox";
            this.screenReaderSapiFallbackBox.UseVisualStyleBackColor = true;
            // 
            // screenReaderTrackChangeBox
            // 
            resources.ApplyResources(this.screenReaderTrackChangeBox, "screenReaderTrackChangeBox");
            this.screenReaderTrackChangeBox.Name = "screenReaderTrackChangeBox";
            this.screenReaderTrackChangeBox.UseVisualStyleBackColor = true;
            // 
            // screenReaderBox
            // 
            resources.ApplyResources(this.screenReaderBox, "screenReaderBox");
            this.screenReaderBox.Name = "screenReaderBox";
            this.screenReaderBox.UseVisualStyleBackColor = true;
            this.screenReaderBox.CheckedChanged += new System.EventHandler(this.screenReaderBox_CheckedChanged);
            // 
            // visualPage
            // 
            resources.ApplyResources(this.visualPage, "visualPage");
            this.visualPage.Controls.Add(this.visualOutputTimeBox);
            this.visualPage.Controls.Add(this.visualOutputTimeLabel);
            this.visualPage.Controls.Add(this.visualDisplayTrackChangesBox);
            this.visualPage.Controls.Add(this.visualOutputBox);
            this.visualPage.Name = "visualPage";
            this.visualPage.UseVisualStyleBackColor = true;
            // 
            // visualOutputTimeBox
            // 
            resources.ApplyResources(this.visualOutputTimeBox, "visualOutputTimeBox");
            this.visualOutputTimeBox.Name = "visualOutputTimeBox";
            this.visualOutputTimeBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // visualOutputTimeLabel
            // 
            resources.ApplyResources(this.visualOutputTimeLabel, "visualOutputTimeLabel");
            this.visualOutputTimeLabel.Name = "visualOutputTimeLabel";
            // 
            // visualDisplayTrackChangesBox
            // 
            resources.ApplyResources(this.visualDisplayTrackChangesBox, "visualDisplayTrackChangesBox");
            this.visualDisplayTrackChangesBox.Name = "visualDisplayTrackChangesBox";
            this.visualDisplayTrackChangesBox.UseVisualStyleBackColor = true;
            // 
            // visualOutputBox
            // 
            resources.ApplyResources(this.visualOutputBox, "visualOutputBox");
            this.visualOutputBox.Name = "visualOutputBox";
            this.visualOutputBox.UseVisualStyleBackColor = true;
            this.visualOutputBox.CheckedChanged += new System.EventHandler(this.visualOutputBox_CheckedChanged);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ControlBox = false;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tabHolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "OptionsDialog";
            this.Load += new System.EventHandler(this.OptionsDialog_Load);
            this.tabHolder.ResumeLayout(false);
            this.generalPage.ResumeLayout(false);
            this.generalPage.PerformLayout();
            this.keyboardPage.ResumeLayout(false);
            this.keyboardPage.PerformLayout();
            this.speechPage.ResumeLayout(false);
            this.speechPage.PerformLayout();
            this.visualPage.ResumeLayout(false);
            this.visualPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.visualOutputTimeBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabHolder;
        private System.Windows.Forms.TabPage generalPage;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ComboBox languageBox;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.ComboBox autoUpdateTypeBox;
        private System.Windows.Forms.Label autoUpdateTypeLabel;
        private System.Windows.Forms.CheckBox autoUpdateEnabledBox;
        private System.Windows.Forms.TabPage keyboardPage;
        private System.Windows.Forms.ComboBox keyboardLayoutBox;
        private System.Windows.Forms.Label keyboardLayoutLabel;
        private System.Windows.Forms.Label keyboardPageDescription;
        private System.Windows.Forms.Button editHotkeysButton;
        private System.Windows.Forms.TextBox keyboardDescriptionBox;
        private System.Windows.Forms.Label keyboardDescriptionLabel;
        private System.Windows.Forms.TabPage speechPage;
        private System.Windows.Forms.CheckBox screenReaderTrackChangeBox;
        private System.Windows.Forms.CheckBox screenReaderBox;
        private System.Windows.Forms.CheckBox screenReaderSapiFallbackBox;
        private System.Windows.Forms.TabPage visualPage;
        private System.Windows.Forms.CheckBox visualDisplayTrackChangesBox;
        private System.Windows.Forms.CheckBox visualOutputBox;
        private System.Windows.Forms.NumericUpDown visualOutputTimeBox;
        private System.Windows.Forms.Label visualOutputTimeLabel;
    }
}