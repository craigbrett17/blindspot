using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blindspot.Helpers;
using libspotifydotnet;

namespace Blindspot
{
    public partial class LoginWindow : Form
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            usernameBox.Text = UserSettings.Instance.Username;
        }

        private void newAccountLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.spotify.com/signup/");
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message, StringStore.ProblemLoadingBrowser,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(StringStore.AnUnexpectedErrorOccurred + "\r\n" + ex.Message, StringStore.UnexpectedError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (usernameBox.Text.Length == 0 || passwordBox.Text.Length == 0)
            {
                MessageBox.Show(StringStore.PleaseEnterAUsernameAndPasswordToLoginToSpotify, StringStore.NoUsernameOrPassword,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Username = usernameBox.Text;
            this.Password = passwordBox.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }
    }
}
