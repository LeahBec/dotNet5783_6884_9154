using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
namespace BlImplementation
{
    internal class BlOrderTracking : IOrderTracking
    {
        public static BO.OrderTracking GetOrderTracking(int id)
        {
            BO.Order order = new();
            order = BLApi.Factory.get().order.GetOrderDetails(id);
            BO.OrderTracking returnOrder = new();
            returnOrder.Status = (BO.OrderStatus)order.Status;
            returnOrder.ID = order.ID;
            return returnOrder;
        }
    }
}
