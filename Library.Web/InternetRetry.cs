using System;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using Library.Tools.Thread;
using Library.Tools.Tasks;
using System.Threading;
using Library.Tools.Debug;

namespace Library.Web
{
    public class InternetRetry : ProgressableCancellable
    {
        #region Public CONSTRUCTORS

        /// <summary>
        /// Permettre d'effectuer plusieurs tentative lors d'action internet
        /// </summary>
        public InternetRetry(ProgressCancelNotifier iNotifier, int iWaitSecondBetweenCycles, int iNumberOfRetries, string iSiteUrl = null)
            : base(iNotifier)
        {
            _sleep = iWaitSecondBetweenCycles;
            _retries = iNumberOfRetries;
            _SiteUrl = iSiteUrl;
        }

        #endregion Public CONSTRUCTORS

        #region Public METHODS

        /// <summary>
        /// Execute une action internet sans retour, avec plusieurs tentative
        /// </summary>
        public void Run(Action iAction)
        {
            Run(() => { iAction(); return 0; });
        }

        /// <summary>
        /// Execute une action internet avec retour, avec plusieurs tentative
        /// </summary>
        public T Run<T>(Func<T> iAction)
        {
            if (RuntimeConfiguration.GetRuntimeConfiguration() == Tools.Enums.RuntimeConfEnum.Release)
            {
                int tryInternetCount = 1;
                int tryCount = 1;

                while (true == true)
                {
                    Notifier.ThrowIfCancellationRequested();

                    try
                    {
                        return iAction();
                    }
                    catch (Exception ex)
                    {
                        if (Notifier.IsCanceled) throw;

                        if (tryCount <= _retries)
                        {
                            Uri theUri = null;

                            if (_SiteUrl != null)
                                theUri = new Uri(_SiteUrl);

                            if (new WebQuery(Notifier, false, false).IsInternetConnected(theUri) == false)
                            {
                                if (_SiteUrl != null)
                                    MyDebug.PrintError(" => site '{0}' indisponible => tentative : {1}".FormatString(_SiteUrl, tryInternetCount), ex);
                                else
                                    MyDebug.PrintError(" => Internet indisponible => tentative : {0}".FormatString(tryInternetCount), ex);

                                new SleepWithStopCheck(Notifier).RunSecond(INTERNETSECONDWAITINGTIME);
                                tryInternetCount += 1;
                            }
                            else
                            {
                                MyDebug.PrintError(" => Internet disponible mais chargement impossible => tentative : " + tryCount, ex);
                                new SleepWithStopCheck(Notifier).RunSecond(_sleep);
                                tryCount += 1;
                            }
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                }
            }
            else
            {
                return iAction();
            }
        }

        #endregion Public METHODS

        #region Private FIELDS

        private const int INTERNETSECONDWAITINGTIME = 5;

        private int _retries;
        private string _SiteUrl;
        private int _sleep;

        #endregion Private FIELDS
    }
}