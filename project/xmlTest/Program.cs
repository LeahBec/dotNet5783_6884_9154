using Dal.DO;
using Dal;
using DalApi;
Dal.DalXml d =(DalXml) DalApi.Factory.Get();

Order order = new Order();
order.OrderID = 500003;
order.CustomerName = "blablalba";
order.CustomerAdress = "ajskdhf";
order.CustomerEmail = "lkj";
order.OrderDate = null;
order.ShipDate = null;
order.DeliveryDate = null;
//d.Order.Delete(500025);
/*var a = d.Order.Get(a => a.OrderID == 500002);*/
/*var a = d.Order.GetAll();*/

Console.WriteLine("success");


//d.Order.Delete(3);/*
/*var a = d.Order.GetAll();
Console.WriteLine(d.Order.GetAll());*/

d.Order.Update(order);

Console.WriteLine("finish");

//var a = d.Product.GetAll();
/*Product product = new Product();
product.ID = 111131;
product.Name = "new";
product.Price = (float)5.5;
product.InStock = 15;
product.Category = DalFacade.DO.eCategory.Cameras;

d.Product.Add(product);
Console.WriteLine("success");*/




