using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blindspot.Helpers
{
    /// <summary>
    /// Handles creating and registering hotkeys as well as loading them from file
    /// </summary>
    public class BufferHotkeyManager
    {
        public List<Hotkey> Hotkeys { get; set; }
        public IBufferHolder Parent { get; private set; }
        
        public BufferHotkeyManager()
        {
            this.Hotkeys = new List<Hotkey>();
        }

        public BufferHotkeyManager(IBufferHolder parent)
            : this()
        {
            this.Parent = parent;
        }

        public static BufferHotkeyManager LoadFromTextFile(IBufferHolder parent)
        {
            return LoadFromTextFile(parent, new StreamReader(@"Settings\hotkeys.txt"));
        }

        public static BufferHotkeyManager LoadFromTextFile(IBufferHolder parent, StreamReader reader)
        {
            BufferHotkeyManager manager = new BufferHotkeyManager(parent);
            string command, key, origLine, lowerLine;
            bool ctrl, win, shift, alt;
            Hotkey newKey;
            while (!reader.EndOfStream)
            {
                origLine = reader.ReadLine();
                if (origLine.StartsWith("[") || origLine.StartsWith("//")) continue;
                if (origLine.Contains("//")) // remove comments
                {
                    origLine = origLine.Substring(0, origLine.IndexOf("//"));
                }
                lowerLine = origLine.ToLower().Replace(" ", ""); // lower case, no space
                ctrl = (lowerLine.Contains("ctrl+") || lowerLine.Contains("control+"));
                alt = lowerLine.Contains("alt+");
                shift = lowerLine.Contains("shift+");
                win = (lowerLine.Contains("win+") || lowerLine.Contains("windows+"));
                if (win == true || alt == true || ctrl == true || shift == true)
                {
                    int lastPlus = origLine.LastIndexOf("+");
                    key = origLine.Substring(lastPlus + 1, origLine.Length - lastPlus - 1);
                }
                else
                {
                    key = origLine.Split('=')[1];
                }
                command = origLine.Split('=')[0];
                newKey = new Hotkey((Keys)Enum.Parse(typeof(Keys), key, true), shift, ctrl, alt, win);
                if (parent.Commands.ContainsKey(command))
                {
                    newKey.Pressed += parent.Commands[command];
                    if (parent is ContainerControl)
                    {
                        newKey.Register((ContainerControl)parent);
                    }
                }
                manager.Hotkeys.Add(newKey);
            }
            reader.Close();
            return manager;
        }

    }
}