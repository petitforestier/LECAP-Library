using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Tasks;
using Library.Tools.Thread;
using System.Threading;
using Library.Tools.Debug;

namespace Library.Tools.WCFRetry
{
	public class WCFRetryHelper : ProgressableCancellable
	{
		private readonly int _maxRetry;
		private readonly int _waitingTimeSecondOnNoNetwork;

        public WCFRetryHelper(ProgressCancelNotifier iNotifier, int iMaxRetry, int iWaitingTimeSecondOnNoNetwork)
            : base(iNotifier)
		{
			_maxRetry = iMaxRetry;
			_waitingTimeSecondOnNoNetwork = iWaitingTimeSecondOnNoNetwork;
		}


		public TOut SafeNetwork<TIn,TOut>(Func<TIn,TOut> iAction, TIn iArgument)
		{
			int tryCounter = 0;
			while (true)
			{
                Notifier.ThrowIfCancellationRequested();
				try
				{
					return iAction(iArgument);
				}
				catch(System.ServiceModel.EndpointNotFoundException ex)
				{
                    new SleepWithStopCheck(Notifier).RunSecond(_waitingTimeSecondOnNoNetwork);
                    MyDebug.PrintError("Le web service n'est pas connecté", ex);
				}
				catch(Exception)
				{
                    if (Notifier.IsCanceled)
						throw;

					if (tryCounter >= _maxRetry)
						throw;
					else
						tryCounter++;
                    new SleepWithStopCheck(Notifier).RunSecond(10);
				}
			}
		}

        public async Task<TOut2> SafeNetworkAsync<TIn2, TOut2>(Func<TIn2, Task<TOut2>> iAction, TIn2 iArgument)
        {
            int tryCounter = 0;
            while (true)
            {
                bool waitForNoNetwork = false;
                bool waitForError = false;

                Notifier.ThrowIfCancellationRequested();
                try
                {
                    return await iAction(iArgument);
                }
                catch (System.ServiceModel.EndpointNotFoundException ex)
                {
                    waitForNoNetwork = true;
                    MyDebug.PrintError("Le web service n'est pas connecté", ex);
                }
                catch (Exception)
                {
                    if (Notifier.IsCanceled)
                        throw;

                    if (tryCounter >= _maxRetry)
                        throw;
                    else
                        tryCounter++;
                    waitForError = true;
                }

                if (waitForNoNetwork)
                    await Task.Delay(1000 * _waitingTimeSecondOnNoNetwork, Notifier.CancellationTokenSource.Token);

                if(waitForError)
                    await Task.Delay(1000 * 10, Notifier.CancellationTokenSource.Token);
            }
        }
	}
}
