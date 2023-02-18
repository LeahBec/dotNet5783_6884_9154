using BO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;

namespace BlImplementation;
internal class BlProduct : BLApi.IProduct
{
    static Random rand = new Random();
    DalApi.IDal? Dal = DalApi.Factory.Get();
    public IEnumerable<BO.ProductForList> GetProductList()
    {
        try
        {
            IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.GetAll();

            //List<BO.ProductForList> productList = new List<BO.ProductForList>();
            /*  foreach (var item in existingProductsList)
              {
                  BO.ProductForList p = new BO.ProductForList();
                  p.ID = item.ID;
                  p.Name = item.Name;
                  p.Price = item.Price;
                  p.Price = item.Price;
                  p.Category = (BO.Category)item.Category;
                  productList.Add(p);
              }*/
            /*  existingProductsList.Select(item =>
              {
                  BO.ProductForList p = new BO.ProductForList();
                  p.ID = item.ID;
                  p.Name = item.Name;
                  p.Price = item.Price;
                  p.Price = item.Price;
                  p.Category = (BO.Category)item.Category;
                  productList.Add(p);
                  return item;
              }).ToList();*/

            IEnumerable<BO.ProductForList> productList = from Dal.DO.Product item1 in existingProductsList
                                                         select new BO.ProductForList
                                                         {
                                                             ID = item1.ID,
                                                             Name = item1.Name,
                                                             Price = item1.Price,
                                                             Category = (BO.Category)item1.Category,
                                                         };
            if (productList.Count() == 0)
                throw new BO.BlNoEntitiesFound("");
            return productList;
        }
        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }

        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error");
        }
    }

    public IEnumerable<BO.ProductItem> GetCatalog()
    {
        try
        {
            IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.GetAll();
            List<BO.ProductItem> productList = new List<BO.ProductItem>();
            /*foreach (var item in existingProductsList)
            {
                BO.ProductItem p = new BO.ProductItem();
                p.ID = item.ID;
                p.Name = item.Name;
                p.Price = item.Price;
                p.Category = (BO.Category)item.Category;
                p.Amount = (int)rand.NextInt64(0, 10);
                p.inStock = item.InStock >= p.Amount;
                productList.Add(p);
            }*/
            existingProductsList.Select(item =>
            {
                BO.ProductItem p = new BO.ProductItem();
                p.ID = item.ID;
                p.Name = item.Name;
                p.Price = item.Price;
                p.Category = (BO.Category)item.Category;
                p.Amount = (int)rand.NextInt64(0, 10);
                p.inStock = item.InStock >= p.Amount;
                productList.Add(p);
                return item;
            }).ToList().OrderBy(p => p.Name);


            if (productList.Count() == 0)
                throw new Exception();//NoEntitiesFound("No products found");
            return productList;
        }
        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error");
        }
    }
    public BO.Product GetProductCustomer(int id)
    {
        try
        {
            BO.Product p = new BO.Product();
            if (id > 0)
            {
                Dal.DO.Product product = Dal.Product.Get(p => p.ID == id);
                p.ID = product.ID;
                p.Name = product.Name;
                p.Price = product.Price;
                p.Category = (BO.Category)product.Category;
                p.inStock = product.InStock;
                return p;
            }
            throw new BO.BlEntityNotFoundException("");
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error");
        }
    }

    public BO.Product GetProductManager(int id)
    {
        try
        {
            BO.Product p = new BO.Product();
            if (id > 0)
            {
                Dal.DO.Product product = Dal.Product.Get(p => p.ID == id);
                p.ID = product.ID;
                p.Name = product.Name;
                p.Price = product.Price;
                p.Category = (BO.Category)product.Category;
                p.inStock = product.InStock;
                return p;
            }
            else
            {
                throw new BO.BlInvalidIntegerException();
            }
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error");
        }
    }
    public int AddProduct(BO.Product p)
    {
        try
        {
            if (p.ID <= 0)
                throw new BO.BlInvalidIdToken("");// ProductIdIsImpossible
            if (p.Name == "")
                throw new BO.BlInvalidNameToken("");// ProductNameIsImpossible
            if (p.Price <= 0)
                throw new BO.BlInvalidPriceToken("");//ProductPriceIsImpossible
            Dal.DO.Product DOProduct = new Dal.DO.Product();
            DOProduct.ID = p.ID;
            DOProduct.Name = p.Name;
            DOProduct.Price = (float)p.Price;
            DOProduct.Category = (DalFacade.DO.eCategory)p.Category;
            DOProduct.InStock = p.inStock;
            int id = (int)Dal?.Product?.Add(DOProduct);
            return id;
        }
        catch (Exception err) { throw new BO.BlDefaultException(err.Message); }
    }
    public void DeleteProduct(int id)
    {
        try
        {
            if (id <= 0)
                throw new BO.BlInvalidIdToken("");
            var orderitems = Dal.OrderItem.GetAll();
            /*foreach (var oi in orderitems)
            {
                if (oi.ID == id)
                    throw new BO.BlProductExistsInAnOrder("product exists in an order");
                //can't delete the product because a customer ordered it!
            }*/
            orderitems.Where(oi => oi.ID == id).Select(oi =>
            {
                throw new BO.BlProductExistsInAnOrder("product exists in an order");
                return oi;
            });
            Dal.Product.Delete(id);
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (BO.BlProductExistsInAnOrder)
        {
            throw new BO.BlProductExistsInAnOrder("object is ordered");
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error occured");
        }
    }

    //Help function:
    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
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
    public void Update(BO.Product p)
    {
        try
        {
            if (p.ID <= 0)
                throw new BO.BlInvalidIdToken("");// ProductIdIsImpossible
            if (p.Name == "")
                throw new BO.BlInvalidNameToken("");// ProductNameIsImpossible
            if (p.Price <= 0)
                throw new BO.BlInvalidPriceToken("");//ProductPriceIsImpossible
            if (p.inStock < 0)
                throw new BO.blInvalidAmountToken("");
            Dal.DO.Product DOProduct = new Dal.DO.Product();
            DOProduct.ID = p.ID;
            DOProduct.Name = p.Name;
            DOProduct.Price = (float)p.Price;
            DOProduct.Category = (DalFacade.DO.eCategory)p.Category;
            DOProduct.InStock = p.inStock;
            Dal?.Product.Update(DOProduct);
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("product not found");
        }
        catch (BO.BlInvalidIdToken)
        {
            throw new BO.BlInvalidIdToken("invalid id token");
        }
        catch (BO.BlInvalidNameToken)
        {
            throw new BO.BlInvalidNameToken("invalid name token");
        }
        catch (BO.BlInvalidPriceToken)
        {
            throw new BO.BlInvalidPriceToken("invalid price token");
        }
        catch (BO.blInvalidAmountToken)
        {
            throw new BO.blInvalidAmountToken("invalid amount token");
        }

        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error occured");
        }
    }

    public IEnumerable<BO.ProductForList> GetListByCategory(BO.Category category)
    {
        try
        {
            IEnumerable<Dal.DO.Product> products = Dal.Product.GetAll();
            List<BO.ProductForList> returnList = new List<BO.ProductForList>();

            List<Dal.DO.Product> a = products.Where(item => (Category)item.Category == category).ToList();


            var b = from Dal.DO.Product item1 in a
                    select new BO.ProductForList
                    {
                        ID = item1.ID,
                        Name = item1.Name,
                        Price = (float)item1.Price,
                        Category = (BO.Category)item1.Category
                    };


            return b;
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlNoEntitiesFound("");
        }
    }


    //public IEnumerable<BO.ProductForList> GetProductByCategoty(BO.eCategory category)
    //{
    //    IEnumerable<Dal.DO.Product> lst = Dal.Product.GetProductByCategory((Dal.DO.eCategory)category);

    //    List<BO.ProductForList> productsForList = new List<BO.ProductForList>();


    //    foreach (Dal.DO.Product DoProduct in lst)
    //    {
    //        BO.ProductForList ProductForList = new BO.ProductForList();

    //        ProductForList.ID = BO.BoConfig.ProductForListID;
    //        ProductForList.Name = DoProduct.Name;
    //        ProductForList.Price = DoProduct.Price;
    //        ProductForList.Category = (BO.eCategory)DoProduct.Category;
    //        productsForList.Add(ProductForList);
    //    }


    //    if (productsForList.Count() == 0)
    //        throw new BO.BlNoEntitiesFound("No products found");

    //    return productsForList;
    //}

}


