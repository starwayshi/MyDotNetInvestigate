using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    public class ReflectionInvestigate
    {

        public void Execute()
        {
            var actions = new List<Action> { foo1 };
            var tasks = new List<Task>();
            actions.ForEach(o => tasks.Add(Task.Run(o)));
            Task.WaitAll(tasks.ToArray());
        }

        private class MyInvoker
        {
            public static RT InvokeExecute1<RT>(Func<RT> method)
            {
                Utility.WriteLog("Call InvokeExecute1");
                return method.Invoke();
            }


            public static T InvokeExecute2<T>(object request, Func<T> method)
            {
                Utility.WriteLog("Call InvokeExecute2");
                Utility.WriteLog("request is " + request.ToString());
                return method.Invoke();
            }
        }

        private void foo1()
        {
            string result1 = MyInvoker.InvokeExecute1<string>(() =>
            {
                return "test1";
            });

            var request1 = "request";
            string result2 = MyInvoker.InvokeExecute2<string>(request1, () =>
            {
                return "test2";
            });
        }
    }
}
