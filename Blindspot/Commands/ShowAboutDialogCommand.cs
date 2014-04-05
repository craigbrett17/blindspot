using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Blindspot.Commands
{
    public class ShowAboutDialogCommand : HotkeyCommandBase
    {
        public override string Key
        {
            get { return "show_about_window"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            string aboutText = GetApplicationInfoText();
            MessageBox.Show(aboutText, "About Blindspot", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string GetApplicationInfoText()
        {
            var version = Application.ProductVersion;
            var productName = Application.ProductName;
            StringBuilder aboutInfo = new StringBuilder();
            aboutInfo.AppendFormat("{0} version {1}", productName, version);
            aboutInfo.AppendLine();
            aboutInfo.AppendFormat("Copyright (c) {0} {1}", DateTime.Now.Year, Application.CompanyName);
            aboutInfo.AppendLine();
            aboutInfo.AppendLine();
            aboutInfo.AppendLine("Powered by SPOTIFY(R) CORE");
            return aboutInfo.ToString();
        }

    }
}
