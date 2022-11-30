﻿using BLApi;

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
            BO.OrderItem oi = cart.items.Find(oi => oi.ProductID == productId);
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
                    oi.ID = productId;
                    oi.ProductName = productName;
                    oi.Amount = 1;
                    cart.items.Add(oi);
                    cart.TotalPrice += productPrice;
                }
                return cart;
            }
            else
                throw new Exception();//OutOfStockException();
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

    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    public void CartConfirmation(BO.Cart c, string customerName, string customerEmail, string customerAddress)
    {
        try
        {
            if (customerAddress == "" || !IsValidEmail(customerEmail) || customerEmail == "" || customerName == "")
                throw new Exception();
            foreach (var item in c.items)
            {
                if (item.Amount < 0 || (Dal.Product.Get(item.ID).InStock - item.Amount) < 0) throw new Exception();
                int amountInStock = Dal.Product.Get(item.ID).InStock;
                Dal.Order.Add(new Dal.DO.Order());

                Dal.DO.Order o = new Dal.DO.Order();
                o.OrderID = item.ID;
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
            BO.OrderItem oi = c.items.Find(oi => oi.ProductID == id);
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

                throw new Exception();//OutOfStockException();

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
