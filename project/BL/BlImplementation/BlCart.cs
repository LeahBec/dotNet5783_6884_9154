using BLApi;
using BO;
using DalApi;
using Dal;
namespace BlImplementation
{
    internal class BlCart : ICart
    {
        private IDal Dal = new Dal.DalList();

        public Cart AddProductToCart(Cart cart, int productId)
        {
            int productInStock = Dal.Product.Get(productId).InStock;
            double productPrice = Dal.Product.Get(productId).Price;
            OrderItem oi = cart.items.Find(oi => oi.ProductID == productId);
            if (productInStock > 0)
            {
                if (oi.ID != 0)
                {
                    oi.Amount++;
                    oi.TotalPrice += productPrice;
                    cart.TotalPrice += productPrice;
                    return cart;
                }
                else
                {
                    oi.Price = productPrice;
                    oi.TotalPrice = productPrice;
                    cart.items.Add(oi);
                    cart.TotalPrice += productPrice;
                }
                return cart;
            }
            else
                throw new OutOfStockException();
        }

        bool IsValidEmail(string email)
{
    var trimmedEmail = email.Trim();

    if (trimmedEmail.EndsWith(".")) {
        return false; // suggested by @TK-421
    }
    try {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == trimmedEmail;
    }
    catch {
        return false;
    }
}

        public void CartConfirmation(Cart c, string customerName, string customerEmail, string customerAddress)
        {
            if(customerAddress == "" || !IsValidEmail(customerEmail)|| customerEmail =="" || customerName== "")
                throw;
            foreach(var item in c.items)
            {
                if(item.Amount < 0 ||( Dal.Product.Get(item.ID).InStock - item.Amount) < 0) throw ;
                int amountInStock = Dal.Product.Get(item.ID).InStock;
                Dal.Order.Add (new Dal.DO.Order());
                
                Dal.DO.Order o = new Dal.DO.Order();
                o.OrderID = item.ID;
                
            }
            throw new NotImplementedException();
        }

        public Cart Update(Cart c, int id, double newAmount)
        {

            int productInStock = Dal.Product.Get(id).InStock;
            OrderItem oi = c.items.Find(oi => oi.ProductID == id);
            if (oi.ID != 0)
            {
                if (newAmount == 0)
                {
                    c.items.Remove(oi);
                    c.TotalPrice -= oi.TotalPrice;
                }
                else if (newAmount > oi.Amount)
                {
                    AddProductToCart(c, id);
                }
                else if (newAmount < oi.Amount)
                {
                    oi.Amount--;
                    oi.TotalPrice -= oi.Price;
                    c.TotalPrice -= oi.Price;
                }


                return c;
            }
            else

                throw new OutOfStockException();
        }
    }
}
