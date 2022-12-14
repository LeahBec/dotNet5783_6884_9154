using DalApi;
namespace BlImplementation;
internal class BlProduct : BLApi.IProduct
{
    static Random rand = new Random();
    private IDal Dal = new Dal.DalList();
    public IEnumerable<BO.ProductForList> GetProductList()
    {
        try
        {
            IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.GetAll();

            List<BO.ProductForList> productList = new List<BO.ProductForList>();
            foreach (var item in existingProductsList)
            {
                BO.ProductForList p = new BO.ProductForList();
                p.ID = item.ID;
                p.Name = item.Name;
                p.Price = item.Price;
                p.Price = item.Price;
                p.Category = (BO.Category)item.Category;
                productList.Add(p);
            }
            if (productList.Count() == 0)
                throw new BO.BlNoEntitiesFound("");
            return productList;
        }
        catch (ExceptionFailedToRead)
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
            foreach (var item in existingProductsList)
            {
                BO.ProductItem p = new BO.ProductItem();
                p.ID = item.ID;
                p.Name = item.Name;
                p.Price = item.Price;
                p.Category = (BO.Category)item.Category;
                p.Amount = (int)rand.NextInt64(0, 10);
                p.inStock = item.InStock >= p.Amount;
                productList.Add(p);
            }


            if (productList.Count() == 0)
                throw new Exception();//NoEntitiesFound("No products found");
            return productList;
        }
        catch (ExceptionFailedToRead)
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
            throw new BO.BlEntityNotFoundException();
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException();
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
            throw new BO.BlEntityNotFoundException();
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error");
        }
    }
    public void AddProduct(BO.Product p)
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
            Dal.Product.Add(DOProduct);
        }
        catch (Exception) { throw new BO.BlDefaultException(""); }
    }
    public void DeleteProduct(int id)
    {
        try
        {
            if (id <= 0)
                throw new BO.BlInvalidIdToken("");
            var orderitems = Dal.OrderItem.GetAll();
            foreach (var oi in orderitems)
            {
                if (oi.ID == id)
                    throw new BO.BlProductExistsInAnOrder("product exists in an order");
                //can't delete the product because a customer ordered it!
            }
            Dal.Product.Delete(id);
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException();
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
            Dal.Product.Update(DOProduct);
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

    public IEnumerable<BO.ProductForList> GetListByCategory(BO.Category category)
    {
        try
        {
            IEnumerable<Dal.DO.Product> products = Dal.Product.GetAll();
            List<BO.ProductForList> returnList = new List<BO.ProductForList>();
            foreach (var item in products)
            {
                if ((BO.Category)item.Category == category)
                {
                    BO.ProductForList BOProduct = new BO.ProductForList();
                    BOProduct.ID = item.ID;
                    BOProduct.Name = item.Name;
                    BOProduct.Price = (float)item.Price;
                    BOProduct.Category = (BO.Category)item.Category;
                    returnList.Add(BOProduct);
                }
            }
            return returnList;
        }
        catch (ExceptionObjectNotFound)
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


