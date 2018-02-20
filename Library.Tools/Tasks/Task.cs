using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Tools.Tasks
{
    public static class MyTask
    {
        #region Public METHODS

        /// <summary>
        /// Retourne une liste de tâches async avec limitation d'action en parallèle
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="iList"></param>
        /// <param name="iMaxParallel"></param>
        /// <param name="iAction"></param>
        /// <returns></returns>
        public static async Task WhenAllWithLimitation<TIn>(List<TIn> iList, int iMaxParallel, Func<TIn, Task> iAction)
        {
            var throttler = new SemaphoreSlim(initialCount: iMaxParallel);
            var allTasks = new List<Task>();
            foreach (var item in iList.Enum())
            {
                await throttler.WaitAsync();
                allTasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        await iAction(item);
                    }
                    finally
                    {
                        throttler.Release();
                    }
                }));
            }
            await Task.WhenAll(allTasks);
        }

        /// <summary>
        /// Retourne une liste de tâches async avec limitation d'action en parallèle
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="iList"></param>
        /// <param name="iMaxParallel"></param>
        /// <param name="iAction"></param>
        /// <returns></returns>
        public static async Task<List<TOut>> WhenAllWithLimitation<TIn, TOut>(List<TIn> iList, int iMaxParallel, Func<TIn, Task<TOut>> iAction)
        {
            var throttler = new SemaphoreSlim(initialCount: iMaxParallel);
            var allTasks = new List<Task<TOut>>();
            foreach (var item in iList.Enum())
            {
                await throttler.WaitAsync();
                allTasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var actionResult = await iAction(item);
                        return actionResult;
                    }
                    finally
                    {
                        throttler.Release();
                    }
                }));
            }
            var allResult = await Task.WhenAll(allTasks);
            return allResult.ToList();
        }

        #endregion
    }
}