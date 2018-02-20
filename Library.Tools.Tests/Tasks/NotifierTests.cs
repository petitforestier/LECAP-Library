using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Tools.Tasks;
using Library.Tools.Debug;
using System.Threading.Tasks;
using System.Threading;

namespace Library.Tools.Tests.Tasks
{
    [TestClass]
    public class NotifierTests
    {

        [TestMethod]
        public async Task  Notifier()
        {
           
                var notifier = new ProgressCancelNotifier();
                var worker = new Worker(notifier);

                var task1 = Task.Run(() => worker.Work1(),notifier.CancellationTokenSource.Token);

                await Task.Delay(1000);

                notifier.Cancel();

                await task1;          

        }

    }

    internal class Worker : ProgressableCancellable
    {
        public Worker(ProgressCancelNotifier iNotifier)
            : base(iNotifier)
        { }

        public async Task Work1()
        {
            try
            {
                using (var level1 = new ProgressCancelReporter.SubLevel(Notifier, 10, 1))
                {
                    for (int a = 1; a <= 10; a++)
                    {
                        using (var level2 = new ProgressCancelReporter.SubLevel(Notifier, 110, 1))
                        {
                            int counter = 0;
                            for (int b = 1; b <= 100; b++)
                            {
                                Notifier.ThrowIfCancellationRequested();
                                await Task.Delay(10);
                                counter++;
                                level2.Report("report", counter);

                            }

                            for (int c = 1; c <= 10; c++)
                            {
                                await Task.Delay(10);
                                counter++;
                                level2.Report("report", counter);
                            }
                        }

                        level1.Report("report", a);
                    }
                }
            }
            catch (Exception)
            {
                if (Notifier.IsCanceled == false)
                    throw;
            }


        }
    }
}
