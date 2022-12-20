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
            foreach (var item in existingOrdersList)
            {
                BO.OrderForList o = new BO.OrderForList();
                o.ID = item.OrderID;
                o.CustomerName = item.CustomerName;
                var orderItems = Dal.OrderItem.getByOrderId(item.OrderID);
                foreach (var orderItem in orderItems)
                {
                    o.AmountOfItems++;
                    o.TotalPrice += orderItem.Price * orderItem.Amount;
                }
                if (item.DeliveryDate > DateTime.MinValue)
                    o.Status = (BO.OrderStatus)3;
                else if (item.ShipDate > DateTime.MinValue)
                    o.Status = (BO.OrderStatus)2;
                else
                    o.Status = (BO.OrderStatus)1;
                ordersList.Add(o);
            }
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
            oi.CustomerAdress = o.CustomerAdress;
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
            foreach (Dal.DO.OrderItem item in orderItems)
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
            }
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
            if (o.DeliveryDate != DateTime.MinValue)
                throw new BO.BlInvalidIdToken("");
            o.DeliveryDate = DateTime.Now;

            Dal.Order.Update(o);
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
            else
            {
                if (o.CustomerName == "")
                    throw new BO.BlInvalidNameToken("");
                else
                {
                    throw new BO.BlOrderAlreadyDelivered("");
                }
            }
        }
        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException();
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
            if (o.ShipDate != DateTime.MinValue)
                throw new BO.BlInvalidIdToken("");
            o.ShipDate = DateTime.Now;
            Dal.Order.Update(o);
            BO.Order order = new BO.Order();
            order.ID = o.OrderID;
            order.OrderDate = o.OrderDate;
            order.DeiveryDate = o.DeliveryDate;
            order.ShipDate = o.ShipDate;
            order.CustomerAdress = o.CustomerAdress;
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
            throw new BO.BlEntityNotFoundException();
        }
        catch (Exception)
        {

            throw new BO.BlDefaultException("");
        }
    }
    //bonus
    public BO.Order UpdateOrderForManager(int id)
    {
        throw new NotImplementedException();
    }


}

