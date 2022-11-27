using BlImplementation;

Bl bl = new Bl();
BO.Cart cart = new BO.Cart();

//=============orders==================



void getOrders()
{
    IEnumerable<BO.OrderForList> orderList = bl.order.GetOrderList();
    foreach (var item in orderList)
    {
        Console.WriteLine(item);
    }
}

void getOrderItems()
{
    Console.WriteLine("enter order id");
    int id = int.Parse(Console.ReadLine());
    BO.Order orderItems = bl.order.GetOrderDetails(id);
    Console.WriteLine(orderItems);
}

void updateOrderShipping()
{
    Console.WriteLine("enter order id");
    int id = int.Parse(Console.ReadLine());
    bl.order.UpdateOrderShipping(id);
}

void updateOrderDelivery()
{

    Console.WriteLine("enter order id");
    int id = int.Parse(Console.ReadLine());
    bl.order.UpdateOrderDelivery(id);
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
        default:

            break;
    }
}


//=============products=================

void getProducts()
{
    IEnumerable<BO.ProductForList> products = bl.product.GetProductList();
    foreach (var item in products)
    {
        Console.WriteLine(item);
    }
}

void getCatalog()
{
    IEnumerable<BO.ProductItem> catalog = bl.product.GetCatalog();
    foreach (var item in catalog)
    {
        Console.WriteLine(item);
    }
}

void getProManager()
{
    Console.WriteLine("enter product id");
    int id = int.Parse(Console.ReadLine());
    BO.Product pro = bl.product.GetProductManager(id);
    Console.WriteLine(pro);
}
void getProCustomer()
{
    Console.WriteLine("enter product id");
    int id = int.Parse(Console.ReadLine());
    BO.Product pro = bl.product.GetProductCustomer(id);
    Console.WriteLine(pro);
}

void addPro()
{
    BO.Product pro = new BO.Product();
    Console.WriteLine("enter name, price, amount in stock");
    pro.Name = Console.ReadLine();
    pro.Price = int.Parse(Console.ReadLine());
    pro.inStock = int.Parse(Console.ReadLine());
    Console.WriteLine("enter the Product's category: 1.Drones,2.Cameras, 3.Headphones, 4.Computers, 5.SmartWatches");
    int choice = Convert.ToInt32(Console.ReadLine());
    pro.Category = (BO.Category)choice;
    bl.product.AddProduct(pro);
}

void deletePro()
{
    Console.WriteLine("enter product id");
    int id = int.Parse( Console.ReadLine());
    bl.product.DeleteProduct(id);
}

void updatePro()
{
    BO.Product pro = new BO.Product();
    Console.WriteLine("enter id, name, price, amount in stock");
    pro.ID = int.Parse(Console.ReadLine());
    pro.Name = Console.ReadLine();
    pro.Price = int.Parse(Console.ReadLine());
    pro.inStock = int.Parse(Console.ReadLine());
    Console.WriteLine("enter the Product's category: 1.Drones,2.Cameras, 3.Headphones, 4.Computers, 5.SmartWatches");
    int choice = Convert.ToInt32(Console.ReadLine());
    pro.Category = (BO.Category)choice;
    bl.product.Update(pro);
}


void products()
{
    int choice;
    Console.WriteLine("enter the choice: 1.get products list 2. get catalog. 3.get product for manager 7.get product for customer 4.add product 5.delete product 6.update product");
    choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            getProducts();
            break;
        case 2:
            getCatalog();
            break;
        case 3:
            getProManager();
            break;
        case 7:
            getProCustomer();
            break;
        case 4:
            addPro();
            break;
        case 5:
            deletePro();
            break;
        case 6:
            updatePro();
            break;
        default:
            Console.WriteLine("wrong choice");
            break;
    }
}

//=============carts===================

void addProduct()
{
    Console.WriteLine("enter product id");
    int productId;
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new BO.InvalidIntegerException();
    cart = bl.cart.AddProductToCart(cart, productId);
}


void updateProductAmount()
{
    int productId, newAmount;
    Console.WriteLine("enter product id");
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new BO.InvalidIntegerException();
    Console.WriteLine("enter new amount for the product");
    if (!(int.TryParse(Console.ReadLine(), out newAmount)))
        throw new BO.InvalidIntegerException();
    cart = bl.cart.Update(cart, productId, newAmount);
}


void confirmCart()
{

    Console.WriteLine("enter the customer's name");
    string CustomerName = Console.ReadLine();
    Console.WriteLine("enter the customer's email");
    string CustomerEmail = Console.ReadLine();
    Console.WriteLine("enter the customer's address");
    string CustomerAddress = Console.ReadLine();

    bl.cart.CartConfirmation(cart, CustomerName, CustomerEmail, CustomerAddress);
}


void carts()
{
    int choice;
    Console.WriteLine("enter the choice: 1.add product 2.update product amount. 3.confirm cart ");
    choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            addProduct();
            break;
        case 2:
            updateProductAmount();
            break;
        case 3:
            confirmCart();
            break;
        default:
            Console.WriteLine("wrong choice");
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
                    carts();
                    break;
                default:
                    Console.WriteLine("wrong choice");
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