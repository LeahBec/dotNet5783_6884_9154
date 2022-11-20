using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        int ID { get; set; }
        string CustomerName { get; set; }
        string CustomerEmail { get; set; }
        string CustomerAdress { get; set; }
        DateTime OrderDate { get; set; }
        OrderStatus Status { get; set; }
        DateTime ShipDate { get; set; }
        DateTime DeiveryDate { get; set; }
        OrderItem Item { get; set; }
        double TotalPrice { get; set; }

        public override string ToString()
        {
            string order = $@"
        Order id: {ID}
        Customer Name: {CustomerName}, 
        Customer Adress : {CustomerAdress}
        Customer E-mail : {CustomerEmail}
        Order Date : {OrderDate}
    	Status : {Status}
        Ship Date : {ShipDate}
        Deivery Date : {DeiveryDate}
        Item : {Item}
        total price: {TotalPrice}";
            return order;
        }

    }
}
