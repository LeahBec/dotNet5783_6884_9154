// See https://aka.ms/new-console-template for more information
using DalList;
using Dal.DO;
using DalFacade.DO;

const int IDX = 500000;


// =============order functions=============
void addOrder()
{
    int _id;
    string _name;
    string _email;
    string _address;
    DateTime _oDate;
    DateTime _sDate;
    DateTime _dDate;
    Console.WriteLine("enter costumer name");
    _name = Console.ReadLine();
    Console.WriteLine("enter costumer email");
    _email = Console.ReadLine();
    Console.WriteLine("enter costumer address");
    _address = Console.ReadLine();
    _oDate = DateTime.Today;
    //(DateTime)DateTime.Now.ToShortDateString()
    TimeSpan shipSpan = TimeSpan.FromDays(10);
    TimeSpan deliverySpan = TimeSpan.FromDays(25);
    _sDate = _oDate + shipSpan;
    _dDate = _sDate + deliverySpan;
    _id = DataSource.Config.OrderIndex++ + IDX;
    Order newOrder = new Order();
    newOrder.OrderID = _id;
    newOrder.CustomerName = _name;
    newOrder.CustomerEmail = _email;
    newOrder.CustomerAdress = _address;
    newOrder.OrderDate = _oDate;
    newOrder.ShipDate = _sDate;
    newOrder.DeliveryDate = _dDate;
    DalOrder.Create(newOrder);

}

void viewOrder()
{
    Console.WriteLine("enter order id");
    int id = int.Parse(Console.ReadLine());
    Order order = new Order();
    order = DalOrder.ReadSingle(id);
    Console.WriteLine(order.OrderID + order.CustomerName + order.CustomerEmail + order.CustomerAdress + order.OrderDate + order.ShipDate + order.DeliveryDate);
}

void viewOrderList()
{
    Order[] orders = new Order[100];
    orders = DalOrder.Read();
    int amountOfOrders = DataSource.Config.OrderIndex;
    if (amountOfOrders == 0) { Console.WriteLine("no orders were found"); return; }
    for (int i = 0; i < amountOfOrders; i++)
    {
        Console.WriteLine(orders[i].OrderID +" "+ orders[i].CustomerName + " " + orders[i].CustomerEmail + " " + orders[i].CustomerAdress + " " + orders[i].OrderDate + " " + orders[i].ShipDate + " " + orders[i].DeliveryDate);
    }
}

void updateOrder()
{
    int _id = int.Parse(Console.ReadLine());
    string _name;
    string _email;
    string _address;
    DateTime _oDate;
    DateTime _sDate;
    DateTime _dDate;
    Console.WriteLine("enter costumer name");
    _name = Console.ReadLine();
    Console.WriteLine("enter costumer email");
    _email = Console.ReadLine();
    Console.WriteLine("enter costumer address");
    _address = Console.ReadLine();
    _oDate = DateTime.Today;
    TimeSpan shipSpan = TimeSpan.FromDays(10);
    TimeSpan deliverySpan = TimeSpan.FromDays(25);
    _sDate = _oDate + shipSpan;
    _dDate = _sDate + deliverySpan;
    Order newOrder = new Order();
    newOrder.OrderID = _id;
    newOrder.CustomerName = _name;
    newOrder.CustomerEmail = _email;
    newOrder.CustomerAdress = _address;
    newOrder.OrderDate = _oDate;
    newOrder.ShipDate = _sDate;
    newOrder.DeliveryDate = _dDate;
    DalOrder.Update(newOrder);
}

void deleteOrder()
{

    int id;
    Console.WriteLine("enter id of the order you want to delete");
    id = int.Parse(Console.ReadLine());
    DalOrder.Delete(id);
}

void orders()
{
    Console.WriteLine("1. add new order. 2. view order. 3. view orders list. 4. update order. 5. delete order.");
    int choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            addOrder();
            break;
        case 2:
            viewOrder();
            break;
        case 3:
            viewOrderList();
            break;
        case 4:
            updateOrder();
            break;
        case 5:
            deleteOrder();
            break;

    }

}

// ==========finish order functions============

// ============orderItems functions============

void addOrderItem()
{
    int id;
    int orderId;
    int productId;
    int amount;
    float price;

    id = DataSource.Config.OrderItemIndex;
    Console.WriteLine("enter order id");
    orderId = int.Parse(Console.ReadLine());
    Console.WriteLine("enter product id");
    productId = int.Parse(Console.ReadLine());
    Console.WriteLine("enter amount");
    amount = int.Parse(Console.ReadLine());
    Console.WriteLine("enter price");
    price = Single.Parse(Console.ReadLine());

    OrderItem orderItem = new OrderItem();
    orderItem.ID = id;
    orderItem.OrderID = orderId;
    orderItem.ProductID = productId;
    orderItem.Amount = amount;
    orderItem.Price = price;
    DalOrderItem.Create(orderItem);

}

void viewOrderItem()
{
    Console.WriteLine("enter order item id");
    int id = int.Parse(Console.ReadLine());
    OrderItem orderItem = new OrderItem();
    orderItem = DalOrderItem.ReadSingle(id);
    Console.WriteLine(orderItem.ID + orderItem.OrderID + orderItem.ProductID + orderItem.Price + orderItem.Amount);
}
void viewOrderListItem()
{
    OrderItem[] orderItems = new OrderItem[200];
    orderItems = DalOrderItem.Read();
    for (int i = 0; i < orderItems.Length; i++)
    {
        Console.WriteLine(orderItems[i].ID + orderItems[i].OrderID + orderItems[i].ProductID + orderItems[i].Price + orderItems[i].Amount);

    }
}
void updateOrderItem()
{
    int id;
    int orderId;
    int productId;
    int amount;
    float price;

    Console.WriteLine("enter order item id");
    id = int.Parse(Console.ReadLine());
    Console.WriteLine("enter order id");
    orderId = int.Parse(Console.ReadLine());
    Console.WriteLine("enter product id");
    productId = int.Parse(Console.ReadLine());
    Console.WriteLine("enter amount");
    amount = int.Parse(Console.ReadLine());
    Console.WriteLine("enter price");
    price = Single.Parse(Console.ReadLine());

    OrderItem orderItem = new OrderItem();
    orderItem.ID = id;
    orderItem.OrderID = orderId;
    orderItem.ProductID = productId;
    orderItem.Amount = amount;
    orderItem.Price = price;
    DalOrderItem.Update(orderItem);
}
void deleteOrderItem()
{
    int id;
    Console.WriteLine("enter id of the order item you want to delete");
    id = int.Parse(Console.ReadLine());
    DalOrderItem.Delete(id);
}
void orderItems()
{
    Console.WriteLine("1. add new order item. 2. view order item. 3. view orders items list. 4. update order item. 5. delete order item.");
    int choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            addOrderItem();
            break;
        case 2:
            viewOrderItem();
            break;
        case 3:
            viewOrderListItem();
            break;
        case 4:
            updateOrderItem();
            break;
        case 5:
            deleteOrderItem();
            break;

    }
}

// ============finish orderItems functions============


// ============product help functions============

Product createProduct()
{
    string name;
    int id = DataSource.Config.ProductIndex++;
    eCategory category;
    float price;
    int inStock;
    Console.WriteLine("enter name for the new product");
    name = Console.ReadLine();
    Console.WriteLine("enter the product's category: 1 - Drones, 2 - Cameras, 3 - Headphones, 4 -  Computers, 5 - SmartWatches");
    int choice;
    choice = int.Parse(Console.ReadLine());
    category = (eCategory)choice;
    Console.WriteLine("enter price for the new product");
    price = Single.Parse(Console.ReadLine());
    Console.WriteLine("enter amount in stock");
    Console.WriteLine("enter the amout of product in stock");
    inStock = int.Parse(Console.ReadLine());
    Product product = new Product();
    product.ID = id;
    product.Name = name;
    product.Price = price;
    product.InStock = inStock;
    product.Category = category;
    return product;

}

// ============products functions============
void addProduct()
{

    Product newProduct = createProduct();
    DalProduct.Create(newProduct);

}
void viewProduct()
{
    int id;
    Console.WriteLine("enter id of the product you want to watch");
    id = int.Parse(Console.ReadLine());
    Product product = new Product();
    product = DalProduct.ReadSingle(id);
    Console.WriteLine(product.ID + product.Name + product.Price + product.InStock);

}
void viewProductList()
{
    Product[] products = new Product[50];
    products = DalProduct.Read();
    for (int i = 0; i < products.Length; i++)
    {
        Console.WriteLine(products[i].ID + products[i].Name + products[i].Price + products[i].InStock);
    }
}

void updateProduct()
{
    eCategory category;
    float price;
    int inStock;
    Console.WriteLine("enter id for the new product");
    int id = int.Parse(Console.ReadLine());
    Console.WriteLine("enter name for the new product");
    string name = Console.ReadLine();
    Console.WriteLine("enter the product's category: 1 - Drones, 2 - Cameras, 3 - Headphones, 4 -  Computers, 5 - SmartWatches");
    int choice;
    choice = int.Parse(Console.ReadLine());
    category = (eCategory)choice;
    Console.WriteLine("enter price for the new product");
    price = Single.Parse(Console.ReadLine());
    Console.WriteLine("enter amount in stock");
    Console.WriteLine("enter the amount of product in stock");
    inStock = int.Parse(Console.ReadLine());
    Product product = new Product();
    product.ID = id;
    product.Name = name;
    product.Price = price;
    product.InStock = inStock;
    product.Category = category;
    DalProduct.Update(product);
}

void deleteProduct()
{
    int id;
    Console.WriteLine("enter id of the product you want to watch");
    id = int.Parse(Console.ReadLine());
    DalProduct.Delete(id);
}


void products()
{
    Console.WriteLine("1. add new product. 2. view product. 3. view product list. 4. update product. 5. delete product.");
    int choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            addProduct();
            break;
        case 2:
            viewProduct();
            break;
        case 3:
            viewProductList();
            break;
        case 4:
            updateProduct();
            break;
        case 5:
            deleteProduct();
            break;

    }
}
// ============finish products functions============

// ============MAIN PROGRAM============

void main()
{
    int choice;
    do
    {
        Console.WriteLine("enter the entity number: 1. orders 2. products 3. order-items 0. to exit");
        choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case 0:
                return;
            case 1:
                orders();
                break;
            case 2:
                products();
                break;
            case 3:
                orderItems();
                break;
            default:
                Console.WriteLine("wrong chice");
                break;

        }
    } while (choice != 0);
}

main();




