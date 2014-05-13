using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blindspot.Helpers
{
    /// <summary>
    /// A class that handles either screen reader or visual output
    /// </summary>
    public class OutputManager
    {
        private OutputManager()
        { }

        private static OutputManager _instance;
        public static OutputManager Instance
        {
            get
            {
                return _instance ?? (_instance = new OutputManager());
            }
        }

        private bool UseScreenReader { get { return UserSettings.Instance.ScreenReaderOutput; } }
        private bool UseGraphicalOutput { get { return UserSettings.Instance.GraphicalOutput; } }
    }
}