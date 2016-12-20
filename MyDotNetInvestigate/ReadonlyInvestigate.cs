using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    public class ReadonlyInvestigate
    {
        public ReadonlyInvestigate()
        {
            _OrderDataInfo = new OrderInfo();
        }

        public void Execute(){
            _OrderDataInfo.UserId = 123;
        }

        class OrderInfo
        {
            public string UserName { get; set; }
            public int UserId { get; set; }
        }

        readonly OrderInfo _OrderDataInfo;

        OrderInfo OrderDataInfo { get { return _OrderDataInfo; } }

    }
}
