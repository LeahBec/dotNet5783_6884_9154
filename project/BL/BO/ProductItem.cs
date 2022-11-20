using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductItem
    {
        int ID { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        Category Category { get; set; }
        int Amount { get; set; }
        bool inStock { get; set; }

        public override string ToString()
        {
            string productItem = $@"
        Order id: {ID}
        Name: {Name}, 
        Price : {Price}
        Category : {Category}
        Amount : {Amount}
        in Stock : {inStock}";
            return productItem;
        }
    }
}
