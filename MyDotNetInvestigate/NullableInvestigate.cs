using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    public class NullableInvestigate
    {
        /// <summary>
        /// Nullable Types 
        /// </summary>
        /// <remarks>
        /// https://msdn.microsoft.com/zh-cn/library/1t3y8s4s.aspx
        /// </remarks>
        public void Execute()
        {
            foo1(); foo2(); foo3();
        }

        private void foo1()
        {
            int? request = null;

            bool result1 = request.HasValue;
            int result2 = request.GetValueOrDefault();
            int result3 = request ?? default(int);
            int result4 = request.GetValueOrDefault(3);
            int result5 = request ?? 3;
            //var result6 = request.Value; //throw exception!
        }

        private void foo2()
        {
            bool? request = null;

            bool result1 = request.HasValue;
            bool result2 = request.GetValueOrDefault();
            bool result3 = request ?? default(bool);
            bool result4 = request.GetValueOrDefault(true);
            bool result5 = request ?? true;
            //var result6 = request.Value; //throw exception!
        }

        private void foo3()
        {
            DateTime? request = null;

            bool result1 = request.HasValue;
            DateTime result2 = request.GetValueOrDefault();
            DateTime result3 = request ?? default(DateTime);
            DateTime result4 = request.GetValueOrDefault(new DateTime(2016, 12, 15));
            DateTime result5 = request ?? new DateTime(2016, 12, 15);
            //var result6 = request.Value; //throw exception!
        }

    }
}
