using BLApi;

using DalApi;

namespace BlImplementation;
internal class BlCart : ICart
{
    private IDal Dal = new Dal.DalList();
    public BO.Cart AddProductToCart(BO.Cart cart, int productId)
    {
        try
        {
            int productInStock = Dal.Product.Get(productId).InStock;
            double productPrice = Dal.Product.Get(productId).Price;
            string productName = Dal.Product.Get(productId).Name;
            //int orderId = Dal.OrderItem.Get()
            BO.OrderItem oi = cart.items.Find(item => item.ProductID == productId);
            if (productInStock > 0)
            {
                if (oi != null)
                {
                    oi.Amount++;
                    oi.TotalPrice += productPrice;
                    cart.TotalPrice += productPrice;
                    return cart;
                }
                else
                {
                    oi = new BO.OrderItem();
                    oi.Price = productPrice;
                    oi.TotalPrice = productPrice;
                    oi.ProductID = productId;
                    oi.ProductName = productName;
                    oi.Amount = 1;
                    cart.items.Add(oi);
                    cart.TotalPrice += productPrice;
                }
                return cart;
            }
            else
                throw new BO.BlOutOfStockException();//OutOfStockException();
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException();
        }catch (BO.BlOutOfStockException)
        {
            throw new BO.BlOutOfStockException();
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error occured");
        }
    }

    bool IsValidEmail(string email)
    {
        //var trimmedEmail = email.Trim();
        return true;

        //if (trimmedEmail.EndsWith("."))
        //{
        //    return false; // suggested by @TK-421
        //}
        //try
        //{
        //    var addr = new System.Net.Mail.MailAddress(email);
        //    return addr.Address == trimmedEmail;
        //}
        //catch
        //{
        //    return false;
        //}
    }

    public void CartConfirmation(BO.Cart c, string customerName, string customerEmail, string customerAddress)
    {
        try
        {
            if (customerAddress == "" || !IsValidEmail(customerEmail) || customerEmail == "" || customerName == "")
                throw new Exception();
            foreach (var item in c.items)
            {
                if (item.Amount < 0 || (Dal.Product.Get(item.ProductID).InStock - item.Amount) < 0)
                    throw new Exception();
                int amountInStock = Dal.Product.Get(item.ProductID).InStock;

                Dal.DO.Order o = new Dal.DO.Order();
                o.OrderID = 0;
                o.OrderDate = DateTime.Now;
                o.DeliveryDate = DateTime.MinValue;
                o.ShipDate = DateTime.MinValue;
                o.CustomerAdress = customerAddress;
                o.CustomerName = customerName;
                o.CustomerEmail = customerEmail;
                int id = Dal.Order.Add(o);
                List<Dal.DO.OrderItem> allItems = Dal.OrderItem.GetAll().ToList();
                foreach (BO.OrderItem item1 in c.items)
                {
                    Dal.DO.OrderItem cartItem = new();
                    cartItem.ID = 0;
                    cartItem.Amount = item1.Amount;
                    cartItem.Price = item1.Price;
                    cartItem.OrderID = o.OrderID;
                    cartItem.ProductID = item1.ProductID;
                    Dal.OrderItem.Add(cartItem);
                    Dal.Product.updateAmount(item1.ID, item1.Amount);
                }

            }
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException();
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public BO.Cart Update(BO.Cart c, int id, double newAmount)
    {
        try
        {

            int productInStock = Dal.Product.Get(id).InStock;
            BO.OrderItem oi = c.items.Find(item => item.ProductID == id);
            if (oi != null)
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

                throw new BO.BlOutOfStockException();//OutOfStockException();

        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException();
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error occured");
        }
    }
}
