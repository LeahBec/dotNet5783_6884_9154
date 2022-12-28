using DalApi;

namespace Dal;

sealed internal class DalXml : IDal
{
    public IProduct Product => throw new NotImplementedException();

    public IOrder Order => throw new NotImplementedException();

    public IOrderItem OrderItem => throw new NotImplementedException();

    public IProduct product { get; } = new Dal.DalProduct();
    public IOrder order { get; } = new Dal.DalOrder();
    public IOrderItem orderItem { get; } = new Dal.DalOrderItem();
}
