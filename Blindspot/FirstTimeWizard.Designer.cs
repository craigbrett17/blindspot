namespace Blindspot
{
    partial class FirstTimeWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstTimeWizard));
            this.step1GroupBox = new System.Windows.Forms.GroupBox();
            this.languageBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.step1WelcomeLabel = new System.Windows.Forms.Label();
            this.step2GroupBox = new System.Windows.Forms.GroupBox();
            this.keyboardDescriptionBox = new System.Windows.Forms.TextBox();
            this.keyboardDescriptionLabel = new System.Windows.Forms.Label();
            this.keyboardStyleBox = new System.Windows.Forms.ComboBox();
            this.keyboardStyleLabel = new System.Windows.Forms.Label();
            this.skipButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.step1GroupBox.SuspendLayout();
            this.step2GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // step1GroupBox
            // 
            resources.ApplyResources(this.step1GroupBox, "step1GroupBox");
            this.step1GroupBox.Controls.Add(this.languageBox);
            this.step1GroupBox.Controls.Add(this.languageLabel);
            this.step1GroupBox.Controls.Add(this.step1WelcomeLabel);
            this.step1GroupBox.Name = "step1GroupBox";
            this.step1GroupBox.TabStop = false;
            // 
            // languageBox
            // 
            resources.ApplyResources(this.languageBox, "languageBox");
            this.languageBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.languageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageBox.FormattingEnabled = true;
            this.languageBox.Name = "languageBox";
            // 
            // languageLabel
            // 
            resources.ApplyResources(this.languageLabel, "languageLabel");
            this.languageLabel.Name = "languageLabel";
            // 
            // step1WelcomeLabel
            // 
            resources.ApplyResources(this.step1WelcomeLabel, "step1WelcomeLabel");
            this.step1WelcomeLabel.Name = "step1WelcomeLabel";
            // 
            // step2GroupBox
            // 
            resources.ApplyResources(this.step2GroupBox, "step2GroupBox");
            this.step2GroupBox.Controls.Add(this.keyboardDescriptionBox);
            this.step2GroupBox.Controls.Add(this.keyboardDescriptionLabel);
            this.step2GroupBox.Controls.Add(this.keyboardStyleBox);
            this.step2GroupBox.Controls.Add(this.keyboardStyleLabel);
            this.step2GroupBox.Name = "step2GroupBox";
            this.step2GroupBox.TabStop = false;
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
            // keyboardStyleBox
            // 
            resources.ApplyResources(this.keyboardStyleBox, "keyboardStyleBox");
            this.keyboardStyleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.keyboardStyleBox.FormattingEnabled = true;
            this.keyboardStyleBox.Name = "keyboardStyleBox";
            this.keyboardStyleBox.SelectionChangeCommitted += new System.EventHandler(this.keyboardStyleBox_SelectionChangeCommitted);
            // 
            // keyboardStyleLabel
            // 
            resources.ApplyResources(this.keyboardStyleLabel, "keyboardStyleLabel");
            this.keyboardStyleLabel.Name = "keyboardStyleLabel";
            // 
            // skipButton
            // 
            resources.ApplyResources(this.skipButton, "skipButton");
            this.skipButton.Name = "skipButton";
            this.skipButton.UseVisualStyleBackColor = true;
            this.skipButton.Click += new System.EventHandler(this.skipButton_Click);
            // 
            // backButton
            // 
            resources.ApplyResources(this.backButton, "backButton");
            this.backButton.Name = "backButton";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // nextButton
            // 
            resources.ApplyResources(this.nextButton, "nextButton");
            this.nextButton.Name = "nextButton";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // saveButton
            // 
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.Name = "saveButton";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // FirstTimeWizard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.step1GroupBox);
            this.Controls.Add(this.step2GroupBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.skipButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FirstTimeWizard";
            this.Load += new System.EventHandler(this.FirstTimeWizard_Load);
            this.step1GroupBox.ResumeLayout(false);
            this.step1GroupBox.PerformLayout();
            this.step2GroupBox.ResumeLayout(false);
            this.step2GroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox step1GroupBox;
        private System.Windows.Forms.ComboBox languageBox;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.Label step1WelcomeLabel;
        private System.Windows.Forms.GroupBox step2GroupBox;
        private System.Windows.Forms.Label keyboardDescriptionLabel;
        private System.Windows.Forms.ComboBox keyboardStyleBox;
        private System.Windows.Forms.Label keyboardStyleLabel;
        private System.Windows.Forms.TextBox keyboardDescriptionBox;
        private System.Windows.Forms.Button skipButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button saveButton;
    }
}