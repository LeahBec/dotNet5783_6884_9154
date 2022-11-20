using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        int ID { get; set; }
        string Name { get; set; }
        int ProductID { get; set; }
        double Price { get; set; }
        int Amount { get; set; }
        double TotalPrice { get; set; }

        public override string ToString()
        {
            string orderItem = $@"
        Order id: {ID}
        Name: {Name}, 
    	Product ID : {ProductID}
        Price : {Price}
        Amount : {Amount}
        Total Price : {TotalPrice}";
            return orderItem;
        }
    }
}
