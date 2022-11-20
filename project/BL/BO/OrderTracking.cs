using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int ID { get; set; }
        public OrderStatus Status { get; set; }
        public List<(DateTime ,string)> dateAndTrack { get; set; }
        public override string ToString()
        {
            string orderTracking = $@"
        Order id: {ID}
        Status : {Status}
        Order Tracking : {dateAndTrack}";
            return orderTracking;
        }
    }
}
