
using Dal.DO;
using DalFacade.DO;
namespace Dal.UseObjects;

internal class DalProduct : IDalObject
{
    public product Create(int id, string name, eCategory cat, float price, int inStock)
    {
        product p = new product() { };
        p.ID = id; p.Name = name; p.Category = cat; p.Price = price; p.InStock = inStock;
        //products[0] = obj;
        //// p.ProductName = (obj as Product).ProductName;
        //p.ProductName = ((Product)obj).ProductName;
        //int x = 2;
        //p.Category = (eCategory)x;
        //throw new NotImplementedException();
        return p;
    }

    public void Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public IDataObject[] Read()
    {
        throw new NotImplementedException();
    }

    public IDataObject ReadSingle(int Id)
    {
        throw new NotImplementedException();
    }

    public void Update(IDataObject obj)
    {
        throw new NotImplementedException();
    }
}

