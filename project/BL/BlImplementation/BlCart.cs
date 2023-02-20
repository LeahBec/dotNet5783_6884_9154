﻿using BLApi;
using BO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace BlImplementation;
internal class BlCart : ICart
{
    DalApi.IDal? Dal = DalApi.Factory.Get();
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart AddProductToCart(BO.Cart cart, int productId)
    {
        try
        {
            int productInStock = Dal.Product.Get(p => p.ID == productId).InStock;
            double productPrice = Dal.Product.Get(p => p.ID == productId).Price;
            string productName = Dal.Product.Get(p => p.ID == productId).Name;
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
                throw new BO.BlOutOfStockException();
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (BO.BlOutOfStockException)
        {
            throw new BO.BlOutOfStockException();
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error occured");
        }
    }
    bool IsValidEmail(string _email)
    {
        string email = _email;
        var mail = new MailAddress(email);
        bool isValidEmail = mail.Host.Contains(".");
        if (!isValidEmail)
        {
            return false;
        }
        return true;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void CartConfirmation(BO.Cart c, string customerName, string customerEmail, string customerAddress)
    {
        try
        {
            if (customerAddress == "" || !IsValidEmail(customerEmail) || customerEmail == "" || customerName == "")
                throw new Exception();
            c.items.Select(item =>
                {
                    if (item.Amount < 0 || (Dal.Product.Get(p => p.ID == item.ProductID).InStock - item.Amount) < 0)
                        throw new Exception();
                    int amountInStock = Dal.Product.Get(p => p.ID == item.ProductID).InStock;
                    Dal.DO.Order o = new Dal.DO.Order();
                    o.OrderID = 0;
                    o.OrderDate = DateTime.Now;
                    o.DeliveryDate = null;
                    o.ShipDate = null;
                    o.CustomerAddress = customerAddress;
                    o.CustomerName = customerName;
                    o.CustomerEmail = customerEmail;
                    int id = Dal.Order.Add(o);
                    List<Dal.DO.OrderItem> allItems = Dal.OrderItem.GetAll().ToList();

                    var cartItems = from BO.OrderItem item1 in c.items
                                    select new BO.OrderItem
                                    {
                                        ID = item1.ID,
                                        Amount = item1.Amount,
                                        Price = item1.Price,
                                        ProductID = item1.ProductID,
                                        ProductName = item1.ProductName,
                                        TotalPrice = item1.TotalPrice
                                    };

                   var items1 = from BO.OrderItem item1 in c.items
                                    select new BO.OrderItem
                                    {
                                        ID = item1.ID,
                                        Amount = item1.Amount,
                                        Price = item1.Price,
                                        ProductID = item1.ProductID,
                                        ProductName = item1.ProductName,
                                        TotalPrice = item1.TotalPrice
                                    };
                    return items1;
                }).ToList();

        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart Update(BO.Cart c, int id, double newAmount)
    {
        try
        {

            int productInStock = Dal.Product.Get(p => p.ID == id).InStock;
            if (productInStock < newAmount) throw new BlOutOfStockException();
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
            throw new BO.BlEntityNotFoundException("");
        }
        catch (BlOutOfStockException ex)
        {
            throw new BO.BlOutOfStockException();
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error occured");
        }
    }
}
