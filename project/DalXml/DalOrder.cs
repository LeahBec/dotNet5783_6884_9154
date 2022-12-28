using DalApi;
using Dal.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
internal class DalOrder : IOrder
{
    public int Add(Order obj)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Order Get(Func<Order, bool> func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAll(Func<Order, bool> func = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Order obj)
    {
        throw new NotImplementedException();
    }
}

