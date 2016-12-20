using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    /// <summary>
    /// 任务并行库 Task Parallel Library (TPL)
    /// </summary>
    /// <remarks>
    /// TPL 是 System.Threading 和 System.Threading.Tasks 空间中的一组公共类型和 API
    /// 从 .NET Framework 4 开始，TPL 是编写多线程代码和并行代码的首选方法
    /// https://msdn.microsoft.com/zh-cn/library/dd460717.aspx
    /// PLINQ : https://msdn.microsoft.com/zh-cn/library/dd460688.aspx
    /// </remarks>
    public class Async_3G_TPL_Investigate
    {
        public void Execute()
        {
            foo1();
        }

        IEnumerable<int> target = Enumerable.Range(100000000, 100000);

        void foo1()
        {
            Measure("LINQ", () =>
            {
                var result = target.Where(o => Utility.IsPrime(o)).Count();
                Utility.WriteLog(result.ToString());
            });

            Measure("PLINQ", () =>
            {
                var result = target.AsParallel().AsOrdered().Where(o => Utility.IsPrime(o)).Count();
                Utility.WriteLog(result.ToString());
            });
        }

        void Measure(string name, Action func)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            func();
            stopwatch.Stop();
            Utility.WriteLog("-------------------------");
            Utility.WriteLog("      {0} 耗时: {1}ms", name, stopwatch.ElapsedMilliseconds);
            Utility.WriteLog("-------------------------");
        }


    }


}
