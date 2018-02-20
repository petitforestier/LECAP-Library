using System.Diagnostics;
using System.Linq;
using System.Threading;
namespace Library.Tools.ProcessTools
{
	

	public static class ProcessTools
	{
		#region Public METHODS

		public static bool IsProcessRunning(string iProcessName)
		{
			Process[] pname = Process.GetProcessesByName(iProcessName);
			if (pname.Length == 0)
				return false;
			else
				return true;
		}

        public static void KillProcessIfRunning(string iProcessName)
        {
            Process[] pname = Process.GetProcessesByName(iProcessName);
            if (pname.Length != 0)
                foreach (var item in pname.ToList())
                {
                    item.Kill();
                    try
                    {
                        item.WaitForExit();
                    }
                    catch { }  
                }

            System.Threading.Thread.Sleep(1000 * 5);      
        }

		#endregion Public METHODS
	}
}