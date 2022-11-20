using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLApi
{
    public interface IProduct
    {
        public IEnumerable<ProductForList> GetProductList();
        public IEnumerable<ProductItem> GetProductItems();
        public Product GetProductForManager(int id);
        public Product GetProductForCustomer(int id);
        public void AddProductForManager(Product p);
        public void DeleteProductForManager(int id);
        public void UpdateProductForManager(Product p);

    }
}
