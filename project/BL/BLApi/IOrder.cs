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
        public Order GetOrderDetails(int id);
        public BO.Order UpdateOrderShipping(int id);
        public BO.Order UpdateOrderDelivery(int id);
        //BONUS.
        public BO.Order UpdateOrderForManager(int id);

    }
}
