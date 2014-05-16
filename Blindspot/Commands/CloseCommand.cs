using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Blindspot.Helpers;

namespace Blindspot.Commands
{
    public class CloseCommand : HotkeyCommandBase
    {
        private Form parent;
        private IOutputManager _output;

        public CloseCommand(Form parentForm)
        {
            parent = parentForm;
            _output = OutputManager.Instance;
        }

        public override string Key
        {
            get { return "close_blindspot"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            _output.OutputMessage(StringStore.ExitingProgram);
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