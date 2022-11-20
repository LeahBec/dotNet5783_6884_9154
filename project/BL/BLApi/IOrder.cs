using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLApi
{
    public interface IOrder
    {
        public IEnumerable<OrderForList> GetOrderList();
        public Order GetOrderInfoForManager(int id);
        public Order GetOrderInfoForCustomer(int id);
        public BO.Order UpdateOrderShippingForManager(int id);
        public BO.Order UpdateOrderDeliveryForManager(int id);
        //BONUS.
        public BO.Order UpdateOrderForManager(int id);

    }
}
