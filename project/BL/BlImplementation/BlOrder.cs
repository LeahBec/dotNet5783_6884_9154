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

