namespace BLApi
{
    public interface ICart
    {
        public BO.Cart AddProductToCart(BO.Cart c, int id);
        public BO.Cart Update(BO.Cart c, int id, double newAmount);
        public void CartConfirmation(BO.Cart c, string customerName, string customerEmail, string customerAddress);
    }
}
