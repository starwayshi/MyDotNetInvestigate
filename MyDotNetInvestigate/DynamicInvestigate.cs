using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    class DynamicInvestigate
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
            dynamic mailParameters = new ExpandoObject();
            mailParameters.Name = "starway";
            mailParameters.Address = "Shanghai China";

            mailParameters.MobileList = new List<object>();
            mailParameters.MobileList.Add(new 
            {
                mobile1 = 13666985587
            });
            mailParameters.MobileList.Add(new
            {
                mobile2 = 22222
            });
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(mailParameters);
        }


    }
}
