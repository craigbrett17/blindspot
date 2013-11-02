using Blindspot.Helpers;
using ScreenReaderAPIWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Blindspot
{
    public partial class FirstTimeWizard : Form
    {
        private int currentStep = 0;
        private List<GroupBox> stepBoxes;
        private XDocument keyboardDescriptions;
        private CultureInfo CurrentUICulture
        {
            get { return System.Threading.Thread.CurrentThread.CurrentUICulture; }
        }

        public FirstTimeWizard()
        {
            InitializeComponent();
            SetupLanguageBox();
            LoadKeyboardDescriptions();
            SetupKeyboardSettingsBox();
            stepBoxes = new List<GroupBox>() { step1GroupBox, step2GroupBox };
        }
        
        private void FirstTimeWizard_Load(object sender, EventArgs e)
        {
            keyboardDescriptionBox.Text = GetKeyboardDescription();
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
            int currentUICultureID = CurrentUICulture.LCID;
            languageBox.SelectedValue = currentUICultureID;
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
            keyboardStyleBox.DataSource = new BindingSource(fileAndPath, null);
            keyboardStyleBox.DisplayMember = "Key";
            keyboardStyleBox.ValueMember = "Value";
            string screenReaderName = ScreenReader.getCurrentScreenReaderName().ToLower();
            if ((screenReaderName == "jfw" || screenReaderName == "jaws" || screenReaderName == "jaws for windows")
                && fileAndPath.ContainsKey("Modern"))
            {
                keyboardStyleBox.SelectedValue = fileAndPath["Modern"];
            }
            else if (fileAndPath.ContainsKey("Standard"))
            {
                keyboardStyleBox.SelectedValue = fileAndPath["Standard"];
            }
        }

        private string GetKeyboardDescription()
        {
            string notFound = StringStore.NoDescriptionAvailable;
            if (keyboardDescriptions == null) return notFound;
            var selectedKeyboardLayout = ((KeyValuePair<string, string>)keyboardStyleBox.SelectedItem).Key;
            var layoutElement = keyboardDescriptions.Root.Elements().FirstOrDefault(l => l.Attribute("name").Value == selectedKeyboardLayout);
            if (layoutElement == null) return notFound;
            var descriptionElement = layoutElement.Elements().FirstOrDefault(l => l.Attribute("language").Value == CurrentUICulture.TwoLetterISOLanguageName);
            if (descriptionElement == null) return notFound;
            return descriptionElement.Value;
        }

        private void skipButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (currentStep == 0) return;
            stepBoxes[currentStep].Visible = false;
            stepBoxes[--currentStep].Visible = true;
            if (currentStep == 0) backButton.Enabled = false;
            if (currentStep < stepBoxes.Count - 1)
            {
                if (!nextButton.Enabled) nextButton.Enabled = true;
                saveButton.Enabled = false;
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (currentStep == stepBoxes.Count - 1) return;
            stepBoxes[currentStep].Visible = false;
            stepBoxes[++currentStep].Visible = true;
            if (currentStep > 0 && !backButton.Enabled) backButton.Enabled = true;
            if (currentStep == stepBoxes.Count - 1)
            {
                nextButton.Enabled = false;
                saveButton.Enabled = true;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var selectedKeyboardLayoutPath = keyboardStyleBox.SelectedValue as string;
            if (!keyboardStyleNoChangeBox.Checked && !String.IsNullOrEmpty(selectedKeyboardLayoutPath))
            {
                var targetDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Blindspot\Settings");
                File.Copy(selectedKeyboardLayoutPath, Path.Combine(targetDirectoryPath, "hotkeys.txt"), true);
            }
            UserSettings.Instance.UILanguageCode = (int)languageBox.SelectedValue;
            UserSettings.Instance.DontShowFirstTimeWizard = true;
            UserSettings.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void keyboardStyleBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            keyboardDescriptionBox.Text = GetKeyboardDescription();
        }

        private void keyboardStyleNoChangeBox_CheckedChanged(object sender, EventArgs e)
        {
            keyboardStyleBox.Enabled = !keyboardStyleNoChangeBox.Checked;
        }

    }
}
