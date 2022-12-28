using DalApi;
using Dal.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem obj)
    {

        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public OrderItem Get(Func<OrderItem, bool> func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool> func = null)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItem> getByOrderId(int orderId)
    {
        throw new NotImplementedException();
    }

    public void Update(OrderItem obj)
    {
        throw new NotImplementedException();
    }
}

