using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenReaderAPIWrapper;

namespace Blindspot.Helpers
{
    public interface IOutputManager
    {
        void OutputMessage(string message, bool interrupt = false, NavigationDirection navigationDirection = NavigationDirection.None);
        void OutputMessageToScreenReader(string message, bool interrupt = false);
        void OutputMessageGraphically(string message, bool interrupt = false, NavigationDirection navigationDirection = NavigationDirection.None);
    }
    
    /// <summary>
    /// A class that handles either screen reader or visual output
    /// </summary>
    public class OutputManager : IOutputManager
    {
        public IScreenReader ScreenReader { get; set; }
        
        public OutputManager()
        {
            ScreenReader = new ScreenReader();
            ScreenReader.SapiEnabled = true;
        }

        public OutputManager(IScreenReader screenreader)
        {
            ScreenReader = screenreader;
        }

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
                
        public void OutputMessage(string message, bool interrupt = false, NavigationDirection navigationDirection = NavigationDirection.None)
        {
            if (UseScreenReader)
                OutputMessageToScreenReader(message, interrupt);
            if (UseGraphicalOutput)
                OutputMessageGraphically(message, interrupt, navigationDirection);
        }   

        public void OutputMessageToScreenReader(string message, bool interrupt = false)
        {
            ScreenReader.SayString(message, interrupt);
        }

        public void OutputMessageGraphically(string message, bool interrupt = false, NavigationDirection navigationDirection = NavigationDirection.None)
        {
            throw new NotImplementedException();
        }
    }

    public enum NavigationDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}