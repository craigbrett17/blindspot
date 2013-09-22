using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Blindspot.Helpers
{
    [Serializable()]
    public class UserSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] ApiKey { get; set; }
        public bool StartInPrivateSession { get; set; }
        private int _searchResults = 50;
        public int SearchResults
        {
            get { return _searchResults; }
            set { _searchResults = value; }
        }
        public bool DontShowFirstTimeInfo { get; set; }
        public bool AutoLogin { get; set; }
        public float LastVolume { get; set; }
        public Language UILanguage { get; set; }

        [NonSerialized]
        private static string fileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Blindspot\Settings\user_settings.dat");

        private static UserSettings _instance;
        public static UserSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    Load();
                }
                if (_instance == null)
                {
                    _instance = new UserSettings();
                    Save();
                }
                return _instance;
            }
        }

        private UserSettings() { }

        private static void Load()
        {
            if (File.Exists(fileLocation))
            {
                using (Stream loadStream = File.OpenRead(fileLocation))
                {
                    BinaryFormatter deserializer = new BinaryFormatter();
                    _instance = deserializer.Deserialize(loadStream) as UserSettings; 
                }
            }
        }
        
        public static void Save()
        {
            using (Stream saveStream = File.Create(fileLocation))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(saveStream, _instance); 
            }
        }

        public enum Language
        {
            English = 0,
            French,
            Spanish,
            German,
            Swedish
        }
    }
}
