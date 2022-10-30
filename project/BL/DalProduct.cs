using Dal.DO;
using DalList;
namespace Dal.UseObjects;

internal class DalProduct : IDalObject
{
    public Product Create(IDalObject obj)
    {
         if (DataSource.Config.ProductIndex >99) 
                throw new Exception("not enough space");
            DataSource.products[DataSource.Config.ProductIndex++]=(Product)obj;
        
    }

    public void Delete(int Id)
    {
          int i;
        for ( i = 0; i < DataSource.Config.ProductIndex; i++)
		{
            if (DataSource.products[i].ID == Id)
            {
                for (int k = i; k < DataSource.Config.ProductIndex; k++)
			    {
                    DataSource.products[k] = DataSource.products[k+1]
			    }
                DataSource.Config.ProductIndex--;
                return;
            }
		}
            throw new Exception("object not found");

    }

    public IDataObject[] Read()
    {
        Product[] products = new Product[100];
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
			{
            products[i] = DataSource.products[i];
			}
            return products;
        
        throw new Exception("failed to read products");
    }

    public IDataObject ReadSingle(int Id)
    {
         for ( i = 0; i < DataSource.Config.ProductIndex; i++)
		{
            if (DataSource.products[i].ID == Id)
            {
               return DataSource.products[i].ID;
            }
		}            throw new Exception("object not found");

    }

    public void Update(IDataObject obj)
    {
        Product p = (Product)obj;
        for ( i = 0; i < DataSource.Config.ProductIndex; i++)
		{
            if (DataSource.products[i].id ==p.ID)
            {
                DataSource.products[i] = p;
                return;
            }
		}            throw new Exception("object not found");

    }
}

