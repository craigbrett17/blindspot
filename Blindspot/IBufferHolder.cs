using Blindspot.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using Blindspot.Commands;
using Blindspot.Helpers;

namespace Blindspot
{
    public interface IBufferHolder
    {
        BufferHotkeyManager KeyManager { get; set; }
        Dictionary<string, HotkeyCommandBase> Commands { get; set; }
        BufferListCollection Buffers { get; set; }
        void ReRegisterHotkeys();
    }
}
