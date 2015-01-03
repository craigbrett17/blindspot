using Blindspot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
using NAudio.CoreAudioApi;
using NAudio.Wave;

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
        /// <summary>
        /// Whether or not the screen reader output setting has changed whilst this dialog has been open
        /// </summary>
        public bool ScreenReaderOutputChanged { get; set; }
        /// <summary>
        /// Whether or not the SAPI fallback setting has changed whilst this dialog has been open
        /// </summary>
        public bool SAPIIsFallbackChanged;
        public bool OutputTrackChangeWithScreenReaderChanged;
        public bool VisualOutputChanged;
        public bool OutputTrackChangeGraphicallyChanged;
        public bool VisualDisplayTimeChanged;
        public bool OutputDeviceChanged;

        public OptionsDialog()
        {
            InitializeComponent();
            SetupLanguageBox();
            LoadKeyboardDescriptions();
            SetupKeyboardSettingsBox();
            SetupOutputDeviceBox();
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
            var devicesList = deviceBox.Items.Cast<DirectSoundDeviceInfo>().ToList(); // need to pull the items out of the combobox
            deviceBox.SelectedIndex = devicesList.FindIndex(device => device.Guid == settings.OutputDeviceID);
            if (deviceBox.SelectedIndex == -1) deviceBox.SelectedIndex = 0;

            screenReaderBox.Checked = settings.ScreenReaderOutput;
            screenReaderSapiFallbackBox.Checked = settings.SapiIsScreenReaderFallback;
            screenReaderTrackChangeBox.Checked = settings.OutputTrackChangesWithSpeech;
            EnableOrDisableControlsFromMasterCheckbox(screenReaderBox, speechPage);

            visualOutputBox.Checked = settings.GraphicalOutput;
            visualOutputTimeBox.Value = settings.VisualOutputDisplayTime;
            visualDisplayTrackChangesBox.Checked = settings.OutputTrackChangesGraphically;
            EnableOrDisableControlsFromMasterCheckbox(visualOutputBox, visualPage);
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
            OutputDeviceChanged = deviceBox.SelectedIndex >= 0 && ((DirectSoundDeviceInfo)deviceBox.SelectedItem).Guid != settings.OutputDeviceID;

            ScreenReaderOutputChanged = screenReaderBox.Checked != settings.ScreenReaderOutput;
            SAPIIsFallbackChanged = screenReaderSapiFallbackBox.Checked != settings.SapiIsScreenReaderFallback;
            OutputTrackChangeWithScreenReaderChanged = screenReaderTrackChangeBox.Checked != settings.OutputTrackChangesWithSpeech;

            VisualOutputChanged = visualOutputBox.Checked != settings.GraphicalOutput;
            VisualDisplayTimeChanged = visualOutputTimeBox.Value != settings.VisualOutputDisplayTime;
            OutputTrackChangeGraphicallyChanged = visualDisplayTrackChangesBox.Checked != settings.OutputTrackChangesGraphically;
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
            if (OutputDeviceChanged)
            {
                settings.OutputDeviceID = ((DirectSoundDeviceInfo)deviceBox.SelectedItem).Guid;
                hasAnythingChanged = true;
            }
            if (ScreenReaderOutputChanged)
            {
                settings.ScreenReaderOutput = screenReaderBox.Checked;
                hasAnythingChanged = true;
            }
            if (OutputTrackChangeWithScreenReaderChanged)
            {
                settings.OutputTrackChangesWithSpeech = screenReaderTrackChangeBox.Checked;
                hasAnythingChanged = true;
            }
            if (SAPIIsFallbackChanged)
            {
                settings.SapiIsScreenReaderFallback = screenReaderSapiFallbackBox.Checked;
                OutputManager.Instance.ScreenReader.SapiEnabled = settings.SapiIsScreenReaderFallback;
                hasAnythingChanged = true;
            }
            if (VisualOutputChanged)
            {
                settings.GraphicalOutput = visualOutputBox.Checked;
                hasAnythingChanged = true;
            }
            if (VisualDisplayTimeChanged)
            {
                settings.VisualOutputDisplayTime = (int)visualOutputTimeBox.Value;
                hasAnythingChanged = true;
            }
            if (OutputTrackChangeGraphicallyChanged)
            {
                settings.OutputTrackChangesGraphically = visualDisplayTrackChangesBox.Checked;
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

        private void SetupOutputDeviceBox()
        {
            deviceBox.DisplayMember = "Description";
            // can't just use Guid as a value member here unfortunately
            // doesn't seem to come through. Means a bit of ugly casting
            foreach (var device in DirectSoundOut.Devices)
            {
                deviceBox.Items.Add(device);
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

        private void screenReaderBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisableControlsFromMasterCheckbox(screenReaderBox, speechPage);
        }
        
        private void visualOutputBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisableControlsFromMasterCheckbox(visualOutputBox, visualPage);
        }

        private static void EnableOrDisableControlsFromMasterCheckbox(CheckBox masterCheckbox, Control container)
        {
            bool enabled = masterCheckbox.Checked;
            foreach (Control control in container.Controls)
            {
                // find all the controls except the main one and set them to the checked state of the main box
                if (control.Name == masterCheckbox.Name)
                    continue;
                control.Enabled = enabled;
            }
        }
        
    }
}