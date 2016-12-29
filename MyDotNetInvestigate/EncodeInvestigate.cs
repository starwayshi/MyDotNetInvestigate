using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyDotNetInvestigate
{
    public class EncodeInvestigate
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
            string result = HttpUtility.UrlEncode("沪A22345");
            string result2 = HttpUtility.UrlDecode("%E8%8B%8FA11111");
            string result3 = HttpUtility.UrlEncode("坚途");
            string result4 = HttpUtility.UrlEncode("{\"Sysid\":654987,\"Uid\":\"654658\",\"Ext\":\"`1234567890-=\\[];',./~!@#$%^&*()_+|{}:\"<>?\"}");
            string result5 = HttpUtility.UrlDecode(result4);
        }


    }
}
