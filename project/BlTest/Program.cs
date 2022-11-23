using BlImplementation;

Bl bl = new Bl();

//=============orders==================

void getOrders()
{

}

void orders()
{
    int choice;
    Console.WriteLine("enter the choice: 1.get orders list 2. get order items. 3.update shipping 4.update delivery");
    choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            getOrders();
            break;
        case 2:
            getOrderItems();
            break;
        case 3:
            updateOrderShipping();
            break;
        case 4:
            updateOrderDelivery();
            break;
        default
            break;
    }
}

// ============MAIN PROGRAM============

void main()
{
    int choice;

    try
    {
        do
        {
            Console.WriteLine("enter the entity number: 1. order 2. product 3. cart  0. to exit");
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