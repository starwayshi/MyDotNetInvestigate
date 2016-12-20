using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    static class ExtensionInvestigate
    {
        /// <summary>
        /// 将对象序列化成JSON字符串
        /// </summary>
        public static string SerializeToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将字符串实例化成对象
        /// </summary>
        public static T DeserializeToObject<T>(this string obj)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(obj ?? string.Empty);
        }
    }
}
