using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    /// <summary>
    /// 基于任务的异步模式 Task-based Asynchronous Pattern (TAP)
    /// </summary>
    /// <remarks>
    /// TAP 是在 .NET Framework 4 中引入的，并且它是在 .NET Framework 中进行异步编程的推荐使用方法。
    /// 基于任务的编程模型（TAP，Task-based Asynchronous Pattern）
    /// https://msdn.microsoft.com/zh-cn/library/hh873175.aspx
    /// </remarks>
    public class Async_3G_TAP_Investigate
    {
        public async void Execute()
        {
            try
            {
                Utility.WriteLog("主线程执行开始");
                await new MyAsyncInvestigateClass().RunAsync();
                //new MyAsyncInvestigateClass().RunAsync().Wait();
                //new MyAsyncInvestigateClass().Run();
                Utility.WriteLog("主线程执行结束");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + (ex.InnerException == null ? string.Empty : ex.InnerException.Message));
                throw ex;
            }
        }

        public class MyAsyncInvestigateClass
        {
            public async Task RunAsync()
            {
                Utility.WriteLog("TAP_RunAsync start");

                //异步执行
                Utility.WriteLog("主线程异步调用LongTimeMethodAsync begin");
                var LongTimeMethodTask = LongTimeMethodAsync(false);
                Utility.WriteLog("主线程异步调用LongTimeMethodAsync end");

                //异步执行
                Utility.WriteLog("主线程异步调用AccessTheWebAsync begin");
                var AccessTheWebTask = Utility.AccessWebAsync(false);
                Utility.WriteLog("主线程异步调用AccessTheWebAsync end");

                Utility.WriteLog("主线程伴随异步调用同时执行独立作业 begin");
                Task.Delay(1000).Wait();
                Utility.WriteLog("主线程伴随异步调用同时执行独立作业 end");

                //throw new Exception("主线程异常!!");

                var result1 = await LongTimeMethodTask;
                Utility.WriteLog("主线程异步调用LongTimeMethodAsync 结果 is " + result1);

                var result2 = await AccessTheWebTask;
                Utility.WriteLog($"主线程异步调用AccessTheWebAsync 结果 is {result2}");

                Utility.WriteLog("TAP_RunAsync finish");
            }

            public void Run()
            {
                Utility.WriteLog("TAP_Run start");

                //异步执行
                Utility.WriteLog("主线程异步调用LongTimeMethodAsync begin");
                var LongTimeMethodTask = LongTimeMethodAsync(false);
                Utility.WriteLog("主线程异步调用LongTimeMethodAsync end");

                //异步执行
                Utility.WriteLog("主线程异步调用AccessTheWebAsync begin");
                var AccessTheWebTask = Utility.AccessWebAsync(false);
                Utility.WriteLog("主线程异步调用AccessTheWebAsync end");

                Utility.WriteLog("主线程伴随异步调用同时执行独立作业 耗时1秒 begin");
                Task.Delay(1000).Wait();
                Utility.WriteLog("主线程伴随异步调用同时执行独立作业 耗时1秒 end");

                //throw new Exception("主线程异常!!");

                var result1 = LongTimeMethodTask.Result;
                Utility.WriteLog("主线程异步调用LongTimeMethodAsync 结果 is " + result1);

                var result2 = AccessTheWebTask.Result;
                Utility.WriteLog("主线程异步调用AccessTheWebAsync 结果 is " + result2);

                Utility.WriteLog("TAP_Run finish");
            }


            //正确的异步函数
            Task<long> LongTimeMethodAsync(bool throwException)
            {
                return Task.Run(() =>
                {
                    return Utility.LongRunningWork(throwException);
                });
            }

            //伪异步
            async Task<long> LongTimeMethodAsync_fake(bool throwException)
            {
                return await new Task<long>(() =>
                {
                    return Utility.LongRunningWork(throwException);
                });
            }
        }


    }
}
