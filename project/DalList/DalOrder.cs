using Dal.DO;
using DalApi;
namespace DalList;

internal class DalOrder : IOrder
{
    public int Add(Order obj)
    {
        obj.OrderID = DataSource.Config.OrderId;
        DataSource.orders.Add(obj);
        return obj.OrderID;
    }
    public void Delete(int Id)
    {

        int i;
        for (i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i].OrderID == Id)
            {
                DataSource.orders.Remove(DataSource.orders[i]);
                return;
            }
        }
        throw new ExceptionObjectNotFound();
    }

    public IEnumerable<Order> GetAll()
    {
        List<Order> orders = new List<Order>();
        for (int i = 0; i < DataSource.orders.Count(); i++)
        {
            orders.Add(DataSource.orders[i]);
        }
        return orders;
        throw new ExceptionFailedToRead();
    }

    public Order Get(int Id)
    {
        int i;
        for (i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i].OrderID == Id)
            {
                return DataSource.orders[i];
            }
        }
        throw new ExceptionObjectNotFound();
    }

    public void Update(Order obj)
    {
        int i;
        Order o = obj;
        for (i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i].OrderID == o.OrderID)
            {
                DataSource.orders[i] = o;
                break;
            }
        }
        if(i==DataSource.orders.Count())
        throw new ExceptionObjectNotFound();

    }
}

