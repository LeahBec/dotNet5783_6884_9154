
using Dal.DO;
using DalFacade.DO;
namespace DalList;


static public class DataSource
{
    const int V = 500000;
    private static void s_Initialize()
    {
        CreateProductList();
        CreateOrderList(V);
        CreateOrderItemList();
    }

    static DataSource() { s_Initialize(); }
    static Random rand = new Random();

    public static readonly int random;

    public static Product[] products = new Product[50];
    public static Order[] orders = new Order[100];
    public static OrderItem[] orderItems = new OrderItem[200];
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
            Config.ProductIndex++;
            products[i] = new Product();
            int x = (int)rand.NextInt64(0, 5);
            string[] productsNames = Enum.GetValues(typeof(eProductNames))
                .Cast<int>()
                .Select(x => x.ToString())
                .ToArray();
            int IdxName = (int)rand.NextInt64(Enum.GetNames(typeof(eProductNames)).Length);
            int IdxPrice = (int)rand.NextInt64(10, 100);
            products[i].Name = productsNamesCategories[x].Item2;
            products[i].Price = IdxPrice;
            products[i].ID = uniqueID++;
            products[i].InStock = (int)rand.NextInt64(10, 5000);
            products[i].Category = productsNamesCategories[x].Item1;
        }
    }

    public static void CreateOrderList(int uniqueID)
    {
        string[] CustomerName = { "aaa", "bbb", "ccc" };
        string[] CustomerAdress = { "ddd", "eee", "fff" };
        string[] CustomerEmail = { "ggg", "hhh", "iii" };
        for (int i = 0; i < 20; i++)
        {
            Random rand = new Random();

            orders[i] = new Order();
            orders[i].OrderID = uniqueID++;
            int indexName = (int)rand.NextInt64(CustomerName.Length);
            int indexAdress = (int)rand.NextInt64(CustomerAdress.Length);
            int indexEmail = (int)rand.NextInt64(CustomerEmail.Length);
            orders[i].CustomerName = CustomerName[indexName];
            orders[i].CustomerAdress = CustomerAdress[indexAdress];
            orders[i].CustomerEmail = CustomerEmail[indexEmail];
            orders[i].OrderDate = DateTime.Today;
            TimeSpan shipSpan = TimeSpan.FromDays((int)rand.NextInt64(0, 10));
            TimeSpan deliverySpan = TimeSpan.FromDays((int)rand.NextInt64(10, 25));
            orders[i].ShipDate = orders[i].OrderDate + shipSpan;
            orders[i].DeliveryDate = orders[i].ShipDate + deliverySpan;
            Config.OrderIndex++;
        }
    }
    /* public static void CreateOrderItemList()
     {
         int[] productPrices = { 2000, 1300, 300, 7000, 450 };
         int uniqueID = 100000;
         for (int i = 0; i < 40; i++, Config.OrderItemIndex++)
         {
             int numOfProducts = (int)rand.NextInt64(1, 4);
             for (int j = 0; j < numOfProducts; j++)
             {

                 int ProductIndex = (int)rand.NextInt64(products.Length);
                 if (products[ProductIndex].InStock == 0)
                     continue;
                 orderItems[i] = new OrderItem();
                 int productId = (int)rand.NextInt64(products.Length);
                 orderItems[i].ID = uniqueID++;
                 orderItems[i].OrderID = (int)rand.NextInt64(orders.Length);
                 orderItems[i].ProductID = productId;
                 orderItems[i].Price = productPrices[productId];
                 orderItems[i].Amount = (int)rand.NextInt64(1, Math.Min(products[productId].InStock, 4));
             }
         }
     }*/

    static private void CreateOrderItemList()
    {
        int uniqueID = 100000;

        for (int i = 0; i < 40;)
        {
            int OrderIndex = (int)rand.NextInt64(Config.OrderIndex);
            int numOfProducts = (int)rand.NextInt64(1, 4);
            for (int j = 0; j < numOfProducts; j++)
            {
                int ProductIndex = (int)rand.NextInt64(Config.ProductIndex);
                if (products[ProductIndex].InStock == 0)
                    continue;
                orderItems[Config.OrderItemIndex] = new OrderItem();
                orderItems[Config.OrderItemIndex].ID = uniqueID++;
                orderItems[Config.OrderItemIndex].ProductID = products[ProductIndex].ID;
                orderItems[Config.OrderItemIndex].OrderID = orders[OrderIndex].OrderID;
                orderItems[Config.OrderItemIndex].Amount = (int)rand.NextInt64(1, products[ProductIndex].InStock);
                products[ProductIndex].InStock -= orderItems[i].Amount;
                orderItems[Config.OrderItemIndex].Price = orderItems[i].Amount * products[ProductIndex].Price;
                Config.OrderItemIndex++;
                i++;
            }
        }
    }





    public class Config
    {
        static public int ProductIndex = 0;
        static public int OrderIndex = 0;
        static public int OrderItemIndex = 0;
    }


}

