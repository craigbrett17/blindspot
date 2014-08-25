using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Blindspot.Helpers;

namespace Blindspot.Commands
{
    public class ShowOptionsDialogCommand : HotkeyCommandBase
    {
        private IBufferHolder bufferHolder;

        public ShowOptionsDialogCommand(IBufferHolder formIn)
        {
            bufferHolder = formIn;
        }
        
        public override string Key
        {
            get { return "options_dialog"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            OptionsDialog options = new OptionsDialog();
            options.ShowDialog();
            if (options.DialogResult == DialogResult.OK)
            {
                UserSettings settings = UserSettings.Instance;
                if (settings.UILanguageCode != Thread.CurrentThread.CurrentUICulture.LCID)
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(settings.UILanguageCode);
                }
                if (options.KeyboardSettingsChanged)
                {
                    bufferHolder.ReRegisterHotkeys();
                }
            }
        }
    }
}