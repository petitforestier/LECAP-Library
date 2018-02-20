using Library.Tools.Debug;
using Library.Tools.Extensions;
using Library.Tools.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Tools.Tasks
{
    public static class ProgressCancelReporter
    {
        #region Public CLASSES

        public class SubLevel : IDisposable
        {
            #region Public CONSTRUCTORS

            public SubLevel(ProgressCancelNotifier iNotifier, int iProgressCount, int iThreadCount)
            {
                lock (_notifierStateListLocker)
                {
                    _notifier = iNotifier;

                    var theLastState = NotifierStateList.Where(x => x.Notifier.Guid == iNotifier.Guid);

                    int level = 1;
                    if (theLastState.IsNotNullAndNotEmpty())
                        level = theLastState.OrderByDescending(x => x.Level).First().Level + 1;

                    var newState = new NotifierState();
                    newState.Level = level;
                    newState.Notifier = iNotifier;
                    newState.ProgressCounter = 0;
                    newState.ProgressCount = iProgressCount;
                    newState.ThreadCount = iThreadCount;

                    NotifierStateList.Add(newState);
                }
            }

            #endregion

            #region Public METHODS

            /// <summary>
            /// Reporter une progression, qui ne sera pas dans les logs
            /// </summary>
            public void Report(ReporterProgress iProgress)
            {
                lock (_notifierStateListLocker)
                {
                    var notifierProgress = new NotifierProgress();

                    NotifierState theState = GetNotifierState();

                    if (theState.ProgressCounter > iProgress.ProgressCounter)
                        notifierProgress.IncoherentCounter = true;

                    theState.ProgressCounter = iProgress.ProgressCounter;

                    notifierProgress.Message = iProgress.Message;
                    notifierProgress.LevelProgressCounter = iProgress.ProgressCounter;
                    notifierProgress.Timer = iProgress.Timer;

                    notifierProgress.LevelProgressCount = theState.ProgressCount;
                    notifierProgress.ThreadCount = theState.ThreadCount;
                    notifierProgress.Level = theState.Level;
                    notifierProgress.TotalPercentage = ComputePercentage(_notifier);
                    _notifier.Report(notifierProgress);
                    MyDebug.PrintInformation(notifierProgress);
                }
            }

            /// <summary>
            /// Reporter une progression
            /// </summary>
            public void Report(string iMessage, int iProgressCounter, MyTimer iTimer = null)
            {
                Report(new ReporterProgress() { Message = iMessage, ProgressCounter = iProgressCounter, Timer = iTimer });
            }

            /// <summary>
            /// Reporter une progression avec incrémentation
            /// </summary>
            public void ReportWithStep(string iMessage, int iStepValue, MyTimer iTimer = null)
            {
                var theState = GetNotifierState();
                Report(new ReporterProgress() { Message = iMessage, ProgressCounter = theState.ProgressCounter + iStepValue, Timer = iTimer });
            }

            /// <summary>
            /// Reporter une erreur
            /// </summary>
            public void Report(string iMessage, Exception iException)
            {
                Report(new ReporterProgress() { Message = iMessage + " " + iException.Message });
            }

            public void Dispose()
            {
                lock (_notifierStateListLocker)
                {
                    var orderedList = NotifierStateList.Enum().Where(x => x.Notifier.Guid == _notifier.Guid).Enum().OrderByDescending(x => x.Level);

                    if (orderedList.IsNullOrEmpty())
                        throw new Exception("Reporter dispose, ne peut pas être disposé");

                    var lastLevel = orderedList.First();

                    NotifierStateList.Remove(lastLevel);
                }
            }

            #endregion

            #region Private FIELDS

            private readonly ProgressCancelNotifier _notifier;

            #endregion

            #region Private METHODS

            private NotifierState GetNotifierState()
            {
                var orderedList = NotifierStateList.Enum().Where(x => x.Notifier.Guid == _notifier.Guid).Enum().OrderByDescending(x => x.Level);

                if (orderedList.IsNullOrEmpty())
                    throw new Exception("Reporter progress, il n'existe pas de notifier state");

                return NotifierStateList.Where(x => x.Notifier.Guid == _notifier.Guid).OrderByDescending(x => x.Level).First();
            }

            #endregion
        }

        #endregion

        #region Internal CLASSES

        internal class NotifierState
        {
            #region Public PROPERTIES

            public ProgressCancelNotifier Notifier { get; set; }
            public int Level { get; set; }
            public int ThreadCount { get; set; }
            public int ProgressCounter { get; set; }
            public int ProgressCount { get; set; }

            #endregion
        }

        #endregion

        #region Private FIELDS

        private static List<NotifierState> _notifierStateList;

        private static object _notifierStateListLocker = new object();

        #endregion

        #region Private PROPERTIES

        private static List<NotifierState> NotifierStateList
        {
            get
            {
                if (_notifierStateList == null)
                    _notifierStateList = new List<NotifierState>();
                return _notifierStateList;
            }
        }

        #endregion

        #region Private METHODS

        private static int ComputePercentage(ProgressCancelNotifier iNotifier)
        {
            var levelList = NotifierStateList.Where(x => x.Notifier.Guid == iNotifier.Guid).OrderBy(x => x.Level).ToList();

            decimal previousRange = 100;
            decimal result = 0;
            //bouclage sur tous les niveaux
            foreach (var levelItem in levelList.Enum())
            {
                var count = levelItem.ProgressCount;
                if (count == 0) count = 1;
                result += RecursivePercentage(previousRange, levelItem.ProgressCounter, count);
                previousRange = decimal.Divide(previousRange, count);
            }
            return (int)result;
        }

        private static decimal RecursivePercentage(decimal iCurrentPercentage, int iCounter, int iCount)
        {
            if (iCount == 0) iCount = 1;
            return Convert.ToInt32(iCurrentPercentage * decimal.Divide((iCounter), iCount));
        }

        #endregion
    }
}