using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using ScreenReaderAPIWrapper;
using Blindspot.ViewModels;
using Blindspot.Core.Models;
using System.Threading;

namespace Blindspot.Helpers
{
    public interface IOutputManager
    {
        void OutputMessage(string message, bool interrupt = false);
        void OutputMessageToScreenReader(string message, bool interrupt = false);
        void OutputMessageGraphically(string title, string message, NavigationDirection navigationDirection = NavigationDirection.None);
        void OutputTrackItem(TrackBufferItem item, bool graphicalOutput = true, bool screenReaderOutput = true);
        void OutputBufferListState(BufferListCollection buffers, NavigationDirection direction, bool interrupt = true);
        void OutputTrackModel(Track track, bool graphicalOutput = true, bool screenReaderOutput = true);
        void OutputMessageWithDelay(string message, int milisecondsDelay, bool interrupt = false);
    }
    
    /// <summary>
    /// A class that handles either screen reader or visual output
    /// </summary>
    public class OutputManager : IOutputManager
    {
        public IScreenReader ScreenReader { get; set; }
        public TaskbarNotifier Notifyer { get; set; }
        private UserSettings settings = UserSettings.Instance;
        
        public OutputManager()
        {
            ScreenReader = new ScreenReader();
            ScreenReader.SapiEnabled = settings.SapiIsScreenReaderFallback;
            var screenWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            Notifyer = new TaskbarNotifier()
            {
                ContentClickable = false,
                TitleClickable = false,
                EnableSelectionRectangle = false,
                NormalContentColor = Color.White,
                HoverContentColor = Color.White,
                NormalTitleColor = Color.White,
                HoverTitleColor = Color.White,
                BackColor = Color.DarkBlue,
                TitleRectangle=new Rectangle(20, 5, screenWidth / 4 - 40, 25),
                ContentRectangle=new Rectangle(8, 25, screenWidth / 4 - 16, 60),
                KeepVisibleOnMousOver= true,
                ChangeContentResetsTimer = true,
                ChangeTitleResetsTimer = true
            };
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

        private bool UseScreenReader { get { return settings.ScreenReaderOutput; } }
        private bool UseGraphicalOutput { get { return settings.GraphicalOutput; } }
        private int VisualDisplayTime { get { return settings.VisualOutputDisplayTime * 1000; } }
                
        public void OutputMessage(string message, bool interrupt = false)
        {
            if (UseScreenReader)
                OutputMessageToScreenReader(message, interrupt);
            if (UseGraphicalOutput)
                OutputMessageGraphically("", message);
        }   

        public void OutputMessageToScreenReader(string message, bool interrupt = false)
        {
            ScreenReader.SayString(message, interrupt);
        }

        public void OutputMessageGraphically(string title, string message, NavigationDirection navigationDirection = NavigationDirection.None)
        {
            int appearingTime = 100, disappearingTime = 200;
            switch (Notifyer.TaskbarState)
            {
                case TaskbarNotifier.TaskbarStates.appearing:
                case TaskbarNotifier.TaskbarStates.disappearing:
                    Notifyer.Hide();
                    Notifyer.Show(title, message, appearingTime, VisualDisplayTime, disappearingTime);
                    break;
                case TaskbarNotifier.TaskbarStates.visible:
                    Notifyer.TitleText = title;
                    Notifyer.ContentText = message;
                    break;
                default:
                    Notifyer.Show(title, message, appearingTime, VisualDisplayTime, disappearingTime);
                    break;
            }
        }
        
        public void OutputTrackItem(TrackBufferItem item, bool graphicalOutput = true, bool screenReaderOutput = true)
        {
            if (UseGraphicalOutput && graphicalOutput)
            {
                var artistString = String.Format("{0} {1}", StringStore.By, item.GetArtistNames());
                OutputMessageGraphically(item.Model.Name, artistString);
            }
            if (UseScreenReader && screenReaderOutput)
            {
                OutputMessageToScreenReader(item.ToString());
            }
        }

        public void OutputBufferListState(BufferListCollection buffers, NavigationDirection direction, bool interrupt = true)
        {
            if (UseScreenReader)
            {
                OutputCurrentBufferItemToScreenReader(buffers, direction, interrupt);
            }
            if (UseGraphicalOutput)
            {
                OutputCurrentBufferItemGraphically(buffers);
            }
        }

        private void OutputCurrentBufferItemToScreenReader(BufferListCollection buffers, NavigationDirection direction, bool interrupt)
        {
            string textToRead;
            if (direction == NavigationDirection.Left || direction == NavigationDirection.Right)
            {
                textToRead = buffers.CurrentList.ToString();
            }
            else
            {
                textToRead = buffers.CurrentList.CurrentItem.ToString();
            }
            OutputMessageToScreenReader(textToRead, interrupt);
        }

        private void OutputCurrentBufferItemGraphically(BufferListCollection buffers)
        {
            var currentList = buffers.CurrentList;
            string titleString = string.Format("{0} ({1}/{2})",
                currentList.Name, currentList.CurrentItemIndex + 1, currentList.Count);
            OutputMessageGraphically(titleString, currentList.CurrentItem.ToString());
        }

        public void OutputTrackModel(Track track, bool graphicalOutput = true, bool screenReaderOutput = true)
        {
            // need to decide if we want any unique logic here
            // for now, we just wrap it in a T.B.I as it suits
            var item = new TrackBufferItem(track);
            this.OutputTrackItem(item, graphicalOutput, screenReaderOutput);
        }
        
        public void OutputMessageWithDelay(string message, int milisecondsDelay, bool interrupt = false)
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(milisecondsDelay);
                OutputMessage(message, interrupt);
            });
        }
    }

    // wasn't sure if we were going to need this to animate things somehow
    // for now, works to let us know where things are coming from
    public enum NavigationDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}