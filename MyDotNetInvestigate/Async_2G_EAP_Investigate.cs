using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    /// <summary>
    ///  基于事件的异步模式 Event-based Asynchronous Pattern (EAP)
    /// </summary>
    /// <remarks>
    /// EAP 是在 .NET Framework 2.0 中引入的。 对于新的开发工作不再建议采用此模式。
    /// 将有一个或多个名为 "MethodNameAsync" 的方法。这些方法可能会创建同步版本的镜像，这些同步版本会在当前线程上执行相同的操作。
    /// 该类还可能有一个 "MethodNameCompleted" 事件，监听异步方法的结果。
    /// 它可能会有一个 "MethodNameAsyncCancel"（或只是 CancelAsync）方法，用于取消正在进行的异步操作。
    /// https://msdn.microsoft.com/zh-cn/library/ms228969.aspx
    /// </remarks>
    public class Async_2G_EAP_Investigate
    {
        public void Execute()
        {
            Utility.WriteLog("主线程执行开始");
            new EAP_Typical().RunAsync();
            Utility.WriteLog("主线程执行结束");
        }

        class EAP_Typical
        {
            /// <summary>
            /// 异步执行
            /// </summary>
            public void RunAsync()
            {
                Utility.WriteLog("EAP_RunAsync:start");

                using (WebClient webClient = new WebClient())
                {
                    //获取完成情况
                    webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
                    webClient.DownloadStringAsync(new Uri("http://www.yangche51.com"));
                    Utility.WriteLog("EAP_RunAsync:download_start");
                }
            }

            /// <summary>
            /// 通过事件回调
            /// </summary>
            void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
            {
                //获取返回结果
                Utility.WriteLog("EAP_RunAsync:download_completed. result_size={0}", e.Result.LongCount());
            }

        }

    }
}
