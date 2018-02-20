using Library.Tools.Extensions;
using Library.Tools.Tasks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Library.Web
{
    public partial class WebQueryNoHideIP : WebQuery, IDisposable, IWebQuery
    {
        #region Public FIELDS

        public CookieContainer cookieContainer = new CookieContainer();
        public Boolean IsWithLogin = false;
        public Dictionary<string, string> LoginParameters;
        public Uri LoginUrl;

        #endregion

        #region Public CONSTRUCTORS

        /// <summary>
        /// Simple requete sans SAFENETWORK
        /// </summary>
        public WebQueryNoHideIP()
            : base(new CancellationTokenSource())
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 20;
            WebQueryType = WebQueryTypeEnum.WebQuerySimple;
            WithSafeNetwork = false;
        }

        /// <summary>
        /// Simple requete
        /// </summary>
        public WebQueryNoHideIP(CancellationTokenSource iCancellationToken, bool iWithSafeNetwork)
            : base(iCancellationToken)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 20;
            WebQueryType = WebQueryTypeEnum.WebQuerySimple;
            WithSafeNetwork = iWithSafeNetwork;
        }

        /// <summary>
        /// Connexion avec conservation des cookies
        /// </summary>
        public WebQueryNoHideIP(CancellationTokenSource iCancellationToken, Uri iUrl, bool iWithSafeNetwork)
            : base(iCancellationToken)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 20;
            WebQueryType = WebQueryTypeEnum.WebQuerySimple;
            WithSafeNetwork = iWithSafeNetwork;

            Request = (HttpWebRequest)WebRequest.Create(iUrl);
            Request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.146 Safari/537.36";
            Request.KeepAlive = true;
            Request.CookieContainer = cookieContainer;
            Response = (HttpWebResponse)Request.GetResponse();
            Response.Close();
        }

        /// <summary>
        /// Connexion avec authentification login et password
        /// </summary>
        public WebQueryNoHideIP(CancellationTokenSource iCancellationToken, Uri iLoginUrl, Dictionary<string, string> iLoginParameters, Func<bool> iLoginCheck, bool iWithSafeNetwork)
            : base(iCancellationToken)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 20;
            WebQueryType = WebQueryTypeEnum.WebQuerySimple;
            WithSafeNetwork = iWithSafeNetwork;

            LoginUrl = iLoginUrl;
            LoginParameters = iLoginParameters;
            IsWithLogin = true;
            GetHtmlSourcePrivate(LoginUrl, iLoginParameters);
            LoginCheck = iLoginCheck;
        }

        #endregion

        #region Public METHODS

        public override string GetHtmlSource(Uri iUrl, Dictionary<string, string> iParameters = null)
        {
            if (iUrl == null) return null;

            Func<string> action= () =>
            {
                if (iParameters == null)
                    return GetHtmlSourcePrivate(iUrl, true);
                else
                    return GetHtmlSourcePrivate(iUrl, iParameters);
            };

            if (WithSafeNetwork)
                return new InternetRetry(CancellationToken, 1, 3).Run(action);
            else
                return action();
        }

        public override Image GetImage(Uri iUrl)
        {
            Func<Image> action = () =>
            {
                using (var webClient = new WebClient())
                {
                    Stream stream = webClient.OpenRead(iUrl);
                    return Image.FromStream(stream);               
                }
            };

            if (WithSafeNetwork)
                return new InternetRetry(CancellationToken, 1, 3).Run(action);
            else
                return action();
        }

        public bool IsAutoRedirectedUrl(Uri iUrl)
        {
            try
            {
                Func<bool> action = () =>
                {
                    GetHtmlSourcePrivate(iUrl, false);
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
            throw new Exception("Non supporté dans ce webQuery");
        }

        public Boolean RefreshConnection()
        {
            lock (RefreshConnectionLocker)
            {
                Func<bool> action = () =>
                {
                    if (LoginCheck == null) return true;
                    if (LoginCheck()) return true;
                    cookieContainer = new CookieContainer();
                    GetHtmlSourcePrivate(LoginUrl, LoginParameters);
                    return true;
                };
                if (WithSafeNetwork)
                    return new InternetRetry(Notifier, 1, 3).Run(action);
                else
                    return action();
            }
        }

        #endregion

        #region Private FIELDS

        private const string UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.76 Safari/537.36";
        private bool WithSafeNetwork;
        private Func<bool> LoginCheck;
        private object RefreshConnectionLocker = new object();
        private HttpWebRequest Request;
        private HttpWebResponse Response;

        #endregion

        #region Private METHODS

        private string GetHtmlSourcePrivate(Uri iUrl, bool iAllowAutoRedirect)
        {
            if (iUrl.IsNullOrEmpty()) return null;

            Request = (HttpWebRequest)WebRequest.Create(iUrl);
            Request.UserAgent = UserAgent;
            Request.KeepAlive = true;
            Request.CookieContainer = cookieContainer;
            Request.AllowAutoRedirect = iAllowAutoRedirect;

            using (var Response = (HttpWebResponse)Request.GetResponse())
            {
                if ((int)Response.StatusCode >= 300 && (int)Response.StatusCode <= 399 && iAllowAutoRedirect == false)
                {
                    Response.Close();
                    throw new RedirectException();
                }
                else if (Response.StatusCode == HttpStatusCode.OK)
                {
                    string result;
                    Stream receiveStream = Response.GetResponseStream();
                    StreamReader readStream = null;
                    if (Response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    result = readStream.ReadToEnd();
                    result = HttpUtility.HtmlDecode(result);
                    Response.Close();
                    readStream.Close();
                    return result;
                }
            }
            return null;
        }

        private string GetHtmlSourcePrivate(Uri iUrl, Dictionary<string, string> iParameters)
        {
            string pageContent = null;
            if (iUrl != null && iParameters != null)
            {
                string postData = "";

                foreach (string key in iParameters.Keys)
                {
                    postData += HttpUtility.UrlEncode(key) + "="
                          + HttpUtility.UrlEncode(iParameters[key]) + "&";
                }

                Request = (HttpWebRequest)HttpWebRequest.Create(iUrl);
                Request.Method = "POST";

                byte[] data = Encoding.ASCII.GetBytes(postData);

                Request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.146 Safari/537.36";
                Request.CookieContainer = cookieContainer;
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.ContentLength = data.Length;
                Request.KeepAlive = true;

                Stream requestStream = Request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                Response = (HttpWebResponse)Request.GetResponse();

                Stream responseStream = Response.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                pageContent = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                responseStream.Close();

                Response.Close();

                return pageContent;
            }
            return pageContent;
        }

        #endregion
    }

    public partial class WebQueryNoHideIP
    {
        #region Public METHODS

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected METHODS

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                if (Response != null)
                    Response.Close();
            }

            // Free any unmanaged objects here.

            disposed = true;
        }

        #endregion

        #region Private FIELDS

        private bool disposed = false;

        #endregion
    }
}