using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyDotNetInvestigate
{
    public class XmlInvestigate
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
            var doc = new XmlDocument();
            doc.Load("D:\\temp\\20161123\\DbServiceShopQuery.config");


        }

    }


}
