using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Blindspot.Helpers
{
    [Serializable()]
    public class UserSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] ApiKey { get; set; }
        public bool StartInPrivateSession { get; set; }

        [NonSerialized]
        private const string fileLocation = @"Settings\user_settings.dat";

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
    }
}
