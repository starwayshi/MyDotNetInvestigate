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
            //new Async_2G_EAP_Investigate().Execute();
            //new Async_1G_APM_Investigate().Execute();
            //new Async_3G_TAP_Investigate().Execute();
            //new Async_3G_TPL_Investigate().Execute();


            //new VirtualInvestigate().Execute();
            //new TaskInvestigate().Execute();
            //new ReadonlyInvestigate().Execute();
            //new MathInvestigate().Execute();
            //new LinqInvestigate().Execute();
            //new DateTimeInvestigate().Execute();
            //new JsonInvestigate().Execute();
            //new RegExpInvestigate().Execute();
            new StringInvestigate().Execute();
            //new EncodeInvestigate().Execute();
            //new ReflectionInvestigate().Execute();
            //new StackTraceInvestigate().Execute();
            //new DynamicInvestigate().Execute();
            //new NullableInvestigate().Execute();
            //new DictionaryInvestigate().Execute();
            //new GeocoderInvestigate().Execute();
            //new InterfaceInvestigate().Execute();
            //new XmlInvestigate().Execute();
            //new WebInvestigate().Execute();

            Utility.WriteLog("done!");
            Console.WriteLine("done!");
            Console.ReadLine();
        }
    }
}
