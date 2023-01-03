using DalApi;
using Dal.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;
internal class DalOrder : IOrder
{
    public int Add(Order order)
    {
        XElement? orderElement = XDocument.Load(@"../../../../xml/Order.xml").Root;
        XElement? orderId = XDocument.Load(@"../../../../xml/dal-config.xml").Root;
        XElement? orderId1 = orderId?.Element("ids")?.Element("orderId");
        XElement? order1 = new XElement("order",
        new XElement("OrderID", orderId1),
        new XElement("CustomerName", order.CustomerName),
        new XElement("CustomerAdress", order.CustomerAdress),
        new XElement("CustomerEmail", order.CustomerEmail),
        new XElement("ShipDate", order.ShipDate));
        new XElement("DeliveryDate", order.DeliveryDate);
        new XElement("OrderDate", order.OrderDate);
        orderElement?.Add(order1);
        orderElement?.Save(@"../../../../xml/Order.xml");
        //var id = int.Parse(orderId.Value);
        int id = int.Parse(orderId1.Value);
        id++;
        
        //orderId.SetAttributeValue("orderId", id);
        orderId1.Value = Convert.ToString( id);
        //orderId = (XElement)id;
        //orderId?.Save(@"../../../../xml/dal-config.xml");
        /* var doc = XElement.Load(fileName);
         var saveGame = doc
       .Element("savegames")
       .Elements("savegame")
       .Where(e => e.Element("IdNumber").Value == "2")
       .Single();

         saveGame.Element("balance").Value = "50";*/
        /*
                doc.Save(fileName);*/
        return order.OrderID;

        /*       XElement? rootConfig = XDocument.Load(@"..\..\..\..\xml\dal-config.xml").Root;
               XElement? id = rootConfig?.Element("OrderID");
               int orderID = Convert.ToInt32(id?.Value);
               orderID++;
               id.Value = orderID.ToString();
               rootConfig?.Save("../../../../xml/dal-config.xml");
               XElement o = new("order",
                               new XElement("OrderID", orderID),
                               new XElement("CustomerName", order.CustomerName),
                               new XElement("CustomerEmail", order.CustomerEmail),
                               new XElement("CustomerAddress", order.CustomerAdress),
                               new XElement("OrderDate", order.OrderDate),
                               new XElement("ShipDate", order.ShipDate),
                               new XElement("DeliveryDate", order.DeliveryDate));
               XElement? root = XDocument.Load("../../xml/Order.xml").Root;
               root?.Add(o);
               root?.Save("../../xml/Order.xml");
               return order.OrderID;
               //throw new NotImplementedException();*/
    }
    /*    public void Delete(int id)
        {
            XElement? root = XDocument.Load("../../../../xml/Order.xml").Root;
            XElement? order = root?.Elements("order")?.
                Where(o => o.Element("OrderID")?.Value == id.ToString()).FirstOrDefault();
            if (order == null)
                throw new NotImplementedException(); //
            order.Remove();
            root?.Save("../../../../xml/Order.xml");
        }*/

    public void Delete(int id)
    {
        XElement? root = XDocument.Load("../../../../xml/Order.xml").Root;
        root?.Descendants("order").Where(p => int.Parse(p?.Element("OrderID").Value) == id).Remove();
        root?.Save("../../../../xml/Order.xml");
    }

    public Dal.DO.Order deepCopy(XElement? o)
    {
        Dal.DO.Order order = new Order();
        order.OrderID = Convert.ToInt32(o?.Element("OrderID")?.Value);
        order.CustomerName = o?.Element("CustomerName")?.Value;
        order.CustomerEmail = o?.Element("CustomerEmail")?.Value;
        order.CustomerAdress = o?.Element("CustomerAdress")?.Value;
        order.OrderDate = Convert.ToDateTime(o?.Element("OrderDate")?.Value);
        order.ShipDate = Convert.ToDateTime(o?.Element("ShipDate")?.Value);
        order.DeliveryDate = Convert.ToDateTime(o?.Element("DeliveryDate")?.Value);
        return order;
    }
    public Order Get(Func<Order, bool> func)
    {
        IEnumerable<Dal.DO.Order> orders = GetAll();
        return (func == null ? orders : orders.Where(func).ToList()).FirstOrDefault();
    }

    public IEnumerable<Order> GetAll(Func<Order, bool> func = null)
    {
        XElement? root = XDocument.Load("../../../../xml/Order.xml")?.Root;
        IEnumerable<XElement>? orderList = root?.Descendants("order")?.ToList();
        List<Dal.DO.Order> orders = new List<Order>();
        foreach (var xOrder in orderList)
        {
            orders.Add(deepCopy(xOrder));
        }

        return (func == null ? orders : orders.Where(func).ToList());
        throw new NotImplementedException();
    }

    public void Update(Order ord)
    {
        XElement? root = XDocument.Load("../../../../xml/Order.xml").Root;
        XElement? order = root?.Elements("order")?.Where(o => o.Element("OrderID")?.Value == ord.OrderID.ToString()).FirstOrDefault();
        if (order == null)
            throw new NotImplementedException(); //
        XElement o = new("order",
                        new XElement("OrderID", ord.OrderID),
                        new XElement("CustomerName", ord.CustomerName),
                        new XElement("CustomerEmail", ord.CustomerEmail),
                        new XElement("CustomerAddress", ord.CustomerAdress),
                        new XElement("OrderDate", ord.OrderDate),
                        new XElement("ShipDate", ord.ShipDate),
                        new XElement("DeliveryDate", ord.DeliveryDate));
        order.Remove();
        root?.Add(o);
        root?.Save("../../../../xml/Order.xml");
        // throw new NotImplementedException();
    }
}

