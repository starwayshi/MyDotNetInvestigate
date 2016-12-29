using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    class StackTraceInvestigate
    {
        public void Execute()
        {
            var actions = new List<Action> { foo1 };
            var tasks = new List<Task>();
            actions.ForEach(o => tasks.Add(Task.Run(o)));
            Task.WaitAll(tasks.ToArray());
        }

        private void foo1()
        {
            StackTrace st = new StackTrace();
            Utility.WriteLog(st.GetFrame(1).GetMethod().Name.ToString());
        }
    }
}
