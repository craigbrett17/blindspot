using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Blindspot.Controllers
{
    /// <summary>
    /// Outputs things to debug log
    /// </summary>
    public static class Logger
    {
        public static void WriteDebug(string message)
        {
            Debug.WriteLine(message);
        }
        
        public static void WriteDebug(string format, params object[] args)
        {
            Debug.WriteLine(format, args);
        }

        public static void WriteTrace(string message)
        {
            Trace.WriteLine(message);
        }

        public static void WriteTrace(string format, params object[] args)
        {
            Trace.WriteLine(String.Format(format, args));
        }

    }
}