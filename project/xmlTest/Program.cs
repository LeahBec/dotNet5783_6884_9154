using Dal.DO;
using Dal;
using DalApi;
using System.Xml.Linq;
using System.Text;

Dal.DalXml d = (DalXml)DalApi.Factory.Get();

/*Order order = new Order();
order.CustomerName = "ppp";
order.CustomerAdress = "kkk";
order.CustomerEmail = "lkj";
order.OrderDate = null;
order.ShipDate = null;
order.DeliveryDate = null;
d?.Order.Add(order);*/
//d.Order.Delete(500025);
/*var a = d.Order.Get(a => a.OrderID == 500002);
var a = d.Order.GetAll();*/
/*
OrderItem oi = new OrderItem();
oi.OrderID = 500020;
oi.ProductID = 100012;
oi.Price = 8888;
oi.Amount = 7;
var a = d.OrderItem.Add(oi);*/

Console.WriteLine("success");


//d.Order.Delete(3);/*
/*var a = d.Order.GetAll();
Console.WriteLine(d.Order.GetAll());*/
/*
d.Order.Update(order);

Console.WriteLine("finish");*/

//var a = d.Product.GetAll();
/*Product product = new Product();
product.Name = "id";
product.Price = (float)88;
product.InStock = 15;
product.Category = DalFacade.DO.eCategory.SmartWatches;

d.Product.Add(product);
*/
Console.WriteLine("success");

/*using System.Text;

int val = 4;
var builder = new StringBuilder();

builder.Append("There are ");
builder.Append(val).ToString();
builder.Append(" hawks");

Console.WriteLine(builder);


*/


/*for (int i = 0; i < 20; i++)
{
    Order order = new Order();
    order.OrderID = 500000 + i;
    //order.CustomerName = "a" + Convert.ToChar(i);
    order.CustomerName = Encoding.ASCII.GetString(new byte[] { ((byte)(i + 96)) });
    order.CustomerAdress = "i am" + (char)96+i;
    order.CustomerEmail = ("a" + Convert.ToChar(i));
    order.OrderDate = DateTime.Today;
    order.DeliveryDate = DateTime.MinValue;
    order.ShipDate = DateTime.MinValue;

    XElement? orderElement = XDocument.Load(@"../../../../xml/Order.xml").Root;
    XElement? orderId = XDocument.Load(@"../../../../xml/dal-config.xml").Root;
    XElement? orderId1 = orderId?.Element("ids")?.Element("orderId");
    XElement? order1 = new XElement("order",
    new XElement("OrderID", 500000 + i),
    new XElement("CustomerName", ('a' + (char)i).ToString()),
    new XElement("CustomerAdress", ("i am" + (char)96 + i)),
    new XElement("CustomerEmail", ('a' + (char)i).ToString()),
    new XElement("ShipDate", DateTime.Today),
    new XElement("DeliveryDate", DateTime.MinValue),
    new XElement("OrderDate", DateTime.MinValue));
    orderElement?.Add(order1);
    orderElement?.Save(@"../../../../xml/Order.xml");

    int id = int.Parse(orderId1.Value);
    id++;

    orderId1.Value = Convert.ToString(id);

}*/

Console.WriteLine("end");