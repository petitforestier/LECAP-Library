using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Debug;

namespace Library.Tools.Misc
{
    public static class DesignMode
    {
        public static bool IsInDesignMode()
        {
            return (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Contains("devenv"));
        }
    }
}
