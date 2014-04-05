using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class CloseCommand : HotkeyCommandBase
    {
        private Form parent;

        public CloseCommand(Form parentForm)
        {
            parent = parentForm;
        }

        public override string Key
        {
            get { return "close_blindspot"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            ScreenReader.SayString(StringStore.ExitingProgram);
            if (parent.InvokeRequired)
            {
                parent.Invoke(new Action(() => { parent.Close(); }));
            }
            else
            {
                parent.Close();
            }
        }
    }
}