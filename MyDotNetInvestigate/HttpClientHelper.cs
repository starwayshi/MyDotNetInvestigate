using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace MyDotNetInvestigate
{

    /// <summary>
    /// 网络请求帮助类
    /// </summary>
    public static class HttpClientHelper
    {

        /// <summary>
        /// GET方式获取数据
        /// </summary>
        /// <typeparam name="RT"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static RT Get<RT>(string url)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            RT result = default(RT);
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    result = JsonConvert.DeserializeObject<RT>(s ?? string.Empty);
                }
            }
            return result;

        }


        /// <summary>
        /// 以POST方式发送请求
        /// </summary>
        /// <typeparam name="RT">返回数据类型</typeparam>
        /// <typeparam name="T">发送数据类型</typeparam>
        /// <param name="url">发送地址</param>
        /// <param name="postData">发送的数据</param>
        /// <returns>反馈数据</returns>
        public static RT Post<RT, T>(string url, T postData)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            RT result = default(RT);
            using (HttpClient httpClient = new HttpClient())
            {
                System.Diagnostics.Debug.WriteLine("API-POST数据：" + JsonConvert.SerializeObject(postData));
                HttpResponseMessage response = httpClient.PostAsJsonAsync(url, postData).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    result = JsonConvert.DeserializeObject<RT>(s ?? string.Empty);
                }
            }
            return result;
        }

        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetResponse(string url)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }

        /// <summary>
        /// Get获得数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T GetResponse<T>(string url) where T : class,new()
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            T result = default(T);

            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;

                result = JsonConvert.DeserializeObject<T>(s ?? string.Empty);
            }
            return result;
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        public static string PostResponse(string url, string postData)
        {

            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }

        /// <summary>
        /// 发起post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">url</param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        public static T PostResponse<T>(string url, string postData) where T : class,new()
        {

            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();

            T result = default(T);

            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;

                result = JsonConvert.DeserializeObject<T>(s ?? string.Empty);
            }
            return result;
        }

        /// <summary>
        /// V3接口全部为Xml形式，故有此方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T PostXmlResponse<T>(string url, string xmlString) where T : class,new()
        {

            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpContent httpContent = new StringContent(xmlString);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();

            T result = default(T);

            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;

                result = XmlDeserialize<T>(s);
            }
            return result;
        }

        /// <summary>
        /// 反序列化Xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(string xmlString) where T : class,new()
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xmlString))
                {
                    return (T)ser.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("XmlDeserialize发生异常：xmlString:" + xmlString + "异常信息：" + ex.Message);
            }

        }

        /// <summary>
        /// 将键值转换成URL
        /// </summary>
        /// <param name="data"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string DataToUrl(this IEnumerable<KeyValuePair<string, string>> data, string url)
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

            var sb = new StringBuilder(url);
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
                sb.Append(Uri.EscapeDataString(item.Key) + "=" + Uri.EscapeDataString(item.Value));
            }
            return sb.ToString();
        }

        public static string PostPage(string requestUrl, Dictionary<string, string> postParams)
        {
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            return PostPage(request, postParams, Encoding.UTF8);
        }

        public static string PostPage(HttpWebRequest request, Dictionary<string, string> postParams)
        {
            return PostPage(request, postParams, Encoding.UTF8);
        }

        public static string PostPage(HttpWebRequest request, Dictionary<string, string> postParams, Encoding pageEncoding)
        {
            string srcString = "";

            // 要提交的字符串数据。格式形如:user=uesr1&password=123
            string postString = "";
            foreach (string key in postParams.Keys)
            {
                postString += key + "=" + postParams[key] + "&";
            }
            postString = postString.Substring(0, postString.Length - 1);

            // 将提交的字符串数据转换成字节数组
            byte[] postData = pageEncoding.GetBytes(postString);

            // 设置提交的相关参数
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;

            // 提交请求数据
            System.IO.Stream outputStream = request.GetRequestStream();
            outputStream.Write(postData, 0, postData.Length);
            outputStream.Close();

            // 接收返回的页面
            HttpWebResponse response;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            System.IO.Stream responseStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
            srcString = reader.ReadToEnd();

            return srcString;
        }

        public static string PostPage(string Url, string Data)
        {
            byte[] data = Encoding.UTF8.GetBytes(Data);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            byte[] responseData = webClient.UploadData(Url, "POST", data);//得到返回字符流  
            return Encoding.UTF8.GetString(responseData);//解码  
        }

        public static string PostJson(string requestUrl, string body)
        {
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            // 将提交的字符串数据转换成字节数组
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(body);

            // 设置提交的相关参数
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "application/json";
            request.ContentLength = postData.Length;

            // 提交请求数据
            System.IO.Stream outputStream = request.GetRequestStream();
            outputStream.Write(postData, 0, postData.Length);
            outputStream.Close();

            // 接收返回的页面
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            System.IO.Stream responseStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
            var srcString = reader.ReadToEnd();

            return srcString;
        }


    }
}
