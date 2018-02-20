using System;
using System.Threading;

namespace Library.Tools.Tasks
{
    public abstract class Cancellable
    {
        #region Public PROPERTIES

        public CancellationTokenSource CancellationTokenSource { get; private set; }
        public CancellationToken CancellationToken { get; private set; }

        #endregion

        #region Public CONSTRUCTORS

        /// <summary>
        /// Permet de créer une classe annulable
        /// </summary>
        public Cancellable(System.Threading.CancellationTokenSource iCancellationTokenSource)
        {
            CancellationTokenSource = iCancellationTokenSource;
            CancellationToken = CancellationTokenSource.Token;
        }

        #endregion
    }
}