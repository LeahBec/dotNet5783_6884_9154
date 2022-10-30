// See https://aka.ms/new-console-template for more information
using DalList;
using Dal.Do;


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
    TimeSpan shipSpan = TimeSpan.FromDays(10);
    TimeSpan deliverySpan = TimeSpan.FromDays(25);
    _sDate = _oDate+shipSpan;
    _dDate = _sDate+deliverySpan;
    _id = DataSource.Config.OrderIndex++;
    Order newOrder = new Order(_id,_name,_email,_address,_oDate,_sDate,_dDate);
    Order.create(newOrder);

}

Order viewOrder()
{
    Console.WriteLine("enter order id");
    int id = Console.readLine();
    Order
}

void orders() {
    Console.WriteLine("1. add new order. 2. view order. 3. view orders list. 4. update order. 5. delete order.");
    int choice = Console.ReadLine();
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
        default:
            Console.WriteLine("wrong choice");

    }

}

void orderItems()
{
    


}

//============Product CRUD==============
void addProduct()
{
    string name;
    int id = DataSource.Config.++
    eCategory category;
    float price;
    int inStock;
    Console.WriteLine("enter name for the new product");
    Console.ReadLine(name);
    Console.WriteLine("enter the product's category: 1 - Drones, 2 - Cameras, 3 - Headphones, 4 -  Computers, 5 - SmartWatches");
    int choice;
    Console.ReadLine(choice);
    category = (eCategory)choice;
    Console.WriteLine("enter price for the new product");
    Console.ReadLine(price);
    Console.WriteLine("enter amount in stock");
    Product newProduct = new Product(0, name, category, price, inStock);
}
products viewProduct()
{
    int id;
    Console.WriteLine("enter id of product you want to watch");
    Console.ReadLine(id);

}


//======================================
void products() {
        Console.WriteLine("1. add new product. 2. view product. 3. view product list. 4. update product. 5. delete product.");
    int choice = Console.ReadLine();
    switch (choice)    {
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
        default:
            Console.WriteLine("wrong choice");

    }
}

/*void main()
{*/
int choice;
Console.WriteLine("enter the entity number: 1. orders 2. products 3. order-items 0. to exit");
choice = Convert.ToInt32(Console.ReadLine());
switch (choice)
{
    case 1:
        orders();
        break;
    case 2:
        products();
        break;
    case 3:
        orderItems();
        break;
}
/*}*/

