using Dal.DO;
using DalApi;
namespace DalList;
internal class DalProduct : IProduct
{
    public int Add(Product obj)
    {
        Random rand = new Random();
        bool idExist = false;
        int proId;
        do
        {
            idExist = true;
            proId = (int)rand.NextInt64(100009, 999999);
            for (int j = 0; j < DalList.DataSource.products.Count(); j++)
                if (DalList.DataSource.products[j].ID == proId)
                    idExist = false;
        } while (!idExist);
        obj.ID = proId;
        DataSource.products.Add(obj);
       return  obj.ID;
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
    public void updateAmount(int id, int am)
    {
        int i;
        Product p;
        for (i = 0; i < DataSource.products.Count(); i++)
        {
            if (DataSource.products[i].ID == id)
            {
                p = DataSource.products[i];
                p.InStock = am;
                DataSource.products[i] = p;
                return;
            }
        }
        throw new ExceptionObjectNotFound();

    }
}

