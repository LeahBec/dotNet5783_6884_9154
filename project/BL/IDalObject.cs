
using Dal.DO;
using DalFacade.DO;
namespace Dal.UseObjects;
public interface IDalObject
{
    Product Create(int id, string name, eCategory cat, float price, int inStock)
; // כאן אא לרשום תוכן בפונקציה
    IDataObject ReadSingle(int Id);
    IDataObject[] Read();
    void Update(IDataObject obj);
    void Delete(int Id);
}

