using DalApi;
using Dal.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem oi)
    {
        StreamReader reader = new StreamReader("../../OrderItem.xml");
        StreamWriter writer = new StreamWriter("../../OrderItem.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderItem));
        List<OrderItem>? orderItems = (List<OrderItem>?)xmlSerializer.Deserialize(reader);
        XElement? rootConfig = XDocument.Load(@"..\..\xml\dal-config.xml").Root;
        XElement? id = rootConfig?.Element("ID");
        int orderItemId = Convert.ToInt32(id?.Value);
        orderItemId++;
        id.Value = orderItemId.ToString();
        orderItems?.Add(oi);
        xmlSerializer.Serialize(writer, orderItems);
        return oi.ID;
    }

    public void Delete(int id)
    {
        StreamReader reader = new StreamReader("../../OrderItem.xml");
        StreamWriter writer = new StreamWriter("../../OrderItem.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderItem));
        List<OrderItem>? OrderItems = (List<OrderItem>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        OrderItem pro = OrderItems.Where(oi => oi.ID==id).FirstOrDefault();
        OrderItems.Remove(pro);
        xmlSerializer.Serialize(writer, OrderItems);
        writer.Close();
        throw new NotImplementedException();
    }

    public OrderItem Get(Func<OrderItem, bool> func)
    {
        StreamReader reader = new StreamReader("../../OrderItem.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderItem));
        List<OrderItem>? orderItems = (List<OrderItem>?)xmlSerializer.Deserialize(reader);
        return (func == null ? orderItems : orderItems.Where(func).ToList()).FirstOrDefault();
    }

    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool> func = null)
    {
        StreamReader reader = new StreamReader("../../OrderItem.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderItem));
        List<OrderItem>? orderItems = (List<OrderItem>?)xmlSerializer.Deserialize(reader);
        return orderItems;
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItem> getByOrderId(int orderId)
    {
        StreamReader reader = new StreamReader("../../OrderItem.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderItem));
        List<OrderItem>? OrderItems = (List<OrderItem>?)xmlSerializer.Deserialize(reader);
        List<OrderItem>? ans = new List<OrderItem>();
        ans = (List<OrderItem>)OrderItems.Where(oit => oit.OrderID==orderId);
        return ans;
    }

    public void Update(OrderItem oi)
    {
        StreamReader reader = new StreamReader("../../OrderItem.xml");
        StreamWriter writer = new StreamWriter("../../OrderItem.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderItem));
        List<OrderItem>? OrderItems = (List<OrderItem>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        OrderItem orit = OrderItems.Where(oit => oit.ID==oi.ID).FirstOrDefault();
        OrderItems.Remove(orit);
        OrderItems.Add(orit);
        xmlSerializer.Serialize(writer, OrderItems);
        writer.Close();
        throw new NotImplementedException();
    }
}

