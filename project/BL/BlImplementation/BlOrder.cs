using DalApi;
namespace BlImplementation;
internal class BlOrder : BLApi.IOrder
{
    private IDal Dal = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        IEnumerable<Dal.DO.Order> existingOrdersList = Dal.Order.GetAll();

        List<BO.OrderForList> ordersList = new List<BO.OrderForList>();

        foreach (var item in existingOrdersList)
        {
            BO.OrderForList o = new BO.OrderForList();
            o.ID = item.OrderID;
            o.CustomerName = item.CustomerName;
            ordersList.Add(o);
        }
        if (ordersList.Count() == 0)
            throw new NotImplementedException();
        return ordersList;
    }

    public BO.Order GetOrderDetails(int id)
    {
        if (id < 0)
            throw new NotImplementedException();
        Dal.DO.Order o = Dal.Order.Get(id);
        BO.Order oi = new BO.Order();
        oi.ID = o.OrderID;
        oi.OrderDate = o.OrderDate;
        oi.ShipDate = o.ShipDate;
        oi.CustomerAdress = o.CustomerAdress;
        oi.CustomerEmail = o.CustomerEmail;
        oi.CustomerName = o.CustomerName;
        oi.DeiveryDate = o.DeliveryDate;
        oi.TotalPrice = 0;
        if (oi.CustomerName != null)
            return oi;
        return null;
    }
    void setShip(Dal.DO.Order o)
    {
        o.ShipDate = DateTime.Now;

    }
    public BO.Order UpdateOrderDelivery(int id)
    {
        Dal.DO.Order o = new Dal.DO.Order();
        o = Dal.Order.Get(id);
        BO.Order order = new BO.Order();
        order.ID = o.OrderID;
        order.OrderDate = o.OrderDate;
        order.DeiveryDate = o.DeliveryDate;
        order.ShipDate = o.ShipDate;
        order.CustomerAdress = o.CustomerAdress;
        order.CustomerEmail = o.CustomerEmail;
        order.CustomerName = o.CustomerName;

        if (o.CustomerName != "" && o.DeliveryDate == DateTime.MinValue)
        {
            o.DeliveryDate = DateTime.Now;

            Dal.Order.Delete(id);
            Dal.Order.Add(o);
            order.ShipDate = o.ShipDate;
            order.Status = (BO.OrderStatus)2;
            //ליצור כאן רשימה של items לפי orderItem?
            //כנל לגבי total price.
            return order;
        }
        throw new NotImplementedException();
    }
    public BO.Order UpdateOrderShipping(int id)
    {
        Dal.DO.Order o = new Dal.DO.Order();
        o = Dal.Order.Get(id);
        BO.Order order = new BO.Order();
        order.ID = o.OrderID;
        order.OrderDate = o.OrderDate;
        order.DeiveryDate = o.DeliveryDate;
        order.ShipDate = o.ShipDate;
        order.CustomerAdress = o.CustomerAdress;
        order.CustomerEmail = o.CustomerEmail;
        order.CustomerName = o.CustomerName;

        if (o.CustomerName != "" && o.ShipDate == DateTime.MinValue)
        {
            o.ShipDate = DateTime.Now;

            Dal.Order.Delete(id);
            Dal.Order.Add(o);
            order.ShipDate = o.ShipDate;
            order.Status = (BO.OrderStatus)2;
            //ליצור כאן רשימה של items לפי orderItem?
            //כנל לגבי total price.
            return order;
        }
        
        throw new NotImplementedException();
    }
    //bonus
    public BO.Order UpdateOrderForManager(int id)
    {
        throw new NotImplementedException();
    }


}

