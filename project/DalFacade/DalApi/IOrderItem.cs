using Dal.DO;
namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        public IEnumerable<OrderItem> getByOrderId(int orderId);
    }
}
