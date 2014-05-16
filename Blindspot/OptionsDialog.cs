﻿using Blindspot.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ScreenReaderAPIWrapper;
using System.Xml.Linq;

namespace Blindspot
{
    public partial class OptionsDialog : Form
    {
        UserSettings settings = UserSettings.Instance;
        private XDocument keyboardDescriptions;
        private CultureInfo CurrentUICulture
        {
            get { return System.Threading.Thread.CurrentThread.CurrentUICulture; }
        }
        /// <summary>
        /// Whether or not the language has changed whilst this dialog has been open
        /// </summary>
        public bool LangSettingsChanged;
        /// <summary>
        /// Whether or not the keyboard layout has changed whilst this dialog has been open
        /// </summary>
        public bool KeyboardSettingsChanged;
        /// <summary>
        /// Whether the auto-update setting has changed whilst this dialog has been open
        /// </summary>
        public bool AutoUpdateSettingsChanged;

        public OptionsDialog()
        {
            InitializeComponent();
            SetupLanguageBox();
            LoadKeyboardDescriptions();
            SetupKeyboardSettingsBox();
        }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
            keyboardDescriptionBox.Text = GetKeyboardDescription();
            SetControlsFromUserSettings();
        }

        private void SetControlsFromUserSettings()
        {
            autoUpdateEnabledBox.Checked = settings.UpdatesInterestedIn != UserSettings.UpdateType.None;
            autoUpdateTypeBox.Enabled = settings.UpdatesInterestedIn != UserSettings.UpdateType.None;
            if (autoUpdateEnabledBox.Checked)
                autoUpdateTypeBox.Text = settings.UpdatesInterestedIn.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            CheckWhichSettingsHaveChanged();
            ProcessSettingsChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CheckWhichSettingsHaveChanged()
        {
            LangSettingsChanged = langSettingsHaveChanged;
            KeyboardSettingsChanged = keyboardSettingsHaveChanged;
            AutoUpdateSettingsChanged = autoUpdateSettingsHaveChanged;
        }

        private void ProcessSettingsChanges()
        {
            bool hasAnythingChanged = false;
            if (LangSettingsChanged)
            {
                settings.UILanguageCode = (int)languageBox.SelectedValue;
                hasAnythingChanged = true;
            }
            if (KeyboardSettingsChanged)
            {
                settings.KeyboardLayoutName = keyboardLayoutBox.Text;
                if (!String.IsNullOrEmpty(settings.KeyboardLayoutName))
                {
                    var newFilePath = keyboardLayoutBox.SelectedValue.ToString();
                    var targetDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Blindspot\Settings");
                    File.Copy(newFilePath, Path.Combine(targetDirectoryPath, "hotkeys.txt"), true);
                }
                hasAnythingChanged = true;
            }
            if (AutoUpdateSettingsChanged)
            {
                settings.UpdatesInterestedIn = (autoUpdateEnabledBox.Checked)
                    ? (UserSettings.UpdateType)Enum.Parse(typeof(UserSettings.UpdateType), autoUpdateTypeBox.Text, true)
                    : UserSettings.UpdateType.None;
                hasAnythingChanged = true;
            }
            if (hasAnythingChanged)
            {
                UserSettings.Save();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editHotkeysButton_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Blindspot\Settings\hotkeys.txt"));
                keyboardLayoutBox.SelectedIndex = -1;
                keyboardDescriptionBox.Text = "";
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message, StringStore.UnexpectedErrorOccurred,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(StringStore.AnUnexpectedErrorOccurred + "\r\n" + ex.Message, StringStore.UnexpectedError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void autoUpdateEnabledBox_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = autoUpdateEnabledBox.Checked;
            if (isChecked)
            {
                autoUpdateTypeBox.Enabled = true;
                if (String.IsNullOrEmpty(autoUpdateTypeBox.Text))
                    autoUpdateTypeBox.SelectedIndex = 0;
            }
            else
            {
                autoUpdateTypeBox.Enabled = false;
            }
        }

        private void SetupLanguageBox()
        {
            var langList = new List<CultureInfo>();
            langList.Add(new CultureInfo("de"));
            langList.Add(new CultureInfo("en"));
            langList.Add(new CultureInfo("es"));
            langList.Add(new CultureInfo("fr"));
            langList.Add(new CultureInfo("sv"));
            languageBox.DataSource = langList;
            languageBox.DisplayMember = "NativeName";
            languageBox.ValueMember = "LCID";
            int currentUICultureID = Thread.CurrentThread.CurrentUICulture.LCID;
            languageBox.SelectedValue = currentUICultureID;
        }

        private void LoadKeyboardDescriptions()
        {
            try
            {
                var fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Blindspot\Keyboard Layouts\Layout Descriptions.xml");
                if (!File.Exists(fullPath))
                    return;
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    keyboardDescriptions = XDocument.Load(stream);
                }
            }
            catch (Exception)
            {
                keyboardDescriptions = null;
            }
        }

        private void SetupKeyboardSettingsBox()
        {
            var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Blindspot\Keyboard Layouts");
            var keyboardFiles = Directory.GetFiles(directoryPath, "*.txt");
            Dictionary<string, string> fileAndPath = new Dictionary<string, string>();
            foreach (string keyboardFile in keyboardFiles)
            {
                int startIndex = keyboardFile.LastIndexOf('\\') + 1;
                fileAndPath.Add(keyboardFile.Substring(startIndex, keyboardFile.Length - startIndex - 4), keyboardFile);
            }
            // bind this dictionary of strings to the box
            keyboardLayoutBox.DataSource = new BindingSource(fileAndPath, null);
            keyboardLayoutBox.DisplayMember = "Key";
            keyboardLayoutBox.ValueMember = "Value";
            if (!String.IsNullOrEmpty(settings.KeyboardLayoutName) && fileAndPath.ContainsKey(settings.KeyboardLayoutName))
            {
                keyboardLayoutBox.SelectedValue = fileAndPath[settings.KeyboardLayoutName];
            }
            else if (fileAndPath.ContainsKey("Standard"))
            {
                keyboardLayoutBox.SelectedValue = fileAndPath["Standard"];
            }
        }

        private string GetKeyboardDescription()
        {
            string notFound = StringStore.NoDescriptionAvailable;
            if (keyboardDescriptions == null) return notFound;
            var selectedKeyboardLayout = ((KeyValuePair<string, string>)keyboardLayoutBox.SelectedItem).Key;
            var layoutElement = keyboardDescriptions.Root.Elements().FirstOrDefault(l => l.Attribute("name").Value == selectedKeyboardLayout);
            if (layoutElement == null) return notFound;
            var descriptionElement = layoutElement.Elements().FirstOrDefault(l => l.Attribute("language").Value == CurrentUICulture.TwoLetterISOLanguageName);
            if (descriptionElement == null) return notFound;
            return descriptionElement.Value;
        }

        private void keyboardLayoutBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            keyboardDescriptionBox.Text = GetKeyboardDescription();
        }

        private bool langSettingsHaveChanged
        {
            get { return (int)languageBox.SelectedValue != settings.UILanguageCode; }
        }

        private bool keyboardSettingsHaveChanged
        {
            get
            {
                return keyboardLayoutBox.Text != settings.KeyboardLayoutName;
            }
        }

        private bool autoUpdateSettingsHaveChanged
        {
            get
            {
                UserSettings.UpdateType updateType;
                if (!autoUpdateEnabledBox.Checked)
                {
                    updateType = UserSettings.UpdateType.None;
                }
                else
                {
                    updateType = (UserSettings.UpdateType)Enum.Parse(typeof(UserSettings.UpdateType), autoUpdateTypeBox.Text, true);
                }
                return settings.UpdatesInterestedIn != updateType;
            }
        }

    }
}