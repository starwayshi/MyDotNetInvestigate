using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyDotNetInvestigate
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();

            tasks.Add(Task.Run(() => { new Async_2G_EAP_Investigate().Execute(); }));
            tasks.Add(Task.Run(() => { new Async_1G_APM_Investigate().Execute(); }));
            tasks.Add(Task.Run(() => { new Async_3G_TAP_Investigate().Execute(); }));
            tasks.Add(Task.Run(() => { new Async_3G_TPL_Investigate().Execute(); }));
            tasks.Add(Task.Run(() => { new VirtualInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new ReadonlyInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new MathInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new LinqInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new DateTimeInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new JsonInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new RegExpInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new StringInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new EncodeInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new ReflectionInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new StackTraceInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new DynamicInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new NullableInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new DictionaryInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new GeocoderInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new InterfaceInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new XmlInvestigate().Execute(); }));
            tasks.Add(Task.Run(() => { new WebInvestigate().Execute(); }));

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception ex)
            {
                if (ex is AggregateException)
                {
                    foreach (var e in (ex as AggregateException).Flatten().InnerExceptions)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
            finally
            {
                Console.WriteLine("Status of tasks:");
                Console.WriteLine($"{"Task Id",7} {"Status",20} {"Exception",14:N0}");

                foreach (var t in tasks)
                {
                    Console.WriteLine($"{t.Id,7} {t.Status,20} {(t.Exception == null ? "N/A" : t.Exception.Message),14}");
                }
            }

            Utility.WriteLog("done!");
            Console.WriteLine("done!");
            Console.ReadLine();
        }
    }
}
