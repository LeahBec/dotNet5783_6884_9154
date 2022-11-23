namespace BLApi
{
    public interface IOrder
    {
        public IEnumerable<BO.OrderForList> GetOrderList();
        public BO.Order GetOrderDetails(int id);
        public BO.Order UpdateOrderShipping(int id);
        public BO.Order UpdateOrderDelivery(int id);
        //BONUS.
        public BO.Order UpdateOrderForManager(int id);

    }
}
