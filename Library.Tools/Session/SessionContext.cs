using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Library.Tools.Session
{
	public static class SessionContext
	{
		public static Session CurrentSession { get; set; }

		public static void CheckToken()
		{
			if (CurrentSession.TokenId != GetCorrectToken())
				throw new Exception("Accès refusé : Le token est incorrect");
		}

		public static void SetToken()
		{
			CurrentSession.TokenId = GetCorrectToken();
		}

		private static string GetCorrectToken()
		{
			return System.Configuration.ConfigurationManager.AppSettings["TokenId"];
		}
	}
}
