using Blindspot.Helpers;
using Blindspot.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace Blindspot
{
    public interface IBufferHolder
    {
        BufferHotkeyManager KeyManager { get; set; }
        Dictionary<string, HandledEventHandler> Commands { get; set; }
        BufferListCollection Buffers { get; set; }
    }
}
