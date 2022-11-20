

using BO;
using DalApi;
namespace BlImplementation
{
    internal class BlProduct : BLApi.IProduct
    {
        private IDal Dal = new Dal.DalList();

        public void Add(Product p)
        {
            Dal.Product.Add(p);
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductForList> GetProductList()
        {
            IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.GetAll();

            List<ProductForList> productList = new List<ProductForList>();
            foreach (var item in existingProductsList)
            {
                ProductForList p = new ProductForList();
                p.ID = item.ID;
                p.Name = item.Name;
                p.Price = item.Price;
                p.Category = (Category)item.Category;
                productList.Add(p);
            }

            if (productList.Count() == 0)
               throw new NoEntitiesFound("No products found");
            return productList;

        }


        public IEnumerable<ProductItem> GetCatalog()
        {
            IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.GetAll();

            List<ProductItem> productList = new List<ProductItem>();
            foreach (var item in existingProductsList)
            {
                ProductItem p = new ProductItem();

                p.ID = item.ID;
                p.Name = item.Name;
                p.Price = item.Price;
                p.Category = (Category)item.Category;
                //p.Amount = 1; במה לאתחל????
                p.inStock = Convert.ToBoolean(item.InStock);
                productList.Add(p);
            }

            if (productList.Count() == 0)
                throw new NoEntitiesFound("No products found");
            return productList;
        }

        public Product GetProductCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductManager(int id)
        {
            Product localProduct = new Product();
            if (id > 0)
            {
                Dal.DO.Product product = Dal.Product.Get(id);
                localProduct.ID = product.ID;
                localProduct.Name = product.Name;
                localProduct.Price = product.Price;
                localProduct.Category = (Category)product.Category;
                localProduct.inStock = product.InStock;
                return localProduct;
            }
            else
            {
                throw new BO.EntityNotFoundException("this product does not exist");
            }


        }

        public void Update(Product p)
        {
            throw new NotImplementedException();
        }
    }
}

