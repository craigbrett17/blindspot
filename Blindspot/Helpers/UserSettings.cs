using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Blindspot.Helpers
{
    [Serializable()]
    [XmlRoot]
    public class UserSettings
    {
        public string Username { get; set; }
        public bool AutoLogin { get; set; }
        public int SearchResults { get; set; }
        public bool DontShowFirstTimeWizard { get; set; }
        public bool StartInPrivateSession { get; set; }
        public float LastVolume { get; set; }
        public int UILanguageCode { get; set; }
        public UpdateType UpdatesInterestedIn { get; set; }
        public string KeyboardLayoutName { get; set; }
        public bool ScreenReaderOutput { get; set; }
        public bool GraphicalOutput { get; set; }
        public bool OutputTrackChangesGraphically { get; set; }
        public bool OutputTrackChangesWithSpeech { get; set; }
        public bool SapiIsScreenReaderFallback { get; set; }
        public int VisualOutputDisplayTime { get; set; }

        [NonSerialized]
        private static string fileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Blindspot\Settings\user_settings.xml");

        private static UserSettings _instance;
        public static UserSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    LoadFromXML();
                }
                if (_instance == null)
                {
                    LoadFromBinary();
                }
                if (_instance == null)
                {
                    _instance = new UserSettings();
                    //Save();
                }
                return _instance;
            }
        }

        private UserSettings()
        {
            // set default values
            SearchResults = 50;
            GraphicalOutput = true;
            ScreenReaderOutput = true;
            StartInPrivateSession = true;
            OutputTrackChangesGraphically = true;
            SapiIsScreenReaderFallback = true;
            VisualOutputDisplayTime = 5;
        }

        private static void LoadFromBinary()
        {
            string location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Blindspot\Settings\user_settings.dat");

            if (File.Exists(location))
            {
                using (Stream loadStream = File.OpenRead(location))
                {
                    BinaryFormatter deserializer = new BinaryFormatter();
                    _instance = deserializer.Deserialize(loadStream) as UserSettings; 
                }
            }
        }

        public static void LoadFromXML()
        {
            // don't set the instance if there is no file there
            if (!File.Exists(fileLocation)) return;

            var serializer = new XmlSerializer(typeof(UserSettings));
            using (Stream loadStream = File.OpenRead(fileLocation))
            {
                _instance = serializer.Deserialize(loadStream) as UserSettings;
            }
        }

        public static void Save()
        {
            var serializer = new XmlSerializer(typeof(UserSettings));
            using (Stream saveStream = File.Create(fileLocation))
            {
                serializer.Serialize(saveStream, _instance); 
            }
        }
        
        public enum UpdateType
        {
            None,
            Stable,
            Beta,
            Dev
        }
    }
}