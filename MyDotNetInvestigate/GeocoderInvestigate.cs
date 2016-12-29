using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    public class GeocoderInvestigate
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
            using (var client = new System.Net.Http.HttpClient())
            {
                //根据地址取得经纬度
                var urlData = new List<KeyValuePair<string, string>>();
                var QuestUrl = "http://api.map.baidu.com/geocoder/v2/";
                urlData.Add(new KeyValuePair<string, string>("address", "上海市上海市"));
                urlData.Add(new KeyValuePair<string, string>("output", "json"));
                urlData.Add(new KeyValuePair<string, string>("ak", "4faa6933eb9a512550af33f892af58d0"));
                urlData.Add(new KeyValuePair<string, string>("city", "上海市"));

                string findurl = DataToUrl(QuestUrl, urlData);
                //执行结果
                var response = client.GetStringAsync(findurl).Result;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);

                var dataResult = result["result"] is Newtonsoft.Json.Linq.JObject;
                if (dataResult)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(response);
                }
            }
        }

        private void foo2()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var urlData = new List<KeyValuePair<string, string>>();
                var QuestUrl = "http://api.map.baidu.com/geocoder/v2/";
                //根据经纬度取得地址
                urlData.Add(new KeyValuePair<string, string>("ak", "4faa6933eb9a512550af33f892af58d0"));
                urlData.Add(new KeyValuePair<string, string>("location", "31.298696082678" + "," + "121.32276766685"));
                urlData.Add(new KeyValuePair<string, string>("coordtype", "wgs84ll"));
                urlData.Add(new KeyValuePair<string, string>("pois", "0"));
                urlData.Add(new KeyValuePair<string, string>("output", "json"));

                string findurl = DataToUrl(QuestUrl, urlData);
                //执行结果
                var response = client.GetStringAsync(findurl).Result;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);

                //var test1 = result.status == "OK";
                //var test2 = result.result is Newtonsoft.Json.Linq.JObject;
                //var test3 = result.abc;
            }
        }


        private void foo3()
        {
            var request = new List<string>()
            {
                "111.8.130.68"
            };

            foreach (var oneIP in request)
            {
                //根据IP地址获得位置信息
                using (var client = new System.Net.Http.HttpClient())
                {
                    var urlData = new List<KeyValuePair<string, string>>();
                    var QuestUrl = "http://api.map.baidu.com/location/ip";
                    //坐标系统需要的数据
                    urlData.Add(new KeyValuePair<string, string>("ak", "4faa6933eb9a512550af33f892af58d0"));
                    urlData.Add(new KeyValuePair<string, string>("ip", oneIP));
                    urlData.Add(new KeyValuePair<string, string>("coor", "bd09ll"));
                    string findurl = DataToUrl(QuestUrl, urlData);
                    var response = client.GetStringAsync(findurl).Result;
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(response);
                    Utility.WriteLog("{0} : {1}", oneIP, result["address"].ToString());
                }
            }

        }

        public static string DataToUrl(string url, IEnumerable<KeyValuePair<string, string>> data)
        {
            if (data == null)
            {
                return url;
            }

            bool first = true;


            if (url.Contains("?"))
            {
                first = false;
            }

            var sb = new System.Text.StringBuilder(url);
            foreach (var item in data)
            {
                if (first)
                {
                    sb.Append('?');
                    first = false;
                }
                else
                {
                    sb.Append('&');
                }
                sb.Append(item.Key + "=" + item.Value);
            }
            return sb.ToString();
        }

    }
}
