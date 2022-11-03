﻿// See https://aka.ms/new-console-template for more information
using DalList;
using Dal.DO;
using DalFacade.DO;

const int IDX = 500000;


// =============order functions=============

/// <summary>
/// creates new order and adds to the orders array
/// </summary>
void addOrder()
{
    Random rand = new Random();
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
    TimeSpan shipSpan = TimeSpan.FromDays((int)rand.NextInt64(0, 10));
    TimeSpan deliverySpan = TimeSpan.FromDays((int)rand.NextInt64(10, 25));
    _sDate = _oDate + shipSpan;
    _dDate = _sDate + deliverySpan;
    _id = DataSource.Config.OrderIndex + IDX;
    DataSource.Config.OrderIndex++;
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

/// <summary>
/// prints to the screen a specific order according to the users will
/// </summary>
void viewOrder()
{
    Console.WriteLine("enter order id");
    int id = int.Parse(Console.ReadLine());
    Order order = new Order();
    order = DalOrder.ReadSingle(id);
    Console.WriteLine(order.OrderID + order.CustomerName + order.CustomerEmail + order.CustomerAdress + order.OrderDate + order.ShipDate + order.DeliveryDate);
}


/// <summary>
/// prints to the screen list of all the orders in the array
/// </summary>
void viewOrderList()
{
    Order[] orders = new Order[DataSource.Config.OrderIndex];
    orders = DalOrder.Read();
    int amountOfOrders = DataSource.Config.OrderIndex;
    if (amountOfOrders == 0) { Console.WriteLine("no orders were found"); return; }
    foreach (Order item in orders)
    {
        Console.WriteLine(item.OrderID + " " + item.CustomerName + " " + item.CustomerEmail + " " + item.CustomerAdress + " " + item.OrderDate + " " + item.ShipDate + " " + item.DeliveryDate);
    }
}


/// <summary>
/// updates a certin order, the user enters the details and the function goes to the array and inserts the updated order.
/// </summary>
void updateOrder()
{
    Random rand = new Random();
    Console.WriteLine("enter id of the order you want to update");
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
    TimeSpan shipSpan = TimeSpan.FromDays((int)rand.NextInt64(0, 10));
    TimeSpan deliverySpan = TimeSpan.FromDays((int)rand.NextInt64(10, 25));
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


/// <summary>
/// deletes a certin order
/// </summary>
void deleteOrder()
{

    int id;
    Console.WriteLine("enter id of the order you want to delete");
    id = int.Parse(Console.ReadLine());
    DalOrder.Delete(id);
}


/// <summary>
/// orders switch
/// </summary>
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
/// <summary>
/// creates new orderItem and adds to the orders array
/// </summary>
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
    orderItem.ID = id + 100000;
    orderItem.OrderID = orderId;
    orderItem.ProductID = productId;
    orderItem.Amount = amount;
    orderItem.Price = price;
    DalOrderItem.Create(orderItem);

}
/// <summary>
/// prints a certin orderItem
/// </summary>
void viewOrderItem()
{
    Console.WriteLine("enter order item id");
    int id = int.Parse(Console.ReadLine());
    OrderItem orderItem = new OrderItem();
    orderItem = DalOrderItem.ReadSingle(id);
    Console.WriteLine(orderItem.ID + " " + orderItem.OrderID + " " + orderItem.ProductID + " " + orderItem.Price + " " + orderItem.Amount);
}
/// <summary>
/// prints the list of orderItems
/// </summary>
void viewOrderListItem()
{
    OrderItem[] orderItems = new OrderItem[DataSource.Config.OrderItemIndex];
    orderItems = DalOrderItem.Read();
    foreach (OrderItem item in orderItems)
    {
        Console.WriteLine(item.ID + " " + item.OrderID + " " + item.ProductID + " " + item.Price + " " + item.Amount);

    }
}
/// <summary>
/// prints a list of all order items with the same certin order id
/// </summary>
OrderItem[] getOrderItemByOrderId(int id)
{
    int i = 0;
    OrderItem[] orderItems = new OrderItem[DataSource.Config.OrderItemIndex];
    foreach (OrderItem item in DataSource.orderItems)
    {
        if (item.OrderID == id)
        {
            i++;
            orderItems[i] = item;
        }
    }
    return orderItems;
}

void viewListOrderId()
{
    Console.WriteLine("enter order id");
    int id = int.Parse(Console.ReadLine());
    OrderItem[] orderItems = new OrderItem[DataSource.Config.OrderItemIndex];
    orderItems = getOrderItemByOrderId(id);
    foreach (OrderItem item in orderItems)
    {
        if (item.OrderID != 0)
            Console.WriteLine(item.ID + " " + item.OrderID + " " + item.ProductID + " " + item.Price + " " + item.Amount);
    }

}
/// <summary>
/// prints a list of all order items with the same product id
/// </summary>
OrderItem[] getOrderItemByProductId(int id)
{
    int i = 0;
    OrderItem[] orderItems = new OrderItem[DataSource.Config.OrderItemIndex];
    foreach (OrderItem item in DataSource.orderItems)
    {
        if (item.ProductID == id)
        {
            i++;
            orderItems[i] = item;
        }
    }
    return orderItems;
}

void viewListProductId()
{
    Console.WriteLine("enter product id");
    int id = int.Parse(Console.ReadLine());
    OrderItem[] orderItems = new OrderItem[DataSource.Config.OrderItemIndex];
    orderItems = getOrderItemByProductId(id);
    foreach (OrderItem item in orderItems)
    {
        if (item.OrderID != 0)
            Console.WriteLine(item.ID + " " + item.OrderID + " " + item.ProductID + " " + item.Price + " " + item.Amount);
    }
}
/// <summary>
/// updates a certin order item
/// </summary>
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
/// <summary>
/// delete a certin order item from the array
/// </summary>
void deleteOrderItem()
{
    int id;
    Console.WriteLine("enter id of the order item you want to delete");
    id = int.Parse(Console.ReadLine());
    DalOrderItem.Delete(id);
}

/// <summary>
/// order item switch
/// </summary>
void orderItems()
{
    Console.WriteLine("1. add new order item. 2. view order item. 3. view orders items list. 4. view order items by order id 5. view order items by product id 6. update order item. 7. delete order item.");
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
            viewListOrderId();
            break;
        case 5:
            viewListProductId();
            break;
        case 6:
            updateOrderItem();
            break;
        case 7:
            deleteOrderItem();
            break;


    }
}

// ============finish orderItems functions============


// ============product help functions============
/// <summary>
/// creates a new product and adds it to the array
/// </summary>
Product addProduct()
{
    int f = DataSource.Config.ProductIndex;
    string name;
    eCategory category;
    float price;
    int inStock;
    Console.WriteLine("enter name for the new product");
    name = Console.ReadLine();
    Console.WriteLine("enter the product's category: 1 - Drones, 2 - Cameras, 3 - Headphones, 4 -  Computers, 5 - SmartWatches");
    int choice = int.Parse(Console.ReadLine());
    category = (eCategory)choice;
    Console.WriteLine("enter price for the new product");
    price = Single.Parse(Console.ReadLine());
    Console.WriteLine("enter the amout of product in stock");
    inStock = int.Parse(Console.ReadLine());
    int id = DataSource.Config.ProductIndex + 100000;
    Product product = new Product();
    product.ID = id;
    product.Name = name;
    product.Price = price;
    product.InStock = inStock;
    product.Category = category;
    DalProduct.Create(product);
    return product;
}

/// <summary>
/// prints a certin product
/// </summary>
void viewProduct()
{
    int id;
    Console.WriteLine("enter id of the product you want to watch");
    id = int.Parse(Console.ReadLine());
    Product product = new Product();
    product = DalProduct.ReadSingle(id);
    Console.WriteLine(product.ID + " " + product.Name + " " + product.Price + " " + product.InStock + " " + product.Category);

}
/// <summary>
/// prints the product array
/// </summary>
void viewProductList()
{
    Product[] products = new Product[DataSource.Config.ProductIndex];
    products = DalProduct.Read();
    foreach (Product item in products)
    {
        Console.WriteLine(item.ID + " " + item.Name + " " + item.Price + " " + item.InStock + " " + item.Category);

    }
}

/// <summary>
/// updates a certin product
/// </summary>
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

/// <summary>
/// products switch
/// </summary>
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
    try
    {
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
    catch (Exception err)
    {
        Console.WriteLine(err);
    }
}

main();




