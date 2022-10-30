

using Dal.DO;
using DalList;
namespace Dal.UseObjects;

internal class DalOrder : IDalObject
{
    public int Create(IDataObject obj)
    {
        
           if (DataSource.Config.OrderIndex >49) 
                throw new Exception("not enough space");
            DataSource.orders[DataSource.Config.OrderIndex++]=(Order)obj;
        
    }

    public void Delete(int Id)
    {
       
        int i;
        for ( i = 0; i < DataSource.Config.OrderIndex; i++)
		{
            if (DataSource.orders[i].OrderID == Id)
            {
                for (int k = i; k < DataSource.Config.OrderIndex; k++)
			    {
                    DataSource.orders[k] = DataSource.orders[k+1]
			    }
                DataSource.Config.OrderIndex--;
                return;
            }
		}
            throw new Exception("object not found");

        
    }

    public IDataObject[] Read()
    {
        
        Order[] orders = new Order[100];
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
			{
            orders[i] = DataSource.orders[i];
			}
            return orders;
        
        throw new Exception("failed to read orders");
    }

    public IDataObject ReadSingle(int Id)
    {

        for ( i = 0; i < DataSource.Config.OrderIndex; i++)
		{
            if (DataSource.orders[i].OrderID == Id)
            {
               return DataSource.orders[i].OrderID;
            }
		}            throw new Exception("object not found");

        
    
    }

    public void Update(IDataObject obj)
    {
        Order o = (Order)obj;
        for ( i = 0; i < DataSource.Config.OrderIndex; i++)
		{
            if (DataSource.orders[i].OrderID ==o.OrderID)
            {
                DataSource.orders[i] = o;
                return;
            }
		}            throw new Exception("object not found");

    }
}

