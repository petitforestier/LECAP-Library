using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Library.Tools.Debug
{
	public static class Logger
	{
		#region Public PROPERTIES

		public static string LogFileFullpath { get; set; }

		#endregion

		#region Public METHODS

		public static void WriteLog(string iMessage)
		{
			try
			{
				using (var sw = new StreamWriter(LogFileFullpath, true))
				{
					sw.WriteLine(DateTime.Now.ToString() + ": " + iMessage);
					sw.Flush();
					sw.Close();
				}
			}
			catch
			{
			}
		}

		#endregion
	}
}