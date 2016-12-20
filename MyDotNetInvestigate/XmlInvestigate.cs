using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyDotNetInvestigate
{
    public class XmlInvestigate
    {
        public void Execute()
        {
            foo1();
        }

        private void foo1()
        {
            var doc = new XmlDocument();
            doc.Load("D:\\temp\\20161123\\DbServiceShopQuery.config");


        }

    }


}
