using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Azure
{
    /// <summary>
    /// Model for Workers
    /// </summary>
    public class WorkerEntryPoint
    {
        /// <summary>
        /// OnStart method for workers
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>bool for success</returns>
        public virtual async Task<bool> OnStart(CancellationToken cancellationToken)
        {
            await Task.FromResult(0);
            return true;
        }

        /// <summary>
        /// Run method
        /// </summary>
        public virtual async Task Run()
        {
            await Task.FromResult(0);
        }

        /// <summary>
        /// This method prevents unhandled exceptions from being thrown
        /// from the worker thread.
        /// </summary>
        internal async Task ProtectedRun()
        {
            try
            {
                // Call the Workers Run() method
                await Run();
            }
            catch (SystemException)
            {
                // Exit Quickly on a System Exception
                throw;
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }
    }
}
