

using Dal.DO;
using DalList;
namespace DalList;

static public class DalOrderItem
{
    static public void Create(OrderItem obj)
    {
        if (DataSource.Config.OrderItemIndex > 200)
            throw new Exception("not enough space");
        DataSource.orderItems[DataSource.Config.OrderItemIndex++] = obj;

    }

    static public void Delete(int Id)
    {
        int i;
        for (i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource.orderItems[i].ID == Id)
            {
                for (int k = i; k < DataSource.Config.OrderItemIndex; k++)
                {
                    DataSource.orderItems[k] = DataSource.orderItems[k + 1];
                }
                DataSource.Config.OrderItemIndex--;
                return;
            }
        }
        throw new Exception("object not found");

    }

    static public OrderItem[] Read()
    {
        OrderItem[] orderItems = new OrderItem[DataSource.Config.OrderItemIndex];
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            orderItems[i] = DataSource.orderItems[i];
        }
        return orderItems;

        throw new Exception("failed to read orderItems");
    }

    static public OrderItem ReadSingle(int Id)
    {
        int i;
        for (i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource.orderItems[i].ID == Id)
            {
                return DataSource.orderItems[i];
            }
        }
        throw new Exception("object not found");
    }

    static public void Update(OrderItem obj)
    {
        int i;
        OrderItem oi = (OrderItem)obj;
        for (i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource.orderItems[i].ID == oi.ID)
            {
                DataSource.orderItems[i] = oi;
                return;
            }
        }
        throw new Exception("object not found");

    }
}

