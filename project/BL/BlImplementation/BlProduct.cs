﻿using DalApi;
namespace BlImplementation;
internal class BlProduct : BLApi.IProduct
{
    static Random rand = new Random();
    private IDal Dal = new Dal.DalList();
    public IEnumerable<BO.ProductForList> GetProductList()
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
            throw new Exception();//NoEntitiesFound("No products found");
        catch (ExceptionFailedToRead ex)
        {
            throw new BO.BlExceptionFailedToRead("");
        }
        return productList;

    }
    public IEnumerable<BO.ProductItem> GetCatalog()
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
    public BO.Product GetProductCustomer(int id)
    {

        BO.Product p = new BO.Product();
        if (id > 0)
        {
            Dal.DO.Product product = Dal.Product.Get(id);
            p.ID = product.ID;
            p.Name = product.Name;
            p.Price = product.Price;
            p.Category = (BO.Category)product.Category;
            p.inStock = product.InStock;
            return p;
        }
        else
        {
            throw new Exception();//EntityNotFoundException("this product does not exist");
        }
    }
    public BO.Product GetProductManager(int id)
    {
        BO.Product p = new BO.Product();
        if (id > 0)
        {
            Dal.DO.Product product = Dal.Product.Get(id);
            p.ID = product.ID;
            p.Name = product.Name;
            p.Price = product.Price;
            p.Category = (BO.Category)product.Category;
            p.inStock = product.InStock;
            return p;
        }
        else
        {
            throw new Exception();//EntityNotFoundException("this product does not exist");
        }
    }
    public void AddProduct(BO.Product p)
    {
        //check input!!!!
        // add try and catch on all the block
        if (p.ID <= 0)
            throw new Exception();// ProductIdIsImpossible
        if (p.Name == "")
            throw new Exception();// ProductNameIsImpossible
        if (p.Price <= 0)
            throw new Exception();//ProductPriceIsImpossible
        Dal.DO.Product DOProduct = new Dal.DO.Product();
        DOProduct.ID = p.ID;
        DOProduct.Name = p.Name;
        DOProduct.Price = (float)p.Price;
        DOProduct.Category = (DalFacade.DO.eCategory)p.Category;
        DOProduct.InStock = p.inStock;
        Dal.Product.Add(DOProduct);
        //throw new NotImplementedException();
    }
    public void DeleteProduct(int id)
    {
        //check input!!!!
        // add try and catch on all the block
        var orderitems = Dal.OrderItem.GetAll();
        foreach (var oi in orderitems)
        {
            if (oi.ID == id)
                throw new Exception("product exists in an order");
            //cant delete the product because a customer ordered it!
        }
        Dal.Product.Delete(id);
        //throw new NotImplementedException();
    }
    public void Update(BO.Product p)
    {
        //check input!!!! + throw
        // add try and catch on all the block
        Dal.DO.Product DOProduct = new Dal.DO.Product();
        DOProduct.ID = p.ID;
        DOProduct.Name = p.Name;
        DOProduct.Price = (float)p.Price;
        DOProduct.Category = (DalFacade.DO.eCategory)p.Category;
        DOProduct.InStock = p.inStock;
        Dal.Product.Update(DOProduct);
        //throw new NotImplementedException();
    }
}


