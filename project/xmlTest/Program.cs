using Dal.DO;
using Dal;
Dal.DalXml d = new Dal.DalXml();

//Order order= new Order();
//order.ID = 24;
//order.CustomerName = "ttami";
//order.CustomerAddress = null;
//order.CustomerEmail = "ssuri";
//order.OrderDate= null;
//order.ShipDate= DateTime.Today;
//order.DeliveryDate= null;
////d.Order.Create(order);

////d.Order.Delete(3);

//Console.WriteLine(d.Order.Read());

//d.Order.Update(order);

//Console.WriteLine("finish");


Product product = new Product();
product.ID = 111111;
product.Name = "new";
product.Price = (float)5.5;
product.InStock = 15;
product.Category = DalFacade.DO.eCategory.Cameras;

d.Product.Add(product);
Console.WriteLine("success");




