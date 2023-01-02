using Dal.DO;
using Dal;
using DalApi;
Dal.DalXml d =(DalXml) DalApi.Factory.Get();

/*Order order = new Order();
order.OrderID = 500025;
order.CustomerName = "ttami";
order.CustomerAdress = null;
order.CustomerEmail = "ssuri";
order.OrderDate= null;
order.ShipDate= DateTime.Today;
order.DeliveryDate= null;
//d.Order.Create(order);

//d.Order.Delete(3);

Console.WriteLine(d.Order.GetAll());

d.Order.Update(order);

Console.WriteLine("finish");*/


Product product = new Product();
product.ID = 111111;
product.Name = "new";
product.Price = (float)5.5;
product.InStock = 15;
product.Category = DalFacade.DO.eCategory.Cameras;

d.Product.Add(product);
Console.WriteLine("success");




