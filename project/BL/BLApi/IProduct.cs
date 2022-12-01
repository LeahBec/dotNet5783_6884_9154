namespace BLApi
{
    public interface IProduct
    {/// <summary>
/// the interface implements additional functions for the BL - Product entity
/// </summary>
        public IEnumerable<BO.ProductForList> GetProductList();
        public IEnumerable<BO.ProductItem> GetCatalog();
        public BO.Product GetProductManager(int id);
        public BO.Product GetProductCustomer(int id);
        public void AddProduct(BO.Product p);
        public void DeleteProduct(int ProductId);
        public void Update(BO.Product p);

    }
}
