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
            this.editHotkeysButton = new System.Windows.Forms.Button();
            this.languageBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.tabHolder.SuspendLayout();
            this.generalPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabHolder
            // 
            this.tabHolder.Controls.Add(this.generalPage);
            resources.ApplyResources(this.tabHolder, "tabHolder");
            this.tabHolder.Name = "tabHolder";
            this.tabHolder.SelectedIndex = 0;
            // 
            // generalPage
            // 
            this.generalPage.Controls.Add(this.editHotkeysButton);
            this.generalPage.Controls.Add(this.languageBox);
            this.generalPage.Controls.Add(this.languageLabel);
            resources.ApplyResources(this.generalPage, "generalPage");
            this.generalPage.Name = "generalPage";
            this.generalPage.UseVisualStyleBackColor = true;
            // 
            // editHotkeysButton
            // 
            resources.ApplyResources(this.editHotkeysButton, "editHotkeysButton");
            this.editHotkeysButton.Name = "editHotkeysButton";
            this.editHotkeysButton.UseVisualStyleBackColor = true;
            this.editHotkeysButton.Click += new System.EventHandler(this.editHotkeysButton_Click);
            // 
            // languageBox
            // 
            this.languageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageBox.FormattingEnabled = true;
            resources.ApplyResources(this.languageBox, "languageBox");
            this.languageBox.Name = "languageBox";
            // 
            // languageLabel
            // 
            resources.ApplyResources(this.languageLabel, "languageLabel");
            this.languageLabel.Name = "languageLabel";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.closeButton, "closeButton");
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
            this.tabHolder.ResumeLayout(false);
            this.generalPage.ResumeLayout(false);
            this.generalPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabHolder;
        private System.Windows.Forms.TabPage generalPage;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ComboBox languageBox;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.Button editHotkeysButton;
    }
}