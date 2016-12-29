using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyDotNetInvestigate
{
    public class WebInvestigate
    {
        public void Execute()
        {
            var actions = new List<Action> { foo1 };
            var tasks = new List<Task>();
            actions.ForEach(o => tasks.Add(Task.Run(o)));
            Task.WaitAll(tasks.ToArray());
        }

        private void foo1()
        {
            string C3ImgDomain = "https://c5.yangche51.com/c3/";

            string url = "123.jpg";
            url = new Uri(new Uri(C3ImgDomain), new Uri(C3ImgDomain).LocalPath + url).AbsoluteUri;
            //originalImg = new Uri(new Uri(AppSetting.C3ImgDomain), originalImg).AbsoluteUri;
            //result.Add(new ImageUrlEntity { imgUrl = url, originalImgUrl = originalImg });



        }

    }


}
