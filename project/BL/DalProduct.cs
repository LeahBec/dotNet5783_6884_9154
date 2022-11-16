using Dal.DO;
using DalApi;
namespace DalList;


internal class DalProduct : IProduct
{
    public void Add(Product obj)
    {
        DataSource.products.Add(obj);
    }
    public void Delete(int Id)
    {
        for (int i = 0; i < DataSource.products.Count(); i++)
        {
            if (DataSource.products[i].ID == Id)
            {
                DataSource.products.Remove(DataSource.products[i]);
                return;
            }
        }
        throw new ExceptionObjectNotFound();

    }

    public IEnumerable<Product> GetAll()
    {
        List<Product> products = new List<Product>();
        for (int i = 0; i < DataSource.products.Count(); i++)
        {
            products.Add(DataSource.products[i]);
        }
        return products;
        throw new ExceptionFailedToRead();
    }

    public Product Get(int Id)
    {
        int i;
        for (i = 0; i < DataSource.products.Count(); i++)
        {
            if (DataSource.products[i].ID == Id)
            {
                return DataSource.products[i];
            }
        }
        throw new ExceptionObjectNotFound();

    }

    public void Update(Product obj)
    {
        int i;
        Product p = obj;
        for (i = 0; i < DataSource.products.Count(); i++)
        {
            if (DataSource.products[i].ID == p.ID)
            {
                DataSource.products[i] = p;
                return;
            }
        }
        throw new ExceptionObjectNotFound();

    }
}

