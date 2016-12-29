using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    class RegExpInvestigate
    {
        public void Execute()
        {
            var actions = new List<Action> { foo1, foo2 };
            var tasks = new List<Task>();
            actions.ForEach(o => tasks.Add(Task.Run(o)));
            Task.WaitAll(tasks.ToArray());
        }

        private void foo1()
        {
            string input = "aaa" + Environment.NewLine + "bbb" + "\r\r" + "ccc" + "\n\n" + "ddd" + "\r\n" + "eee" + "\n\r" + "fff";
            string[] result1 = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string[] result2 = Regex.Split(input, "[\r\n]+");

            string mobile = "12345678901";
            bool mobileResult = Regex.IsMatch(mobile, @"^(1)[0-9]{10}$");
        }

        private void foo2()
        {
            string request = "622832520512";
            string result = Regex.Replace(request, @"(.{4})", "$0 ").Trim();

        }



    }
}
