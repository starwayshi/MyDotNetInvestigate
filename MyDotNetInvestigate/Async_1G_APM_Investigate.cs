using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    /// <summary>
    /// 异步编程模型 Asynchronous Programming Model (APM) 
    /// </summary>
    /// <remarks>
    /// APM 是在 .NET Framework 1.1 中引入的。 对于新的开发工作不再建议采用此模式。
    /// APM 的异步操作是通过名为[BeginOperationName] 和[EndOperationName] 的两个方法来实现的，这两个方法分别开始和结束异步操作 操作名称
    /// 例如，FileStream 类提供 BeginRead 和 EndRead 方法来从文件异步读取字节。 这两个方法实现了 Read 方法的异步版本。
    /// 在调用[Begin操作名称] 后，应用程序可以继续在调用线程上执行指令，同时异步操作在另一个线程上执行。 每次调用[Begin操作名称] 时，应用程序还应调用[End操作名称] 来获取操作的结果。
    /// https://msdn.microsoft.com/zh-cn/library/ms228963.aspx
    /// </remarks>
    public class Async_1G_APM_Investigate
    {
        public void Execute()
        {
            Utility.WriteLog("主线程执行开始");
            new APM_Typical().RunAsync();
            new APM_Typical().ExecuteAsync();
            Utility.WriteLog("主线程执行结束");
        }

        class APM_Typical
        {
            /// <summary>
            /// 异步执行
            /// </summary>
            public void RunAsync()
            {
                Utility.WriteLog("APM_RunAsync : start");

                //测试网址
                HttpWebRequest webRequest = HttpWebRequest.Create("http://www.yangche51.com") as HttpWebRequest;
                webRequest.BeginGetResponse(RunCallback, webRequest);
                Utility.WriteLog("APM_RunAsync : download_start");
            }

            /// <summary>
            /// 异步回调
            /// </summary>
            private static void RunCallback(IAsyncResult ar)
            {
                var source = ar.AsyncState as HttpWebRequest;
                var response = source.EndGetResponse(ar);
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string content = reader.ReadToEnd();
                        Utility.WriteLog("APM_RunAsync : result_size is {0}", content.LongCount());
                    }
                }
            }

            /// <summary>
            /// 异步执行
            /// </summary>
            public void ExecuteAsync()
            {
                //委托简单的包装了一下方法
                Func<bool, long> func = Utility.LongRunningWork;
                func.BeginInvoke(false, ExecuteCallback, null);
                Utility.WriteLog("APM_ExecuteAsync : funciton begin");
            }

            /// <summary>
            /// 异步回调
            /// </summary>
            void ExecuteCallback(IAsyncResult ar)
            {
                AsyncResult asyncResult = ar as AsyncResult;
                var delegateSource = asyncResult.AsyncDelegate as Func<bool, long>;
                try
                {
                    var result = delegateSource.EndInvoke(ar);
                    Utility.WriteLog("APM_ExecuteAsync : funciton end. result is {0}", result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

    }
}
