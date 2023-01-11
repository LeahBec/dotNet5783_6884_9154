namespace BlImplementation;
internal class BlOrder : BLApi.IOrder
{
    DalApi.IDal? Dal = DalApi.Factory.Get();

    public IEnumerable<BO.OrderForList?> GetOrderList()
    {
        try
        {
            IEnumerable<Dal.DO.Order> existingOrdersList = Dal.Order.GetAll();
            List<BO.OrderForList> ordersList = new List<BO.OrderForList>();
            existingOrdersList.Select(item =>
            {
                BO.OrderForList o = new BO.OrderForList();
                o.ID = item.OrderID;
                o.CustomerName = item.CustomerName;
                var orderItems = Dal.OrderItem.getByOrderId(item.OrderID);
                orderItems.Select(orderItem =>
                {
                    o.AmountOfItems++;
                    o.TotalPrice += orderItem.Price * orderItem.Amount;
                    return orderItem;
                }).ToList();
                if (item.DeliveryDate > DateTime.MinValue)
                    o.Status = (BO.OrderStatus)3;
                else if (item.ShipDate > DateTime.MinValue)
                    o.Status = (BO.OrderStatus)2;
                else
                    o.Status = (BO.OrderStatus)1;
                ordersList.Add(o);
                return item;
            }).ToList();
            if (ordersList.Count() == 0)
                throw new BO.BlNoEntitiesFound("");
            return ordersList;
        }
        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (DalApi.ExceptionNoMatchingItems)
        {
            throw new BO.BlExceptionNoMatchingItems();
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("Unknown exception occured");
        }
    }

    public BO.Order GetOrderDetails(int id)
    {
        try
        {
            if (id < 0)
                throw new BO.BlInvalidIntegerException();
            Dal.DO.Order o = Dal.Order.Get(o => o.OrderID == id);
            BO.Order oi = new BO.Order();
            oi.ID = o.OrderID;
            oi.OrderDate = o.OrderDate;
            oi.ShipDate = o.ShipDate;
            oi.CustomerAddress = o.CustomerAddress;
            oi.CustomerEmail = o.CustomerEmail;
            oi.CustomerName = o.CustomerName;
            oi.DeiveryDate = o.DeliveryDate;
            if (o.DeliveryDate > DateTime.MinValue)
                oi.Status = (BO.OrderStatus)3;
            else if (o.ShipDate > DateTime.MinValue)
                oi.Status = (BO.OrderStatus)2;
            else
                oi.Status = (BO.OrderStatus)1;
            IEnumerable<Dal.DO.OrderItem> orderItems = Dal.OrderItem.getByOrderId(id);
            List<BO.OrderItem> items = new();
            /*foreach (Dal.DO.OrderItem item in orderItems)
            {
                BO.OrderItem orderItem = new();
                orderItem.ID = item.ID;
                orderItem.ProductName = Dal.Product.Get(p => p.ID == item.ProductID).Name;
                orderItem.ProductID = item.ProductID;
                orderItem.Price = item.Price;
                orderItem.Amount = item.Amount;
                orderItem.TotalPrice = orderItem.Amount * orderItem.Price;
                oi.TotalPrice += orderItem.TotalPrice;
                items.Add(orderItem);
            }*/
            orderItems.Select(item =>
            {
                BO.OrderItem orderItem = new();
                orderItem.ID = item.ID;
                orderItem.ProductName = Dal.Product.Get(p => p.ID == item.ProductID).Name;
                orderItem.ProductID = item.ProductID;
                orderItem.Price = item.Price;
                orderItem.Amount = item.Amount;
                orderItem.TotalPrice = orderItem.Amount * orderItem.Price;
                oi.TotalPrice += orderItem.TotalPrice;
                items.Add(orderItem);
                return item;
            }).ToList();

            oi.Items = items;
            if (oi.CustomerName != null)
                return oi;
            return null;
        }
        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (Exception)
        {

            throw new BO.BlDefaultException("");
        }
    }
    void setShip(Dal.DO.Order o)
    {
        o.ShipDate = DateTime.Now;

    }
    public BO.Order UpdateOrderDelivery(int id)
    {
        try
        {
            Dal.DO.Order o = new Dal.DO.Order();
            o = Dal.Order.Get(o => o.OrderID == id);
            DateTime d_ = (DateTime)o.DeliveryDate;
            DateTime d2_ = DateTime.MinValue;
            if (!d_.Equals(d2_))
            {
                throw new BO.BlInvalidIdToken("");
            }
            double i_ = DateTime.Compare(d_, d2_);
            if (i_ != 0)
                throw new BO.BlInvalidIdToken("");
            o.DeliveryDate = DateTime.UtcNow;

            Dal.Order.Update(o);
            BO.Order order = new BO.Order();
            order.ID = o.OrderID;
            order.OrderDate = o.OrderDate;
            order.DeiveryDate = o.DeliveryDate;
            order.ShipDate = o.ShipDate;
            order.CustomerAddress = o.CustomerAddress;
            order.CustomerEmail = o.CustomerEmail;
            order.CustomerName = o.CustomerName;

            if (o.CustomerName != "")
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
            else
            {

                throw new BO.BlInvalidNameToken("");
            }
        }
        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (Exception)
        {

            throw new BO.BlDefaultException("unexpected error");
        }
    }
    public BO.Order UpdateOrderShipping(int id)
    {
        try
        {
            Dal.DO.Order o = new Dal.DO.Order();
            o = Dal.Order.Get(o => o.OrderID == id);
            /* if (o.ShipDate != DateTime.MinValue)
                 throw new BO.BlInvalidIdToken("");*/
            o.ShipDate = DateTime.Now;
            Dal.Order.Update(o);
            BO.Order order = new BO.Order();
            order.ID = o.OrderID;
            order.OrderDate = o.OrderDate;
            order.DeiveryDate = o.DeliveryDate;
            order.ShipDate = o.ShipDate;
            order.CustomerAddress = o.CustomerAddress;
            order.CustomerEmail = o.CustomerEmail;
            order.CustomerName = o.CustomerName;

            if (o.CustomerName != "")
            {

                Dal.Order.Delete(id);
                Dal.Order.Add(o);
                order.ShipDate = o.ShipDate;
                order.Status = (BO.OrderStatus)2;
                return order;
            }
            else
            {
                throw new BO.BlInvalidNameToken("");
            }

        }
        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (Exception)
        {

            throw new BO.BlDefaultException("");
        }
    }
    //bonus
    public BO.Order UpdateOrderForManager(BO.Order or)
    {
        try
        {
            Dal.DO.Order o = new Dal.DO.Order();
            o.OrderID = or.ID;
            o.CustomerName = or.CustomerName;
            o.OrderDate = or.OrderDate;
            o.ShipDate = or.ShipDate;
            o.DeliveryDate = or.DeiveryDate;
            o.CustomerEmail = or.CustomerEmail;
            o.CustomerAddress = or.CustomerAddress;
            o.OrderID = or.ID;
            Dal.Order.Update(o);
        }
        catch (DalApi.ExceptionFailedToRead)      ///////////////////////////
        {
            throw new BO.BlExceptionFailedToRead();//////////////////////////
        }
        return or; /////////////////////////////////////////////////////////
    }
}

