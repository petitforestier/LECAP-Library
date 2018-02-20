using Library.Tools.Debug;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Library.Tools.Tasks
{
    public class ProgressCancelNotifier
    {
        #region Public PROPERTIES

        /// <summary>
        /// Obtient le token d'annulation. Permet de donner ce token à un autre notifier afin de lier les annulations
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; private set; }

        /// <summary>
        /// Retourne si une annulation a été demandée
        /// </summary>
        public bool IsCanceled
        {
            get
            {
                return CancellationTokenSource.Token.IsCancellationRequested;
            }
        }

        /// <summary>
        /// Nom du notifier pour afficher si besoin
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Guid d'identification du notifier
        /// </summary>
        public string Guid { get; private set; }

        #endregion

        #region Public CONSTRUCTORS

        /// <summary>
        /// Classe à utiliser dans les tâches, pour retourner une progression et permettre l'annulation, Ajouter dans les boucles une vérification de IsCancellationRequested
        /// </summary>
        /// <param name="iProgressAction">Action à executer lors d'une progression, ex : Action&lt;NotifierProgress&gt; progressAction = (value) => {textBox1.Text = value.ProgressCount + " " + value.Message; }; </param>
        public ProgressCancelNotifier(Action<NotifierProgress> iProgressAction, string iName = null)
        {
            CancellationTokenSource = new System.Threading.CancellationTokenSource();
            Name = iName;
            SetProgressAction(iProgressAction);
            Guid = new Guid().ToString();
        }

        /// <summary>
        /// Classe à utiliser dans les tâches, pour retourner une progression et permettre l'annulation. Ajouter dans les boucles une vérification de IsCancellationRequested. Action de progression est par défaut une ligne en débug.
        /// </summary>
        public ProgressCancelNotifier(string iName = null)
        {
            CancellationTokenSource = new System.Threading.CancellationTokenSource();
            Name = iName;
            //Action<NotifierProgress> theAction = (NotifierProgress iProgress) => { };

            Action<NotifierProgress> theAction = (value) => { MyDebug.PrintInformation(value); };
            SetProgressAction(theAction);
            Guid = System.Guid.NewGuid().ToString();
        }

        #endregion

        #region Public METHODS

        /// <summary>
        /// Attache les notifications d'annulation, Ils seront tous levés lors l'annulation de celle çi
        /// </summary>
        public void AttachNotifierCancellation(ProgressCancelNotifier iProgressCancelNotifier)
        {
            if (_attachedCancellationTokenSourceList == null)
                _attachedCancellationTokenSourceList = new List<CancellationTokenSource>();
            _attachedCancellationTokenSourceList.Add(iProgressCancelNotifier.CancellationTokenSource);
        }

        /// <summary>
        /// Arrête la tâche et ses sous tâches
        /// </summary>
        public void Cancel()
        {
            foreach (var cancelItem in _attachedCancellationTokenSourceList.Enum())
            {
                cancelItem.Cancel();
            }

            if (CancellationTokenSource != null)
                CancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Lève une exception si annulation demandée
        /// </summary>
        public void ThrowIfCancellationRequested()
        {
            CancellationTokenSource.Token.ThrowIfCancellationRequested();
        }

        /// <summary>
        /// Report un changement dans le notifier
        /// </summary>
        /// <param name="iProgress"></param>
        public void Report(NotifierProgress iProgress)
        {
            _progression.Report(iProgress);
        }

        /// <summary>
        /// Défini l'action lors d'une progression
        /// </summary>
        public void SetProgressAction(Action<NotifierProgress> iProgressAction)
        {
            //Progression
            var progressHandler = new Progress<NotifierProgress>(iProgressAction);
            _progression = progressHandler as IProgress<NotifierProgress>;
        }

        #endregion

        #region Private FIELDS

        private List<CancellationTokenSource> _attachedCancellationTokenSourceList;
        private IProgress<NotifierProgress> _progression;

        #endregion
    }
}