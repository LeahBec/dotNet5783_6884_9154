using Dal.DO;
using DalApi;
namespace DalList;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem obj)
    {
        obj.OrderID = DataSource.Config.OrderItemId;
        DataSource.orderItems.Add(obj);
        return obj.OrderID;
    }

    public void Delete(int Id)
    {
        int i;
        for (i = 0; i < DataSource.orderItems.Count(); i++)
        {
            if (DataSource.orderItems[i].ID == Id)
            {
                DataSource.orderItems.Remove(DataSource.orderItems[i]);
                return;
            }
        }
        throw new ExceptionObjectNotFound();
    }

    public IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> orderItems = new List<OrderItem>();
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
        {
            orderItems.Add(DataSource.orderItems[i]);
        }
        return orderItems;

        throw new ExceptionFailedToRead();
    }

    public IEnumerable<OrderItem> getByOrderId(int orderId)
    {
        List<OrderItem> OrderItemList = new List<OrderItem>();
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
        {
            if (DataSource.orderItems[i].OrderID == orderId)
            {
                OrderItemList.Add(DataSource.orderItems[i]);
            }
        } return OrderItemList;
    }


    public OrderItem Get(int Id)
    {
        int i;
        for (i = 0; i < DataSource.orderItems.Count(); i++)
        {
            if (DataSource.orderItems[i].ID == Id)
            {
                return DataSource.orderItems[i];
            }
        }
        throw new ExceptionObjectNotFound();
    }

    public void Update(OrderItem obj)
    {
        int i;
        OrderItem oi = obj;
        for (i = 0; i < DataSource.orderItems.Count(); i++)
        {
            if (DataSource.orderItems[i].ID == oi.ID)
            {
                DataSource.orderItems[i] = oi;
                return;
            }
        }
        throw new ExceptionObjectNotFound();

    }
}

