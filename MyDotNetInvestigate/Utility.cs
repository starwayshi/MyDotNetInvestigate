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
    class Utility
    {
        //public static void WriteLog(string message)
        //{
        //    Debug.WriteLine(string.Format("{0} ThreadId {1} {2}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"), Thread.CurrentThread.ManagedThreadId, message));
        //}

        public static void WriteLogAsync(string format, params object[] arg)
        {
            Task.Run(() =>
            {
                Debug.WriteLine(string.Format("{0}:yyyy/MM/dd HH:mm:ss fff ", DateTime.Now) + string.Format(format, arg));
            });
        }

        public static void WriteLog(string format, params object[] arg)
        {
            Debug.WriteLine(string.Format("{0:yyyy/MM/dd HH:mm:ss fff} ThreadId {1:00} ", DateTime.Now, Thread.CurrentThread.ManagedThreadId) + string.Format(format, arg));
        }

        /// <summary>
        /// 是否是素数
        /// </summary>
        public static bool IsPrime(long x)
        {
            if (x <= 2) return x == 2;
            if (x % 2 == 0) return false;

            long sqrtx = (long)Math.Ceiling(Math.Sqrt(x));
            for (long i = 3; i <= sqrtx; i++)
            {
                if (x % i == 0) return false;
            }
            return true;
        }

        /// <summary>
        /// 长时间, 高CPU损耗函数
        /// </summary>
        public static long LongRunningWork(bool throwException)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            WriteLog("LongTimeMethod begin");
            try
            {
                IEnumerable<int> target = Enumerable.Range(100000000, 500000);
                var result = target.Where(o => IsPrime(o)).LongCount();
                if (throwException) throw new Exception("LongTimeMethod Exception!");
                return result;
            }
            finally
            {
                stopwatch.Stop();
                WriteLog("LongTimeMethod end. 耗时 {0}ms", stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// 异步访问网站
        /// </summary>
        public static async Task<long> AccessWebAsync(bool throwException)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            WriteLog("AccessWebMethod begin");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Task<string> getStringTask = client.GetStringAsync("http://www.yangche51.com");

                    string urlContents = await getStringTask;

                    if (throwException) throw new Exception("AccessTheWebAsync exception!!!");

                    return urlContents.Length;
                };
            }
            finally
            {
                stopwatch.Stop();
                WriteLog("AccessWebMethod end. 耗时 {0}ms", stopwatch.ElapsedMilliseconds);
            }
        }


    }
}
