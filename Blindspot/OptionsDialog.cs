using Blindspot.Helpers;
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

namespace Blindspot
{
    public partial class OptionsDialog : Form
    {
        UserSettings settings = UserSettings.Instance;

        public OptionsDialog()
        {
            InitializeComponent();
            SetupLanguageBox();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            bool hasAnythingChanged = false;
            if (LangSettingsHaveChanged)
            {
                settings.UILanguageCode = (int)languageBox.SelectedValue;
                hasAnythingChanged = true;
            }
            if (hasAnythingChanged)
            {
                UserSettings.Save(); 
            }
            this.Close();
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

        private bool LangSettingsHaveChanged
        {
            get { return (int)languageBox.SelectedValue != settings.UILanguageCode; }
        }
    }
}
