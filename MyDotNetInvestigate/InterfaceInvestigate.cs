using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    class InterfaceInvestigate
    {
        public void Execute()
        {
            var actions = new List<Action> { foo1 };
            var tasks = new List<Task>();
            actions.ForEach(o => tasks.Add(Task.Run(o)));
            Task.WaitAll(tasks.ToArray());
        }

        interface IGetItemBaseRequest
        {
            int ClientType { get; set; }

            bool IgnorePromotion { get; set; }
        }

        interface ISalePlatform
        {
            int SalePlatform { get; set; }

        }

        class GetItemRequest : IGetItemBaseRequest, ISalePlatform
        {
            int IGetItemBaseRequest.ClientType { get; set; }

            bool IGetItemBaseRequest.IgnorePromotion { get; set; }

            int ISalePlatform.SalePlatform { get; set; }

        }


        private void foo1()
        {
            IGetItemBaseRequest request = new GetItemRequest();
        }
    }
}
