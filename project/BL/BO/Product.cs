using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Product
    {
        int ID { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        Category Category { get; set; }
        int inStock { get; set; }

        public override string ToString()
        {
            string product = $@"
        Order id: {ID}
        Name: {Name}, 
        Price : {Price}
        Category : {Category}
        in Stock : {inStock}";
            return product;
        }
    }
}
