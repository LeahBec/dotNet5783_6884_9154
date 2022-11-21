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
        

        public Order GetOrderItem(int id)
        {
            IEnumerable<Dal.DO.Order> existingOrdersList = Dal.DO.Order.get;

            List<OrderForList> ordersList = new List<OrderForList>();

            List<ProductForList> productList = new List<ProductForList>();
            throw new NotImplementedException();
        }

        public IEnumerable<OrderForList> GetOrderList()
        {
            throw new NotImplementedException();
        }

        public Order UpdateOrderDelivery(int id)
        {
            throw new NotImplementedException();
        }
        public Order UpdateOrderShipping(int id)
        {
            throw new NotImplementedException();
        }
        //bonus
        public Order UpdateOrderForManager(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
