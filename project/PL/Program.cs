// See https://aka.ms/new-console-template for more information
using DalList;


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
    _id = DataSource.Config.OrderIndex;
    order = {
        int id: _id,
        string name: _name,
        string email: _email,
        string address: _address,


    }

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

void products() {

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

