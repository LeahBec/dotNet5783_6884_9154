using DalApi;

namespace Dal;

sealed public class DalXml : IDal
{
  /*  public IProduct Product => throw new NotImplementedException();
    public IOrder Order => throw new NotImplementedException();
    public IOrderItem OrderItem => throw new NotImplementedException();*/
    public IProduct Product { get; } = new Dal.DalProduct();
    public IOrder Order { get; } = new Dal.DalOrder();
    public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();
}
