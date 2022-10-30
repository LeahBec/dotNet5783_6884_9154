
using Dal.DO;
using Dal;
using DalFacade.DO;
namespace DalList;


static internal class DataSource
{


    static DataSource()
    {
        s_Initialize();
    }
    static Random rand = new Random();

    public static readonly int random;

    internal static Product[] products = new Product[50];
    internal static Order[] orders = new Order[100];
    internal static OrderItem[] orderItems = new OrderItem[200];

    public static void CreateProductList()
    {
        int uniqueID = 100000;
        for (int i = 0; i < 10; i++)
        {
            products[i] = new Product();
            string[] productsNames = Enum.GetValues(typeof(eProductNames))
                .Cast<int>()
                .Select(x => x.ToString())
                .ToArray();
            int IdxName = (int)rand.NextInt64(Enum.GetNames(typeof(eProductNames)).Length);
            int IdxPrice = (int)rand.NextInt64(10, 100);
            products[i].Name = productsNames[IdxName];
            products[i].Price = IdxPrice;
            products[i].ID = uniqueID++;
            products[i].InStock = (int)rand.NextInt64(10, 5000);
            int x = 1;
            products[i].Category = (eCategory)x;
        }
    }
    public static void CreateOrderList()
    {
        string[] CustomerName = { "aaa", "bbb", "ccc" };
        string[] CustomerAdress = { "ddd", "eee", "fff" };
        string[] CustomerEmail = { "ggg", "hhh", "iii" };
        int uniqueID = 500000;
        for (int i = 0; i < 20; i++)
        {
            orders[i] = new Order();
            int indexName = (int)rand.NextInt64(CustomerName.Length);
            int indexAdress = (int)rand.NextInt64(CustomerAdress.Length);
            int indexEmail = (int)rand.NextInt64(CustomerEmail.Length);
            orders[i].CustomerName = CustomerName[indexName];
            orders[i].CustomerAdress = CustomerAdress[indexAdress];
            orders[i].CustomerEmail = CustomerEmail[indexEmail];
            orders[i].OrderDate = DateTime.MinValue;
            TimeSpan shipSpan = TimeSpan.FromDays(10);
            TimeSpan deliverySpan = TimeSpan.FromDays(25);
            orders[i].ShipDate = orders[i].OrderDate + shipSpan;
            orders[i].DeliveryDate = orders[i].ShipDate+deliverySpan;
        }
    }
    public static void CreateOrderItemList()
    {
        int[] productPrices = { 2000, 1300, 300, 7000, 450 };
        int uniqueID = 100000;
        for (int i = 0; i < 40; i++)
        {
            orderItems[i] = new OrderItem();
            int productId = (int)rand.NextInt64(products.Length);
            orderItems[i].ID = uniqueID++;
            orderItems[i].OrderID = (int)rand.NextInt64(orders.Length);
            orderItems[i].ProductID = productId;
            orderItems[i].Price = productPrices[productId];
            orderItems[i].Amount = (int)rand.NextInt64(1, Math.Min(products[productId].InStock, 4));

        }
    }
    private static void s_Initialize()
    {
        CreateProductList();
        CreateOrderList();
        CreateOrderItemList();
    }
    internal class Config
    {
        static internal int ProductIndex = 10;
        static internal int OrderIndex = 20;
        static internal int OrderItemIndex = 40;
    }

    
}

