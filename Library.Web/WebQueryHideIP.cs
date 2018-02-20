using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Library.Tools.Extensions;
using Library.Tools.ProcessTools;
using Library.Tools.Tasks;
using System.Drawing;
using System.Threading.Tasks;

namespace Library.Web
{
	public partial class WebQueryHideIP : WebQuery, IWebQuery
	{
		#region Public FIELDS

		public static DateTime dateLastResetIp = DateTime.Now;

		#endregion Public FIELDS

		#region Public CONSTRUCTORS

		const string PROXYADRESS = "127.0.0.1";
		const int PROXYPORT = 8118;
		const string SOCKSADRESS = "127.0.0.1";
		const string SOCKSPASSWORD = "szzfzemapoksbcuozv";
		const int SOCKSPORT = 9051;


		/// <param name="iNewIpFrequency">Temps en minutes</param>
		public WebQueryHideIP(ProgressCancelNotifier iProgressCancelNotifier, bool iWithSafeNetwork, bool iWithIpCheck = true, int iNewIpFrequency = 30)
			: base(iProgressCancelNotifier)
		{
			if (ProcessTools.IsProcessRunning("privoxy") == false)
			{
				throw new Exception("Pour effectuer des requetes via proxy, le logiciel 'Privoxy' doit être lancé.");
			}

			if (ProcessTools.IsProcessRunning("vidalia") == false)
			{
				throw new Exception("Pour effectuer des requetes via proxy, le logiciel 'Vidalia' doit être lancé.");
			}

			WebQueryType = WebQueryTypeEnum.WebQueryWithProxy;

			newIpFrequency = iNewIpFrequency;
			proxyAdress = PROXYADRESS;
			proxyPort = PROXYPORT;
			socksAdress = SOCKSADRESS;
			socksPort = SOCKSPORT;
			socksPassword = SOCKSPASSWORD;

			WithSafeNetwork = iWithSafeNetwork;

			//Chargement de la liste de user agent
			userAgentList.Add("Mozilla/5.0 (compatible; MSIE 9.0; AOL 9.7; AOLBuild 4343.19; Windows NT 6.1; WOW64; Trident/5.0; FunWebProducts)");
			userAgentList.Add("Mozilla/5.0 (Windows NT 5.1; rv:31.0) Gecko/20100101 Firefox/31.0");
			userAgentList.Add("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1944.0 Safari/537.36");
			userAgentList.Add("Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5355d Safari/8536.25");
			userAgentList.Add("Opera/9.80 (Windows NT 6.0) Presto/2.12.388 Version/12.14");
			userAgentList.Add("Mozilla/5.0 (X11; U; Linux i686; fr-fr) AppleWebKit/525.1+ (KHTML, like Gecko, Safari/525.1+) midori/1.19");

			//vérification de l'adresse proxy différente de l'adresse réel:
			if(iWithIpCheck)
			{
				string realIp = new WebQueryNoHideIP(Notifier, iWithSafeNetwork).GetMyIp();
				string proxyIp = GetMyIp();

				if (proxyIp != null && realIp != null && proxyIp != realIp)
				{
					using (var sublevel1 = new ProgressCancelNotifier.SubLevel(Notifier))
					{
						Notifier.Report("Ip proxy : {0}, Ip box : {1} ".FormatString(proxyIp, realIp));
					}
				}
				else
				{
					throw new Exception("Error lors de la vérification du proxy. Vérifier si le socks(vidalia) et le proxy(privoxy) sont bien démarrés.");
				}
			}
			
		}

		#endregion Public CONSTRUCTORS

		#region Public METHODS

		/// <summary>
		/// Retourne la code source Html de la page
		/// </summary>
        public override string GetHtmlSource(Uri iUrl, Dictionary<string, string> iParameters = null)
		{
			if (iUrl == null) return null;

            //todo optimisation permettre les paramètres dans la requete post
            if (iParameters != null)
                throw new NotSupportedException();

			Func<string> action = () =>
			{
				using(HttpWebResponse response = GetHttpWebResponseFromUrl(iUrl, true))
				{
					string result;
					Stream receiveStream = response.GetResponseStream();
					StreamReader readStream = null;
					if (response.CharacterSet == null)
						readStream = new StreamReader(receiveStream);
					else
						readStream = new StreamReader(receiveStream, Encoding.UTF8);

					result = readStream.ReadToEnd();

					readStream.Close();
					result = HttpUtility.HtmlDecode(result);
					return result;			
				}		
			};
			if (WithSafeNetwork)
				return new InternetRetry(Notifier, 1, 3).Run(action);
			else
				return action();
		}

		/// <summary>
		/// Retourne l'image de l'URL
		/// </summary>
		public override Image GetImage(Uri iUrl)
		{
			if (iUrl == null) return null;

			Func<Image> action = () =>
			{
				HttpWebResponse response = GetHttpWebResponseFromUrl(iUrl, true);
				Image result;
				Stream receiveStream = response.GetResponseStream();
				result = Image.FromStream(receiveStream);
				return result;			
			};
			if (WithSafeNetwork)
				return new InternetRetry(Notifier, 1, 3).Run(action);
			else
				return action();
		}

		public bool IsAutoRedirectedUrl(Uri iUrl)
		{
			try
			{
				Func<bool> action = () =>
				{
					GetHttpWebResponseFromUrl(iUrl, false);
					return false;
				};
				if (WithSafeNetwork)
					return new InternetRetry(Notifier, 1, 3).Run(action);
				else
					return action();
			}
			catch (RedirectException)
			{
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public bool NewIp()
		{
			string currentIp = CurrentIp;
			var NewIpLock = new object();

			lock (NewIpLock)
			{
				//gestion du multithreading
				if (currentIp != CurrentIp) return true;

				IPEndPoint ip = new IPEndPoint(IPAddress.Parse(socksAdress), socksPort);
				Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				try
				{
					client.Connect(ip);
				}
				catch (SocketException e)
				{
					MessageBox.Show("Unable to connect to server of Tor." + e.Message);
					return false;
				}

				client.Send(Encoding.ASCII.GetBytes("AUTHENTICATE \"" + socksPassword + "\"\n"));
				byte[] data = new byte[1024];
				int receivedDataLength = client.Receive(data);
				string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);

				if (stringData.Contains("250"))
				{
					client.Send(Encoding.ASCII.GetBytes("SIGNAL NEWNYM\r\n"));
					data = new byte[1024];
					receivedDataLength = client.Receive(data);
					stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
					if (!stringData.Contains("250"))
					{
						MessageBox.Show("Unable to signal new user to server of Tor.");
						client.Shutdown(SocketShutdown.Both);
						client.Close();
						return false;
					}
				}
				else
				{
					MessageBox.Show("Unable to authenticate to server of Tor.");
					client.Shutdown(SocketShutdown.Both);
					client.Close();
					return false;
				}
				client.Shutdown(SocketShutdown.Both);
				client.Close();

				dateLastResetIp = DateTime.Now;

				using (var level1 = new ProgressCancelNotifier.SubLevel(Notifier))
				{
					Notifier.Report(new Progress() { Message = "Nouvelle ip : " + GetMyIp() });
				}
				return true;
			}
		}

		public Boolean RefreshConnection()
		{
			throw new Exception("Non supporté dans ce webQuery");
		}

		#endregion Public METHODS

		#region Private FIELDS

		private static List<string> userAgentList = new List<string>();
		private bool WithSafeNetwork;
		private int newIpFrequency;
		private string proxyAdress;
		private int proxyPort;
		private string socksAdress;
		private string socksPassword;
		private int socksPort;

		#endregion Private FIELDS

		#region Private METHODS

		private HttpWebResponse GetHttpWebResponseFromUrl(Uri iUrl, bool iAllowAutoRedirect)
		{
			if (iUrl.IsNullOrEmpty())
				return null;

			TimeSpan ts = DateTime.Now - dateLastResetIp;
			if (ts.Minutes >= newIpFrequency) { NewIp(); };

			HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(iUrl);
			Request.KeepAlive = true;
			Request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
			Request.AllowAutoRedirect = iAllowAutoRedirect;

			Random userAgentRandom = new Random();
			Request.UserAgent = userAgentList[userAgentRandom.Next(0, userAgentList.Count() - 1)];
			Request.Proxy = new WebProxy(proxyAdress + ":" + proxyPort);

			WebHeaderCollection headerCollection = Request.Headers;
			headerCollection.Add("Cache-Control", "max-age=0");
			headerCollection.Add("Accept-Language:fr-FR");
			headerCollection.Add("Accept-Language", "fr;q=0.8");

			HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();

			if ((int)Response.StatusCode >= 300 && (int)Response.StatusCode <= 399 && iAllowAutoRedirect == false)
			{
				Response.Dispose();
				throw new RedirectException();
			}
			else if (Response.StatusCode == HttpStatusCode.OK)
			{		
				return Response;
			}

			return null;
		}

		#endregion Private METHODS
	}

	public partial class WebQueryHideIP
	{
		#region Public METHODS

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion Public METHODS

		#region Protected METHODS

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;

			if (disposing)
			{
				// Free any other managed objects here.
			}

			// Free any unmanaged objects here.

			disposed = true;
		}

		#endregion Protected METHODS

		#region Private FIELDS

		private bool disposed = false;

		#endregion Private FIELDS
	}
}