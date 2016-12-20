using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    class DictionaryInvestigate
    {
        private Dictionary<int, int> _advDict = new Dictionary<int, int>(){
        {0,101357},    //热门推荐
        {49,101358},    //保养配件
        {61,101359},    //油品/化学品
        {249,101360},    //深度养护品
        {199,101361},    //车灯/照明
        {72,101362},    //汽车用品
        {81,101363},    //维修配件
        {94,101364},    //轮胎及相关
        {272,101365},    //汽车工具
    };

        public void Execute()
        {
            foo1(); foo2();
        }

        private void foo1()
        {
            string searchResult = string.Empty;
            string extendPropertyResult = string.Empty;
            var itemIds = searchResult.Split(',').Intersect(extendPropertyResult.Split(',')).Where(o => !string.IsNullOrEmpty(o));

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(_advDict);

            var dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, int>>("{0:101357,49:101358,61:101359,249:101360,199:101361,72:101362,81:101363,94:101364,272:101365}")
                ?? new Dictionary<int, int>();
        }

        private void foo2()
        {
            var request = new Dictionary<string, string>{
                {"aaa","123"},
                {"bbb",null},
                //{null,null},
            };
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(request);
        }



    }
}
