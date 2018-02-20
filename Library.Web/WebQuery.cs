using Library.Tools.Debug;
using Library.Tools.Extensions;
using Library.Tools.ProcessTools;
using Library.Tools.Tasks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Library.Web
{
    public class WebQuery : ProgressableCancellable
    {
        #region Public PROPERTIES

        public bool IsHideIp
        {
            get
            {
                return _hideIp;
            }
        }

        /// <summary>
        /// Retourne si une authentification est nécessaire
        /// </summary>
        public bool IsAuthentificationRequired { get; private set; }

        public CookieContainer CookieContainer { get; private set; }

        #endregion

        #region Public CONSTRUCTORS

        public WebQuery(ProgressCancelNotifier iNotifier, bool iKeepCookies, bool iSafeNetwork, bool iHideIp = false, bool iIpCheck = true, int iNewIpFrequencyMinutes = 0)
            : base(iNotifier)
        {
            CookieContainer = new CookieContainer();

            System.Net.ServicePointManager.DefaultConnectionLimit = DEFAULTCONNECTIONLIMIT;
            ServicePointManager.CheckCertificateRevocationList = false;

            //ServicePointManager.MaxServicePointIdleTime = _MaxIdleTime;
            //ServicePointManager.Expect100Continue = false;

            _safeNetwork = iSafeNetwork;
            _hideIp = iHideIp;
            _newIpFrequencyMinutes = iNewIpFrequencyMinutes;
            _keepCookies = iKeepCookies;

            lock (_lastIpChangedDateTimeLocker)
            {
                if (_lastIpChangedDateTime == DateTime.MinValue)
                    _lastIpChangedDateTime = DateTime.Now;
            }

            //Chargement de la liste de user agent
            _userAgentList = new List<string>();
            //_userAgentList.Add("Mozilla/5.0 (compatible; MSIE 9.0; AOL 9.7; AOLBuild 4343.19; Windows NT 6.1; WOW64; Trident/5.0; FunWebProducts)");
            //_userAgentList.Add("Mozilla/5.0 (Windows NT 5.1; rv:31.0) Gecko/20100101 Firefox/31.0");
            _userAgentList.Add("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1944.0 Safari/537.36");
            //_userAgentList.Add("Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5355d Safari/8536.25");
            //_userAgentList.Add("Mozilla/5.0 (X11; U; Linux i686; fr-fr) AppleWebKit/525.1+ (KHTML, like Gecko, Safari/525.1+) midori/1.19");

            if (iHideIp)
            {
                if (ProcessTools.IsProcessRunning("privoxy") == false)
                    throw new Exception("Pour effectuer des requetes via proxy, le processus 'Privoxy' doit être lancé.");

                if (ProcessTools.IsProcessRunning("tor") == false)
                    throw new Exception("Pour effectuer des requetes via proxy, le processus 'TOR' doit être lancé.");
            }
        }

        #endregion

        #region Public METHODS

        /// <summary>
        /// Retourne si une url est bien une url
        /// </summary>
        /// <param name="iUrl"></param>
        /// <returns></returns>
        public static bool IsUrl(string iUrl)
        {
            Uri uriResult;
            return Uri.TryCreate(iUrl, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }

        /// <summary>
        /// Authentification
        /// </summary>
        /// <param name="iUrl"></param>
        /// <param name="iParameters"></param>
        /// <param name="iCheckAuthAction"></param>
        /// <returns></returns>
        public void Authentification(Uri iUrl, Dictionary<string, string> iAuthentificationParameters, Func<WebQuery, Task<bool>> iCheckAuthFunc)
        {
            Task task = AuthentificationAsync(iUrl, iAuthentificationParameters, iCheckAuthFunc);
            task.Wait();
        }

        /// <summary>
        /// Authentification
        /// </summary>
        /// <param name="iUrl"></param>
        /// <param name="iParameters"></param>
        /// <param name="iCheckAuthAction"></param>
        /// <returns></returns>
        public async Task AuthentificationAsync(Uri iUrl, Dictionary<string, string> iAuthentificationParameters, Func<WebQuery, Task<bool>> iCheckAuthFunc)
        {
            IsAuthentificationRequired = true;
            _authentificationUri = iUrl;
            _checkAuthentificationFunc = iCheckAuthFunc;
            _authentificationParameters = iAuthentificationParameters;
            await PostAsync(iUrl, iAuthentificationParameters, false);
        }

        /// <summary>
        /// Authentification
        /// </summary>
        /// <param name="iUrl"></param>
        /// <param name="iParameters"></param>
        /// <param name="iCheckAuthAction"></param>
        /// <returns></returns>
        public async Task AuthentificationAsync(Uri iUrl, Dictionary<string, string> iAuthentificationParameters, Func<WebQuery, Task<bool>> iCheckAuthFunc, Uri iFirstUri, string iKey, Func<string, string> iValueFunc)
        {
            IsAuthentificationRequired = true;
            _needFirstCall = true;
            _firstCallKey = iKey;
            _firstCallValueFunc = iValueFunc;
            _firstUri = iFirstUri;
            _authentificationUri = iUrl;
            _checkAuthentificationFunc = iCheckAuthFunc;
            _authentificationParameters = iAuthentificationParameters;
            await RunAuthentificationAsync();
        }

        /// <summary>
        /// Ré execute l'authenfication
        /// </summary>
        /// <returns></returns>
        public void RefreshAuthentification()
        {
            Task task = RefreshAuthentificationAsync();
            task.Wait();
        }

        /// <summary>
        /// Ré execute l'authenfication
        /// </summary>
        /// <returns></returns>
        public async Task RefreshAuthentificationAsync()
        {
            await RunAuthentificationAsync();
        }

        ///// <summary>
        ///// Retourne l'ip en cours ASYNC
        ///// </summary>
        ///// <returns></returns>
        //public string GetMyIp()
        //{
        //    Task<string> task = GetMyIpAsync();
        //    task.Wait();
        //    return task.Result;
        //}

        /// <summary>
        /// retourne si l'authentification est tjrs ok.
        /// </summary>
        /// <returns></returns>
        public bool IsAuthentificated()
        {
            var task = _checkAuthentificationFunc(this);
            return task.Result;
        }

        /// <summary>
        /// Retourne l'ip en cours ASYNC
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetMyIpAsync()
        {
            string html = await GetSourceAsync(new Uri("http://bot.whatismyipaddress.com/"));
            if (html != null)
            {
                HtmlAgilityPack.HtmlDocument theDocument = new HtmlAgilityPack.HtmlDocument();
                theDocument.LoadHtml(html);
                HtmlAgilityPack.HtmlNode documentNode = theDocument.DocumentNode;

                return documentNode.InnerText;
            }
            return null;
        }

        /// <summary>
        /// Retourne l'ip en cours
        /// </summary>
        /// <returns></returns>
        public string GetMyIp()
        {
            string html = GetSource(new Uri("http://bot.whatismyipaddress.com/"));
            if (html != null)
            {
                HtmlAgilityPack.HtmlDocument theDocument = new HtmlAgilityPack.HtmlDocument();
                theDocument.LoadHtml(html);
                HtmlAgilityPack.HtmlNode documentNode = theDocument.DocumentNode;

                return documentNode.InnerText;
            }
            return null;
        }

        /// <summary>
        /// Post une requete, pour l'authentification par exemple
        /// </summary>
        /// <param name="iUrl"></param>
        /// <param name="iParameters"></param>
        /// <returns></returns>
        public string Post(Uri iUrl, Dictionary<string, string> iParameters, bool iBeforeCleanCookies)
        {
            Task<string> task = PostAsync(iUrl, iParameters, iBeforeCleanCookies);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Post une requete, pour l'authentification par exemple
        /// </summary>
        /// <param name="iUrl"></param>
        /// <param name="iParameters"></param>
        /// <returns></returns>
        public async Task<string> PostAsync(Uri iUrl, Dictionary<string, string> iParameters, bool iBeforeCleanCookies)
        {
            Func<Task<string>> action = async () =>
            {
                if (iBeforeCleanCookies) CookieContainer = null;

                if (iParameters == null)
                    iParameters = new Dictionary<string, string>();
                var parameterDic = new FormUrlEncodedContent(iParameters);

                using (var client = GetHttpClient())
                using (HttpResponseMessage response = await client.PostAsync(iUrl, parameterDic, Notifier.CancellationTokenSource.Token))
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception();
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            };

            if (_safeNetwork)
                return await new InternetRetry(Notifier, WAITBETWEETRYSECONDS, RETRYNUMBERS).Run(action);
            else
                return await action();
        }

        /// <summary>
        /// Retourne une image
        /// </summary>
        /// <returns></returns>
        public Image GetImage(Uri iUrl)
        {
            var task =  GetImageAsync(iUrl);
            return task.Result;
        }

        /// <summary>
        /// Retourne une image
        /// </summary>
        /// <param name="iUrl"></param>
        /// <returns></returns>
        public async Task<Image> GetImageAsync(Uri iUrl)
        {
            Func<Task<Image>> action = async () =>
            {
                if (_hideIp) NewIp();

                using (var client = GetHttpClient())
                {
                    var stream = await client.GetStreamAsync(iUrl);
                    return Image.FromStream(stream);
                }
            };
            if (_safeNetwork)
                return await new InternetRetry(Notifier, WAITBETWEETRYSECONDS, RETRYNUMBERS).Run(action);
            else
                return await action();
        }

        /// <summary>
        /// Retourne la source
        /// </summary>
        /// <returns></returns>
        public string GetSource(Uri iUrl)
        {
            var task = GetSourceAsync(iUrl);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Retourne la page source
        /// </summary>
        /// <param name="iUrl"></param>
        /// <returns></returns>
        public async Task<string> GetSourceAsync(Uri iUrl)
        {
            Func<Task<string>> action = async () =>
            {
                if (_hideIp) NewIp();

                using (var client = GetHttpClient())
                {
                    var response = await client.GetAsync(iUrl).ConfigureAwait(false);
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return content;
                }
            };
            if (_safeNetwork)
                return await new InternetRetry(Notifier, WAITBETWEETRYSECONDS, RETRYNUMBERS).Run(action);
            else
                return await action();
        }

        /// <summary>
        /// Attribut une nouvelle Ip si besoin
        /// </summary>
        /// <returns></returns>
        public void NewIp()
        {
            if (_hideIp == false) return;

            //gestion du multithreading
            lock (_newIpLocker)
            {
                Action action = () =>
           {
               if (_lastIpChangedDateTime.AddMinutes(_newIpFrequencyMinutes) > DateTime.Now) return;

               var ip = new IPEndPoint(IPAddress.Parse(SOCKSADRESS), SOCKSPORT);
               var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
               try
               {
                   client.Connect(ip);
               }
               catch (SocketException)
               {
                   return;
               }

               client.Send(Encoding.ASCII.GetBytes("AUTHENTICATE \"" + SOCKSPASSWORD + "\"\n"));
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
                       client.Shutdown(SocketShutdown.Both);
                       client.Close();
                       return;
                   }
               }
               else
               {
                   client.Shutdown(SocketShutdown.Both);
                   client.Close();
                   return;
               }
               client.Shutdown(SocketShutdown.Both);
               client.Close();

               lock (_lastIpChangedDateTimeLocker)
                   _lastIpChangedDateTime = DateTime.Now;
               return;
           };
                if (_safeNetwork)
                    new InternetRetry(Notifier, WAITBETWEETRYSECONDS, RETRYNUMBERS).Run(action);
                else
                    action();
            }
        }

        /// <summary>
        /// Retourne si la connexion définie par le constructeur est connecté à internet
        /// </summary>
        /// <param name="iUrl"></param>
        /// <returns></returns>
        public bool IsInternetConnected(Uri iUrl = null)
        {
            try
            {
                if (iUrl == null) iUrl = new Uri("http://www.google.com");
                GetSource(iUrl);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Défini le container de cookies pour le webquery
        /// </summary>
        /// <param name="iCookieContainer"></param>
        public void SetCookierContainter(CookieContainer iCookieContainer)
        {
            CookieContainer = iCookieContainer;
        }

        #endregion

        #region Private FIELDS

        private const string PROXYADRESS = "127.0.0.1";
        private const int PROXYPORT = 8118;
        private const string SOCKSADRESS = "127.0.0.1";
        private const string SOCKSPASSWORD = "szzfzemapoksbcuozv";
        private const int SOCKSPORT = 9051;
        private const int WAITBETWEETRYSECONDS = 1;
        private const int RETRYNUMBERS = 3;
        private const int DEFAULTCONNECTIONLIMIT = 30;
        private static DateTime _lastIpChangedDateTime = DateTime.MinValue;
        private static object _lastIpChangedDateTimeLocker = new object();
        private static object _newIpLocker = new object();
        private bool _safeNetwork;
        private bool _hideIp;
        private int _newIpFrequencyMinutes;
        private bool _keepCookies;
        private List<string> _userAgentList;
        private Func<WebQuery, Task<bool>> _checkAuthentificationFunc;
        private Uri _authentificationUri;
        private Dictionary<string, string> _authentificationParameters;
        private bool _needFirstCall;
        private string _firstCallKey;
        private Func<string, string> _firstCallValueFunc;
        private Uri _firstUri;

        #endregion

        #region Private METHODS

        private async Task RunAuthentificationAsync()
        {
            if (_needFirstCall == false)
            {
                await PostAsync(_authentificationUri, _authentificationParameters, true);
            }
            else
            {
                var source = await GetSourceAsync(_firstUri);
                string contextKey = _firstCallValueFunc(source);

                var parameterDic = new Dictionary<string, string>();

                foreach (var item in _authentificationParameters.Enum())
                    parameterDic.Add(item.Key, item.Value);

                parameterDic.Add(_firstCallKey, contextKey);
                await PostAsync(_authentificationUri, parameterDic, false);
            }
        }

        private HttpClient GetHttpClient()
        {
            var handler = new HttpClientHandler();

            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            if (_keepCookies)
            {
                if (CookieContainer == null)
                    CookieContainer = new CookieContainer();
                handler.CookieContainer = CookieContainer;
            }

            if (_hideIp)
            {
                handler.Proxy = new WebProxy(PROXYADRESS + ":" + PROXYPORT, true);
                handler.UseProxy = true;
            }

            var httpClient = new HttpClient(handler, false);

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

            var userAgentRandom = new Random();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", _userAgentList[userAgentRandom.Next(0, _userAgentList.Count() - 1)]);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Cache-Control", "max-age=0");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "fr-FR");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "fr;q=0.8");

            return httpClient;
        }

        #endregion
    }
}