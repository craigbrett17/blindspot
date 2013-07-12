using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenReaderAPIWrapper;

namespace Blindspot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ScreenReader.sapiEnable(true);
            ScreenReader.SayString("Launching Blindspot...");
            Application.Run(new BuffersWindow());
        }

    }
}
