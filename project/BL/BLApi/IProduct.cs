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
        public IEnumerable<ProductItem> GetCatalog();
        public Product GetProductManager(int id);
        public Product GetProductCustomer(int id);
        public void Add(Dal.DO.Product p);
        public void Delete(int id);
        public void Update(Dal.DO.Product p);

    }
}
