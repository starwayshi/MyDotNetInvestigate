using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    class DateTimeInvestigate
    {
        public void Execute()
        {
            foo1(); foo2(); foo3(); foo4(); foo5();
        }

        private void foo1()
        {
            string datetime1 = DateTime.Now.ToString("yyyy年M月d日");
            string datetime2 = String.Format("{0:yyyy年M月d日}", DateTime.Now);
            DateTime datetime3 = DateTime.Now.AddDays(-10);
            string datetime4 = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string datetime5 = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            string datetime6 = DateTime.Now.ToShortDateString();
            string datetime7 = DateTime.Now.ToLongDateString();
            string datetime8 = DateTime.Now.ToShortTimeString();
            string datetime9 = DateTime.Now.ToLongTimeString();
            string datetime10 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            string datetime11 = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff");


        }

        private void foo2()
        {
            DateTime request = new DateTime(2016, 3, 22, 19, 00, 00).Date;
            TimeSpan ts = request.Subtract(DateTime.Now.Date);
        }

        private void foo3()
        {
            var result = System.DateTime.Now.DayOfWeek.ToString();
        }

        private void foo4()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1);
            //double seconds = Math.Truncate(ts.TotalHours) * 60 * 60;
            double seconds = ts.TotalSeconds;
            string result = seconds.ToString().Substring(0, 8);
        }

        private void foo5()
        {
            var interval = (new DateTime(2016, 5, 27) - DateTime.Now.Date).TotalDays;
            var result = string.Format("{0:0}", interval);
            var result2 = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
        }
    }
}
