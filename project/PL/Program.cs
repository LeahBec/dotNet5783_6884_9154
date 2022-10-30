// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

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

