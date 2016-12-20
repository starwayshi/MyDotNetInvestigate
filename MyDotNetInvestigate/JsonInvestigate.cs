
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MyDotNetInvestigate
{
    public class ReadOnlyClass
    {
        public int Element1
        {
            get
            {
                return 2;
            }
        }

        public readonly int Element2;

        public int Element3;
    }

    public class JsonInvestigate
    {
        public void Execute()
        {
            var callList = new List<Action>() { foo1, foo2, foo3, foo4, foo5 };
            callList.ForEach(a => a.Invoke());
        }

        public static string SerializeJSON<T>(T instance)
        {
            string result;
            if (instance == null)
            {
                result = string.Empty;
            }
            else
            {
                JavaScriptSerializer serialize = new JavaScriptSerializer();
                result = serialize.Serialize(instance);
            }
            return result;
        }

        public static T DeSerializeJSON<T>(string jsonStr)
        {
            T result;
            if (string.IsNullOrEmpty(jsonStr))
            {
                result = default(T);
            }
            else
            {
                JavaScriptSerializer serialize = new JavaScriptSerializer();
                result = serialize.Deserialize<T>(jsonStr);
            }
            return result;
        }


        private void foo1()
        {
            object requestObject = new
            {
                MainOrderId = 123456,
                ServiceShopOrderStatus = 2,
            };
            string result1 = SerializeJSON<object>(requestObject);
            string result2 = JsonConvert.SerializeObject(requestObject);
            string result3 = requestObject.SerializeToJson();

            requestObject = null;
            string result11 = SerializeJSON<object>(requestObject);
            string result12 = JsonConvert.SerializeObject(requestObject);
            string result13 = requestObject.SerializeToJson();


            dynamic requestDynamic = new
            {
                MainOrderId = 123456,
                ServiceShopOrderStatus = 2,
            };
            string result21 = SerializeJSON<object>(requestDynamic);
            string result22 = JsonConvert.SerializeObject(requestDynamic);
            //string result23 = requestDynamic.SerializeToJson();  //未识别类型异常

            requestDynamic = null;
            string result31 = SerializeJSON<object>(requestDynamic);
            string result32 = JsonConvert.SerializeObject(requestDynamic);
            //string result33 = requestDynamic.SerializeToJson();  //null异常

            string requestString = "{\"MainOrderId\":123456,\"ServiceShopOrderStatus\":2}";
            var result41 = DeSerializeJSON<object>(requestString);
            dynamic result42 = JsonConvert.DeserializeObject<object>(requestString);
            var result43 = requestString.DeserializeToObject<object>();


            requestString = null;
            var result51 = DeSerializeJSON<object>(requestString);
            var result52 = JsonConvert.DeserializeObject<object>(requestString ?? string.Empty);
            var result53 = requestString.DeserializeToObject<object>();


        }

        private void foo2()
        {
            ReadOnlyClass requestObject = new ReadOnlyClass();
            string result1 = SerializeJSON<ReadOnlyClass>(requestObject);
            string result2 = JsonConvert.SerializeObject(requestObject);

            string requestString = "{\"Element1\":12,\"Element2\":34,\"Element3\":56}";

            var result51 = DeSerializeJSON<ReadOnlyClass>(requestString);
            var result52 = JsonConvert.DeserializeObject<ReadOnlyClass>(requestString ?? string.Empty);
        }

        private void foo3()
        {
            int request1 = 3;
            string result1 = JsonConvert.SerializeObject(request1);

            string request2 = string.Empty;
            string result2 = JsonConvert.SerializeObject(request2);

            object request3 = null;
            string result3 = JsonConvert.SerializeObject(request3);
        }

        private void foo4()
        {
            var sourceObject = new List<object>();
            sourceObject.Add(new { OrderId = 123, OrderCode = "aaaaaa" });
            sourceObject.Add(new { OrderId = 456, OrderCode = "bbbbbb" });

            var sourceString = JsonConvert.SerializeObject(sourceObject);

            //List<object> resultObject = JsonConvert.DeserializeObject<List<object>>(null);  //nullreference异常
            //object resultObject = JsonConvert.DeserializeObject<object>(null);  //nullreference异常

            var resultObject = JsonConvert.DeserializeObject(sourceString);  //nullreference异常
            //object resultObject = JsonConvert.DeserializeObject<object>(null);  //nullreference异常
            //resultObject[0].OrderCode;

            //List<object> resultObject = JsonConvert.DeserializeObject<List<object>>(sourceString);  //nullreference异常


            //resultObject.Add(new
            //{
            //    OrderId = 456,
            //    OrderCode = "558896665"
            //});

            //resultObject.Add(new
            //{
            //    OrderId = 789,
            //    OrderCode = "2568683333"
            //});

            //resultObject.RemoveAll((dynamic a) =>
            //{
            //    return a.OrderId == 456;
            //});

            //resultObject.RemoveAll( a =>
            //{
            //    return a.OrderId == 456;
            //});
            var resultString = JsonConvert.SerializeObject(resultObject);


        }

        private void foo5()
        {
            //dynamic result = new System.Dynamic.ExpandoObject();
            //result.totalCount = 12;
            dynamic result = new { totalCount = 32 };
            var result1 = JsonConvert.SerializeObject(result);
            var result2 = SerializeJSON<object>(result);
        }
        

    }
}
