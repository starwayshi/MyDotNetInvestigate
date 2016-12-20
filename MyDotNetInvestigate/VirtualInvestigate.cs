using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    public class VirtualInvestigate
    {
        public void Execute()
        {
            new mySubClass().TestMethod(request: "ds");
        }

        class mySubClass : myBaseClass
        {
            public override void TestMethod(string request)
            {
                Utility.WriteLog("this is test method in sub class");
                base.TestMethod(request);
            }
        }

        class myBaseClass
        {
            public virtual void TestMethod(string request)
            {
                Utility.WriteLog("this is test method in base class");
            }
        }
    }
}