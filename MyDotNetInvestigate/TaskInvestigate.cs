using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    class TaskInvestigate
    {
        public void Execute()
        {
            try
            {
                Task<string> quickTask = new Task<string>(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Utility.WriteLog("QucikTask Running. Task ID: {0} Thread ID:{1} Count:{2}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId, i);
                        Thread.Sleep(10);
                    }
                    return "starway test";
                });

                Task slowTask = new Task(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Utility.WriteLog("SlowTask Running. Task ID: {0} Thread ID:{1} Count:{2}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId, i);
                        Thread.Sleep(10);
                    }
                });

                quickTask.Start();
                slowTask.Start();

                //main(very quick)
                for (int i = 0; i < 10; i++)
                {
                    Utility.WriteLog("Running in main thread.");
                    Thread.Sleep(10);
                }

                System.Threading.Tasks.Task.WaitAll(quickTask, slowTask);

                Utility.WriteLog("QuickTask Result is {0}", quickTask.Result);
            }
            catch (AggregateException ex)
            {
                Utility.WriteLog("Exception Catched! {0}", string.Join(Environment.NewLine, ex.InnerExceptions.Select(l => l.Message).ToList()));
            }
            catch (Exception ex)
            {
                Utility.WriteLog("Exception Catched! {0}", ex.Message);
            }
        }
    }
}
