using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        int ID { get; set; }
        string CustomerName { get; set; }
        OrderStatus Status { get; set; }
        int AmountOfItems { get; set; }
        double TotalPrice{ get; set; }

        public override string ToString()
        {
            string order = $@"
        Order id: {ID}
        Customer Name: {CustomerName}, 
    	Status : {Status}
        Amount Of Items : {AmountOfItems}
        Total Price : {TotalPrice}";
            return order;
        }

    }
}
