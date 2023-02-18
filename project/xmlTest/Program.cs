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
order.OrderDate = DateTime?.Now;
order.ShipDate = new DateTime?(2023, 01,19) ;
order.DeliveryDate = new DateTime?(2023 - 02 - 06);
d?.Order.Add(order);*/
//d.Order.Delete(500025);
var a = d.Order.Get(a => a.OrderID == 500002);
var b = d.Order.GetAll();

Console.WriteLine(a.ToString());
/*OrderItem oi = new OrderItem();
oi.OrderID = 500020;
oi.ProductID = 100012;
oi.Price = 8888;
oi.Amount = 7;*/
//var a = d.Order.GetAll();

Console.WriteLine("success");


//d.Order.Delete(3);/*
/*var a = d.Order.GetAll();
Console.WriteLine(a.ElementAt(0).ToString());*/

//d.Order.Update(order);

Console.WriteLine("finish");

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



Console.WriteLine("end");