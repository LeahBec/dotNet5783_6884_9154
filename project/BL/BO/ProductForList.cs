using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductForList
    {
        int ID { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        Category Category { get; set; }

        public override string ToString()
        {
            string productForList = $@"
        Order id: {ID}
        Name: {Name}, 
        Price : {Price}
        Category : {Category}";
            return productForList;
        }
    }
}
