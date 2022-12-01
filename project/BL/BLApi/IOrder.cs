namespace BLApi
{/// <summary>
/// the interface implements additional functions for the BL - Order entity
/// </summary>
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
