using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    class MathInvestigate
    {
        public void Execute()
        {
            foo1();
            foo2();
            foo3();
            foo4();
        }

        private void foo1()
        {
            decimal d = 1;
            decimal request = d / 1000;
            decimal result = request * request * request;

            int i = 130;
            var r = i / 100 * 1.0m;
            int maxUseableScore = (int)((1.3m - 0.1m) * 100);
        }

        private void foo2()
        {
            decimal request = 0;
            Utility.WriteLog("￥" + request.ToString("f0"));
            Utility.WriteLog("￥" + request.ToString("f2"));
            Utility.WriteLog(request.ToString("C"));
            Utility.WriteLog("￥" + request.ToString("#"));
        }

        private void foo3()
        {
            decimal request = 1.3665m;

            decimal result1 = Math.Round(request, 2);
            decimal result2 = Math.Ceiling(request);
            decimal result3 = Math.Floor(request);

            int result4 = (int)Math.Ceiling(39.99m * -10 / 10 * 1.0m);
        }

        private void foo4()
        {
            //10%-20%以内随机数
            decimal request = 0.1m;

            for (int i = 0; i < 10; i++)
            {
                System.Threading.Thread.Sleep(10);
                decimal result = request * 0.1m + decimal.Multiply(request * 0.1m, (decimal)new Random().NextDouble());
                result = result < 1m 
                    ? Math.Ceiling(result *100) / 100
                    : Math.Ceiling(result);

                Utility.WriteLog(result.ToString());
            }
        }

    }
}
