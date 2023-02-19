using BO;
using Dal.DO;

namespace BlImplementation;
internal class BlOrder : BLApi.IOrder
{
    DalApi.IDal? Dal = DalApi.Factory.Get();

    public int AddNewOrder(BO.Order order)
    {
        try
        {
            Dal.DO.Order o = new()
            {
                OrderID = order.ID,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = order.DeiveryDate,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
            };
            int orderId = Dal.Order.Add(o);

            order.Items.ForEach(oi => { oi.ID = orderId; Dal.OrderItem.Add(convertToDal(oi)); });
            /*someValues.ToList().ForEach(x => list.Add(x + 1));*/
            return orderId;

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

    public Dal.DO.OrderItem convertToDal(BO.OrderItem bo)
    {
        Dal.DO.OrderItem o = new();
        o.OrderID = bo.ID;
        o.ProductID = bo.ProductID;
        o.Amount = bo.Amount;
        o.Price = bo.Price;
        return o;
    }
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

                if (item.DeliveryDate != null)
                    o.Status = (BO.OrderStatus)3;
                else if (item.ShipDate != null)
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
            if (o.DeliveryDate != null)
                oi.Status = (BO.OrderStatus)3;
            else if (o.ShipDate != null)
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
            /*            var items = from BO.OrderItem item1 in orderItems
                                        select new BO.OrderItem
                                        {
                                            ID = item1.ID,
                                            Amount = item1.Amount,
                                            Price = item1.Price,
                                            ProductID = item1.ProductID,
                                            ProductName = Dal.Product.Get(p => p.ID == item1.ProductID).Name,
                                            TotalPrice = item1.Amount * item1.Price,
                                            oi.TotalPrice += orderItem.TotalPrice
                                        };*/

            oi.Items = items.ToList();
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
        /* try
         {
             Dal.DO.Order o = new Dal.DO.Order();
             o = Dal.Order.Get(o => o.OrderID == id);
             if ((DateTime?)o.DeliveryDate != null)
                 throw new BO.BlInvalidIdToken("");
             o.DeliveryDate = DateTime.Now;
             Dal.Order.Update(o);
             //BO.Order order = new BO.Order();
             BO.Order order = new()
             {
                 ID = o.OrderID,
                 OrderDate = o.OrderDate,
                 DeiveryDate = o.DeliveryDate,
                 ShipDate = o.ShipDate,
                 CustomerAddress = o.CustomerAddress,
                 CustomerEmail = o.CustomerEmail,
                 CustomerName = o.CustomerName
             };
             *//*   order.ID = o.OrderID;
                order.OrderDate = o.OrderDate;
                order.DeiveryDate = o.DeliveryDate;
                order.ShipDate = o.ShipDate;
                order.CustomerAddress = o.CustomerAddress;
                order.CustomerEmail = o.CustomerEmail;
                order.CustomerName = o.CustomerName;*//*
             if (o.CustomerName != "")
             {
                 o.DeliveryDate = DateTime.Now;
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

             throw new BO.BlDefaultException("unexpected error");
         }
 */


        try
        {
            Dal.DO.Order oDO = Dal.Order.Get(o => o.OrderID == id);
            if (oDO.DeliveryDate == null)
            {
                oDO.DeliveryDate = DateTime.Now;
                BO.Order order = new BO.Order();
                order.ID = oDO.OrderID;
                order.OrderDate = oDO.OrderDate;
                order.DeiveryDate = oDO.DeliveryDate;
                order.ShipDate = oDO.ShipDate;
                order.CustomerAddress = oDO.CustomerAddress;
                order.CustomerEmail = oDO.CustomerEmail;
                order.CustomerName = oDO.CustomerName;
                order.Status = (BO.OrderStatus)1;
                Dal.Order.Update(oDO);
                return order;
            }
            throw new BlEntityNotFoundException("");
        }
        catch (DllNotFoundException e)
        {
            throw new BlEntityNotFoundException("");
        }
    }







public BO.Order UpdateOrderShipping(int orderId)
{
    try
    {
        Dal.DO.Order oDO = Dal.Order.Get(o => o.OrderID == orderId);
        if (oDO.ShipDate == null)
        {
            oDO.ShipDate = DateTime.Now;
            BO.Order order = new BO.Order();
            order.ID = oDO.OrderID;
            order.OrderDate = oDO.OrderDate;
            order.DeiveryDate = oDO.DeliveryDate;
            order.ShipDate = oDO.ShipDate;
            order.CustomerAddress = oDO.CustomerAddress;
            order.CustomerEmail = oDO.CustomerEmail;
            order.CustomerName = oDO.CustomerName;
            order.Status = (BO.OrderStatus)1;
            Dal.Order.Update(oDO);
            return order;
        }
        throw new BlEntityNotFoundException("");
    }
    catch (DllNotFoundException e)
    {
        throw new BlEntityNotFoundException("");
    }
}


    /*  public BO.Order UpdateOrderShipping(int id)
      {
          try
          {
              Dal.DO.Order o = new Dal.DO.Order();
              o = Dal.Order.Get(o => o.OrderID == id);
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
      }*/
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


public BO.OrderTracking OrderTrack(int id)
{
    try
    {
        Dal.DO.Order currOrder = Dal.Order.Get(x => x.OrderID == id);
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        orderTracking.ID = currOrder.OrderID;
        /*            DateTime? s;
                    var t = Tuple.Create(currOrder.ShipDate, BO.OrderStatus.Payed) ;
                    orderTracking.dateAndTrack.Add(t);*/
        /*            orderTracking.dateAndTrack.Add(new Tuple<DateTime?, BO.OrderStatus>((DateTime?)currOrder.OrderDate, (BO.OrderStatus)0));
        */
        orderTracking?.dateAndTrack.Add(new Tuple<DateTime?, OrderStatus?>(currOrder.OrderDate, BO.OrderStatus.Payed));
        orderTracking.Status = BO.OrderStatus.Payed;
        if (currOrder.ShipDate <= DateTime.Now)
        {
            orderTracking.dateAndTrack.Add(new Tuple<DateTime?, OrderStatus?>(currOrder.ShipDate, BO.OrderStatus.Shiped));
            orderTracking.Status = BO.OrderStatus.Shiped;
        }
        if (currOrder.DeliveryDate <= DateTime.Now)
        {
            orderTracking.dateAndTrack.Add(new Tuple<DateTime?, OrderStatus?>(currOrder.DeliveryDate, BO.OrderStatus.Delivered));
            orderTracking.Status = BO.OrderStatus.Delivered;
        }
        return orderTracking;
    }
    catch (DalApi.ExceptionObjectNotFound)
    {
        throw new Exception();
    }
}
}
