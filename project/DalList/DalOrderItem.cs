

using Dal.DO;
using DalList;
using DalApi;
namespace DalList;

 internal class DalOrderItem: IOrderItem
{
     public void Add(OrderItem obj)
    {
        DataSource.orderItems.Add(obj);

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

