using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Library.Web
{
	public interface IWebQuery : IDisposable
	{
		#region Public METHODS

        string GetHtmlSource(Uri iUrl, Dictionary<string, string> iParameters = null);

		Image GetImage(Uri iUrl);

		string GetMyIp();

		WebQueryTypeEnum GetWebQueryType();

		bool IsAutoRedirectedUrl(Uri iUrl);

		bool IsInternetConnected(Uri iUrl = null);

		bool NewIp();

		bool RefreshConnection();

		#endregion Public METHODS
	}
}