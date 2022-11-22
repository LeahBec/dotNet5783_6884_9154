
using BO;
using DalApi;


namespace BlImplementation
{
    internal class BlOrder : BLApi.IOrder
    {
        private IDal Dal = new Dal.DalList();

        public IEnumerable<OrderForList> GetOrderList()
        {
            IEnumerable<Dal.DO.Order> existingOrdersList = Dal.Order.GetAll();

            List<OrderForList> ordersList = new List<OrderForList>();

            foreach (var item in existingOrdersList)
            {
                OrderForList o = new OrderForList();
                o.ID = item.OrderID;
                o.CustomerName = item.CustomerName;
                ordersList.Add(o);
            }
            if (ordersList.Count() == 0)
                throw new NotImplementedException();
            return ordersList;
        }



        public Order GetOrderDetails(int id)
        {
            if (id < 0)
                throw new NotImplementedException();
            Dal.DO.Order o = Dal.Order.Get(id);
            Order o1 = new Order();
            o1.ID = o.OrderID;
            o1.OrderDate = o.OrderDate;
            o1.ShipDate = o.ShipDate;
            o1.CustomerAdress = o.CustomerAdress;
            o1.CustomerEmail = o.CustomerEmail;
            o1.CustomerName = o.CustomerName;
            o1.DeiveryDate = o.DeliveryDate;
            o1.TotalPrice = 0;
            if (o1.CustomerName != null)
                return o1;
            return null;
        }
        void setShip(Dal.DO.Order o)
        {
            o.ShipDate = DateTime.Now;

        }
        public BO.Order UpdateOrderDelivery(int id)
        {
            IEnumerable<Dal.DO.Order> existingOrdersList = Dal.Order.GetAll();
            foreach (var order in existingOrdersList)
            {
                if (order.OrderID == id && order.ShipDate == DateTime.MinValue)
                {
                    setShip(order);
                    //return order;
                    //order.ShipDate = DateTime.Now;
                }
            }
            //BO.Order.get
            throw new NotImplementedException();
        }
        public BO.Order UpdateOrderShipping(int id)
        {
            throw new NotImplementedException();
        }
        //bonus
        public BO.Order UpdateOrderForManager(int id)
        {
            throw new NotImplementedException();
        }


    }
}
