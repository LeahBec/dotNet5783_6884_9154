using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLApi
{
    public interface ICart
    {
        public void Add(Cart c, int id);
        public Cart Update(Cart c, int id, double newAmount);
        public void CartConfirmation(Cart c, string customerName, string customerEmail, string customerAddress);
    }
}
