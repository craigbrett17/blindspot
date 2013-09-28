using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Blindspot.Helpers
{
    public class RegistryReader
    {
        private RegistryKey baseKey;
        private string subKey;

        public RegistryReader()
        {
            baseKey = Registry.CurrentUser;
            subKey = "Software\\Blindspot";
        }

        public string Read(string keyName)
        {
            RegistryKey key = baseKey.OpenSubKey(subKey);
            if (key == null) return null;
            return (string)key.GetValue(keyName.ToUpper());
        }
    }
}