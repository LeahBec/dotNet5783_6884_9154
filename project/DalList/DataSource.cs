
using Dal.DO;
using DalFacade.DO;
namespace DalList;

/// <summary>
/// the class contains all entities data,
/// and initializes all entities data list while the program is starting.
/// </summary>
public class DataSource
{
    /// <summary>
    /// the function is called at the program starting and calls all 3 entities - 
    /// initialize functions.
    /// </summary>
    private static void s_Initialize()
    {  //initializing the program
        CreateProductList();
        CreateOrderList();
        CreateOrderItemList();
    }

    /// <summary>
    /// default constructor
    /// </summary>
    static DataSource() { s_Initialize(); }
    static Random rand = new Random();

    public static readonly int random;

    public static List<Product> products = new List<Product>();
    public static List<Order> orders = new List<Order>();
    public static List<OrderItem> orderItems = new List<OrderItem>();

    /// <summary>
    /// the function create the product list and initilizes it with 10 products
    /// </summary>
    public static void CreateProductList()
    {
        int uniqueID = 100000;
        Tuple<eCategory, string>[] productsNamesCategories = {
            Tuple.Create( eCategory.Drones, "mini mavic"),
            Tuple.Create( eCategory.Computers, "ASUS_x470rv"),
            Tuple.Create( eCategory.Cameras, "canon_x740"),
            Tuple.Create( eCategory.Headphones, "JBL_flip4"),
            Tuple.Create( eCategory.SmartWatches, "RBS753"),
        };
        for (int i = 0; i < 10; i++)
        {
            //Config.ProductIndex++; //increasing the index in config
            //products[i] = new Product();
            int x = (int)rand.NextInt64(0, 5);  //index for the random product name
            //int IdxName = (int)rand.NextInt64(Enum.GetNames(typeof(eProductNames)).Length);
            int IdxPrice = (int)rand.NextInt64(10, 100);
            Product p = new Product();
            p.Name = productsNamesCategories[x].Item2;
            p.Price = IdxPrice;
            p.ID = uniqueID++;
            p.InStock = (int)rand.NextInt64(10, 30);
            p.Category = productsNamesCategories[x].Item1;
            products.Add(p);
        }
    }
    /// <summary>
    /// the function create the orders list and initilizes it with 20 orders
    /// </summary>
    public static void CreateOrderList()
    {
        string[] CustomerName = { "aaa", "bbb", "ccc" };
        string[] CustomerAdress = { "ddd", "eee", "fff" };
        string[] CustomerEmail = { "ggg", "hhh", "iii" };
        for (int i = 0; i < 20; i++)
        {
            Random rand = new Random();
            //orders[i] = new Order();
            Order o = new Order();
            int indexName = (int)rand.NextInt64(CustomerName.Length);
            int indexAdress = (int)rand.NextInt64(CustomerAdress.Length);
            int indexEmail = (int)rand.NextInt64(CustomerEmail.Length);
            o.OrderID = Config.OrderId;
            o.CustomerName = CustomerName[indexName];
            o.CustomerAdress = CustomerAdress[indexAdress];
            o.CustomerEmail = CustomerEmail[indexEmail];
            o.OrderDate = DateTime.Today;
            TimeSpan shipSpan = TimeSpan.FromDays((int)rand.NextInt64(0, 10));
            TimeSpan deliverySpan = TimeSpan.FromDays((int)rand.NextInt64(10, 25));
            //orders[i].ShipDate = orders[i].OrderDate + shipSpan;
            //orders[i].DeliveryDate = orders[i].ShipDate + deliverySpan;
            o.OrderDate = DateTime.Now;
            if (i % 10 < 8)  // 80% have ship date
                o.ShipDate = o.OrderDate + shipSpan;
            else
                o.ShipDate = DateTime.MinValue;
            if (i % 10 < 6)
            { // 60% from them have delivery date
                if (o.ShipDate == DateTime.MinValue)
                    o.ShipDate = o.OrderDate + shipSpan;
                o.DeliveryDate = o.ShipDate + deliverySpan;
            }
            else
                o.DeliveryDate = DateTime.MinValue;
            orders.Add(o);
        }
    }

    /// <summary>
    /// the function orderItems the product list and initilizes it with 40 orderItems
    /// </summary>
    static private void CreateOrderItemList()
    {
        for (int i = 0; i < 40;)
        {
            OrderItem oi = new OrderItem();
            //int OrderIndex = (int)rand.NextInt64(Config.OrderIndex);
            int numOfProducts = (int)rand.NextInt64(1, 4);
            for (int j = 0; j < numOfProducts; j++)
            {
                int ProductIndex = (int)rand.NextInt64(products.Count());
                int OrderIndex = (int)rand.NextInt64(orders.Count());
                if (products[ProductIndex].InStock == 0) //limits the amount of products to the amount in stock
                    continue;
                Product p = new Product();
                p = products[ProductIndex];
                oi.ID = Config.OrderItemId;
                oi.ProductID = products[ProductIndex].ID;
                oi.OrderID = orders[OrderIndex].OrderID;
                oi.Amount = (int)rand.NextInt64(1, products[ProductIndex].InStock);
                p.InStock -= oi.Amount;
                oi.Price = oi.Amount * products[ProductIndex].Price;
                products[ProductIndex] = p;
                orderItems.Add(oi);
                i++;
            }
        }
    }





    static public class Config
    {
        //static public int ProductIndex = 0;
        //static public int OrderIndex = 0;
        //static public int OrderItemIndex = 0;
        static private int orderId = 500000;
        static private int orderItemId = 100000;
        static public int OrderId
        {
            get { return orderId++; }
        }
        static public int OrderItemId
        {
            get { return orderItemId++; }
        }
    }


}

