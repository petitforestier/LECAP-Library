using Library.Tools.Debug;
using System;
using Library.Tools.Extensions;

namespace Library.Tools.Tasks
{
    public class NotifierProgress
    {
        #region Public PROPERTIES

        /// <summary>
        /// Obtient le message détaillé avec date, itération, compteur, temps opération etc..
        /// </summary>
        public string FullMessage
        {
            get
            {
                string message = string.Empty;
                if (LevelProgressCount == 0)
                {
                    message = string.Format("[Info] {0} : {1}", DateTime.Now, Message);
                }
                else
                {
                    string counterMessage;
                    if (IncoherentCounter)
                        counterMessage = "{0}/{1}".FormatString("???",LevelProgressCount);
                    else
                        counterMessage = "{0}/{1}".FormatString(LevelProgressCounter, LevelProgressCount);

                    message = string.Format("[Info] {0} : {1}%  level: {2} => {3} => {4}", DateTime.Now, TotalPercentage, Level, counterMessage, Message);

                    if (Timer != null)
                    {
                        if (ThreadCount == 0) ThreadCount = 1;
                        if (Timer.OperationTime != TimeSpan.Zero) { message += string.Format(", tps opération {0}", Timer.OperationTime.ToString(@"dd\.hh\:mm\:ss")); };
                        message += string.Format(", tps restant niv. {0}", TimeSpan.FromTicks(Timer.GetRemainingTime(LevelProgressCounter - LevelProgressCount).Ticks / ThreadCount).ToString(@"dd\.hh\:mm\:ss"));
                    }
                }

                return IndentMessage(message, Level);
            }
        }

        public int Level { get;  set; }

        public string Message { get; set; }

        /// <summary>
        /// Retourne le pourcentage calculé par rapport au ProgressCounter et ProgressCount
        /// </summary>
        public int TotalPercentage { get; set; }

        public int LevelProgressCount { get; set; }

        public int LevelProgressCounter { get; set; }

        public bool IncoherentCounter { get; set; }

        public int ThreadCount { get; set; }

        public MyTimer Timer { get; set; }

        #endregion

        #region Private METHODS

        private string IndentMessage(string iMessage, int iLevel)
        {
            for (int a = 2; a <= iLevel; a++)
                iMessage = "   " + iMessage;
            return iMessage;
        }

        #endregion
    }
}