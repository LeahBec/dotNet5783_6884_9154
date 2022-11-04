using Dal.DO;
namespace DalList;


static public class DalProduct
{

    const int MAXPRODUCTS = 99;
    static public void Create(Product obj)
    {
        if (DataSource.Config.ProductIndex > MAXPRODUCTS)
            throw new Exception("not enough space");
        DataSource.products[DataSource.Config.ProductIndex] = (Product)obj;
        DataSource.Config.ProductIndex++;
    }

    static public void Delete(int Id)
    {
        int i;
        for (i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource.products[i].ID == Id)
            {
                for (int k = i; k < DataSource.Config.ProductIndex; k++)
                {
                    DataSource.products[k] = DataSource.products[k + 1];

                }
                DataSource.Config.ProductIndex--;
                return;
            }
        }
        throw new Exception("object not found");

    }

    static public Product[] Read()
    {
        Product[] products = new Product[DataSource.Config.ProductIndex];
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            products[i] = DataSource.products[i];
        }
        return products;
        throw new Exception("failed to read products");
    }

    static public Product ReadSingle(int Id)
    {
        int i;
        for (i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource.products[i].ID == Id)
            {
                return DataSource.products[i];
            }
        }
        throw new Exception("object not found");

    }

    static public void Update(Product obj)
    {
        int i;
        Product p = obj;
        for (i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource.products[i].ID == p.ID)
            {
                DataSource.products[i] = p;
                return;
            }
        }
        throw new Exception("object not found");

    }
}

