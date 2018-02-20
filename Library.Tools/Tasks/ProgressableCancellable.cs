using System;

namespace Library.Tools.Tasks
{
    public abstract class ProgressableCancellable
    {
        #region Public PROPERTIES

        public ProgressCancelNotifier Notifier { get; private set; }

        #endregion

        #region Public CONSTRUCTORS

        /// <summary>
        /// Permet de créer une classe annulable
        /// </summary>
        public ProgressableCancellable(ProgressCancelNotifier iNotifier)
        {
            Notifier = iNotifier;
        }

        #endregion
    }
}