using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLApi;
using BO;

namespace BlImplementation
{
    internal class BlOrder : IOrder
    {
        

        public Order GetOrderInfoForManager(int id)
        {

            throw new NotImplementedException();
        }

        public IEnumerable<OrderForList> GetOrderList()
        {
            throw new NotImplementedException();
        }

        public Order UpdateOrderDeliveryForManager(int id)
        {
            throw new NotImplementedException();
        }

        public Order UpdateOrderForManager(int id)
        {
            throw new NotImplementedException();
        }

        public Order UpdateOrderShippingForManager(int id)
        {
            throw new NotImplementedException();
        }
    }
}
