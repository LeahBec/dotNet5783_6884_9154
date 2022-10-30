

using Dal.DO;
using DalList;
namespace Dal.UseObjects;

internal class DalOrderItem : IDalObject
{
    int IDalObject.Create(IDataObject obj)
    {
         if (DataSource.Config.OrderItemIndex >200) 
                throw new Exception("not enough space");
            DataSource.orderItems[DataSource.Config.OrderItemIndex++]=(OrderItem)obj;
        
    }

    void IDalObject.Delete(int Id)
    {
        int i;
        for ( i = 0; i < DataSource.Config.OrderItemIndex; i++)
		{
            if (DataSource.orderItems[i].ID == Id)
            {
                for (int k = i; k < DataSource.Config.OrderItemIndex; k++)
			    {
                    DataSource.orderItems[k] = DataSource.orderItems[k+1]
			    }
                DataSource.Config.OrderItemIndex--;
                return;
            }
		}
            throw new Exception("object not found");

    }

    IDataObject[] IDalObject.Read()
    {
           OrderItem[] orderItems = new OrderItem[100];
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
			{
            orderItems[i] = DataSource.orderItems[i];
			}
            return orderItems;
        
        throw new Exception("failed to read orderItems");
    }

    IDataObject IDalObject.ReadSingle(int Id)
    {
        for ( i = 0; i < DataSource.Config.OrderItemIndex; i++)
		{
            if (DataSource.orderItems[i].ID == Id)
            {
               return DataSource.orderItems[i].ID;
            }
		}            throw new Exception("object not found");

    }

    void IDalObject.Update(IDataObject obj)
    {
      OrderItem oi = (OrderItem)obj;
        for ( i = 0; i < DataSource.Config.OrderItemIndex; i++)
		{
            if (DataSource.orderItems[i].ID ==oi.ID)
            {
                DataSource.orderItems[i] = oi;
                return;
            }
		}            throw new Exception("object not found");

    }
}

