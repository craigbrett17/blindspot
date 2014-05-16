using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blindspot.Helpers;
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
            var output = OutputManager.Instance;
            
            // first check if there's not an instance of Blindspot running already
            bool isOnlyInstanceOfApplication;
            var applicationMutex = new Mutex(true, "Global/Blindspot", out isOnlyInstanceOfApplication);
            if (!isOnlyInstanceOfApplication)
            {
                output.OutputMessage(StringStore.BlindspotIsAlreadyRunning, false);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            output.OutputMessage("Launching Blindspot...");
            Application.Run(new BuffersWindow());
            GC.KeepAlive(applicationMutex); // says that up to this point (end of program), we hold this Mutex. MINE!
        }

    }
}
