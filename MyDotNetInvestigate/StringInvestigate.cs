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
    public class StringInvestigate
    {

        public void Execute()
        {
            var actions = new List<Action> { foo1, foo2, foo3, foo4, foo5, foo6 };
            var tasks = new List<Task>();
            actions.ForEach(o => tasks.Add(Task.Run(o)));
            Task.WaitAll(tasks.ToArray());
        }

        enum OrderByType
        {
            /// <summary>
            /// 价格升序
            /// </summary>
            PriceAsc = 1,

            /// <summary>
            /// 价格降序
            /// </summary>
            PriceDesc = 2,

            /// <summary>
            /// 上架时间
            /// </summary>
            [Description("上架时间")]
            OnSaleTime = 3,

            /// <summary>
            /// 原厂等级
            /// </summary>
            FactoryGrade = 10
        }

        private void foo1()
        {
            string request = "123";
            string result1 = Regex.Replace(request, @"(?<=[\d]{3})\d(?=[\d]{4})", "*");
            string result2 = OrderByType.FactoryGrade.ToString();
            string result3 = request.Substring(request.Length - 4 > 0 ? request.Length - 4 : 0);
            string result4 = new Uri(new Uri("http://c1.testimg.cn/"), "test.jpg").AbsoluteUri;

            List<int> r = null;
            string result5 = string.Join(",", r ?? new List<int>());

        }

        private void foo2()
        {
            string result1 = GetDescriptionByEnum<OrderByType>((OrderByType)12);

            int a = 150;
            decimal b = 12.12m;
            DateTime c = new DateTime(2016, 12, 19);
            dynamic d = new System.Dynamic.ExpandoObject();
            object e = null;

            string result2 = $"{a}-{b}-{c:yyyy/MM/dd mm:ss fff}-{d}-{e}";
            string result3 = string.Format("{0}-{1}-{2:yyyy/MM/dd mm:ss fff}-{3}-{4}", a, b, c, d, e);

            string result4 = $"¥{b - a * 1.1m / 100 * 1.0m:f2}+{a}积分";

            string result5 = string.Concat(new List<int> { 1, 2, 3, 4, 5 });

        }

        public static string GetDescriptionByEnum<T>(T defaultenum)
        {
            try
            {
                Type type = defaultenum.GetType();
                FieldInfo fieldInfo = type.GetField(defaultenum.ToString());

                if (fieldInfo == null)
                    return string.Empty;

                DescriptionAttribute da = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

                if (fieldInfo == null)
                    return string.Empty;

                if (da != null)
                {
                    return da.Description;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }

        }

        private void foo3()
        {
            string source = "今天真高兴；;";
            string result = source.TrimEnd(';', '；');

            decimal dv = 12m;
            string dvs1 = dv.ToString("f2");
            string dvs2 = string.Format("{0:f2}", dv);
        }

        private void foo4()
        {
            string source = "|462933.jpg|462934.jpg|462935.jpg|462936.jpg|462937.jpg";
            var splitResult = source.Split(new string[] { "|" }, options: StringSplitOptions.RemoveEmptyEntries);
            if (splitResult.Any())
            {
                string result = splitResult.FirstOrDefault();
            }

        }

        private void foo5()
        {
            dynamic root = new System.Dynamic.ExpandoObject();

            int? expend = null;

            root.expend = expend;

            root = null;

            int result = root == null ? 0 : root.expend ?? 0;
        }

        private void foo6()
        {
            string keyword = "123%4_56";

            string result1 = keyword.Replace("%", "[%]").Replace("_", "[_]");

            string a1 = null;
            string a2 = "aa";
            string a3 = a1 == a2 ? a1 : (a1 + Environment.NewLine + a2).Trim();

            string result2 = $@"(@Table_MaxID{a1}
                                   ,{a2}--[SaleOrderId]
                                   ,{a3}--[OrderID]
                                   ,GETDATE())";
            string result3 = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {a2} <br/>";

        }

    }
}
