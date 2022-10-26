
using Dal.DO;
namespace Dal.UseObjects;

internal class DalProduct : IDalObject
{
    public int Create(IDataObject obj)
    {
        //Product p = new Product() { };
        //ProductList[0]=obj;
        //// p.ProductName = (obj as Product).ProductName;
        //p.ProductName = ((Product)obj).ProductName;
        //int x = 2;
        //p.Category = (eCategory)x;
        throw new NotImplementedException();
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

