

using Dal.DO;

namespace Dal.UseObjects;

internal class DalOrderItem : IDalObject
{
    int IDalObject.Create(IDataObject obj)
    {
        throw new NotImplementedException();
    }

    void IDalObject.Delete(int Id)
    {
        throw new NotImplementedException();
    }

    IDataObject[] IDalObject.Read()
    {
        throw new NotImplementedException();
    }

    IDataObject IDalObject.ReadSingle(int Id)
    {
        throw new NotImplementedException();
    }

    void IDalObject.Update(IDataObject obj)
    {
        throw new NotImplementedException();
    }
}

