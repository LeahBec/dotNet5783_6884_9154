

using Dal.DO;
using DalList;
namespace DalList;

static public class DalOrder
{
    static public void Create(Order obj)
    {

        if (DataSource.Config.OrderIndex > 49)
            throw new Exception("not enough space");
        DataSource.orders[DataSource.Config.OrderIndex++] = obj;

    }

    static public void Delete(int Id)
    {

        int i;
        for (i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            if (DataSource.orders[i].OrderID == Id)
            {
                for (int k = i; k < DataSource.Config.OrderIndex; k++)
                {
                    DataSource.orders[k] = DataSource.orders[k + 1];
                }
                DataSource.Config.OrderIndex--;
                return;
            }
        }
        throw new Exception("object not found");


    }

    static public Order[] Read()
    {

        Order[] orders = new Order[100];
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            orders[i] = DataSource.orders[i];
        }
        return orders;

        throw new Exception("failed to read orders");
    }

    static public Order ReadSingle(int Id)
    {
        int i;
        for (i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            if (DataSource.orders[i].OrderID == Id)
            {
                return DataSource.orders[i];
            }
        }
        throw new Exception("object not found");



    }

    static public void Update(Order obj)
    {
        int i;
        Order o = (Order)obj;
        for (i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            if (DataSource.orders[i].OrderID == o.OrderID)
            {
                DataSource.orders[i] = o;
                return;
            }
        }
        throw new Exception("object not found");

    }
}

