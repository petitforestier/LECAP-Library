namespace Library.Tools.Thread
{
	using Library.Tools.Tasks;
    using System.Threading;
    using System.Threading.Tasks;


	public class SleepWithStopCheck : ProgressableCancellable
	{
		#region Public CONSTRUCTORS

        public SleepWithStopCheck(ProgressCancelNotifier iNotifier)
            : base(iNotifier)
		{
		}

		#endregion Public CONSTRUCTORS

		#region Public METHODS

		/// <summary>
		/// Lancement d'un arrêt de thread (sleep) avec vérification périodique d'une notication d'annulation.
		/// </summary>
		public void RunMilliSecond(int iSleepMilliSecondTime)
		{
			int neededLoop = (iSleepMilliSecondTime) / REFRESHTIMEMILLISECOND;

			for (int a = 1; a <= neededLoop; a++)
			{
                Notifier.ThrowIfCancellationRequested();
			    System.Threading.Thread.Sleep(REFRESHTIMEMILLISECOND);
			}
		}


        /// <summary>
        /// Lancement d'un arrêt de thread (sleep) avec vérification périodique d'une notication d'annulation.
        /// </summary>
        public async Task RunMilliSecondAsync(int iSleepMilliSecondTime)
        {
            int neededLoop = (iSleepMilliSecondTime) / REFRESHTIMEMILLISECOND;

            for (int a = 1; a <= neededLoop; a++)
            {
                Notifier.ThrowIfCancellationRequested();
                await Task.Delay(REFRESHTIMEMILLISECOND);
            }
        }

		/// <summary>
		/// Lancement d'un arrêt de thread (sleep) avec vérification périodique d'une notication d'annulation.
		/// </summary>
		public void RunSecond(int iSleepSecondTime)
		{
			RunMilliSecond(iSleepSecondTime * 1000);
		}

        /// <summary>
        /// Pause d'un tâche async
        /// </summary>
        /// <param name="iSleepSecondTime"></param>
        /// <returns></returns>
        public async Task RunSecondAsync(int iSleepSecondTime)
        {
            await RunMilliSecondAsync(iSleepSecondTime * 1000);
        }

		#endregion Public METHODS

		#region Private FIELDS

		private const int REFRESHTIMEMILLISECOND = 1000;

		#endregion Private FIELDS
	}
}