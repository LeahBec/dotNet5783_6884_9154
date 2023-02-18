
using DalApi;
using DalFacade.DO;
using System.Xml.Linq;

namespace Dal;

sealed public class DalXml : IDal
{
    static private Lazy<DalXml>? instance = null;
    public static IDal Instance { get => GetInstance(); }

    public IProduct Product { get; } = new Dal.DalProduct();
    public IOrder Order { get; } = new Dal.DalOrder();
    public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();

    private DalXml() { }
    public static DalXml GetInstance()
    {
        lock (instance ??= new Lazy<DalXml>(() => new DalXml()))
        {


            return instance.Value;
        }

    }
    #region Will be deleted



   /* public DalXml()
    {
        CreateProductList();

    }

    static Random rand = new Random();

    public static readonly int random;

    public static List<DO.Product> products = new();
    public static List<DO.Order> orders = new();
    public static List<DO.OrderItem> orderItems = new();

    /// <summary>
    /// the function create the product list and initilizes it with 10 products
    /// </summary>
    public static void CreateProductList()
    {


        
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
            DO.Product p = new DO.Product();
            p.Name = productsNamesCategories[x].Item2;
            p.Price = IdxPrice;
            p.InStock = (int)rand.NextInt64(10, 30);
            p.Category = productsNamesCategories[x].Item1;
            XElement? rootConfig = XDocument.Load(@"..\..\..\..\xml\config.xml").Root;
            XElement? id = rootConfig?.Element("productId");
            int pId = Convert.ToInt32(id?.Value);
            p.ID = pId;
            pId++;
            id.Value = pId.ToString();
            rootConfig?.Save("../../../../xml/config.xml");
            products.Add(p);
        }
    }
    /// <summary>
    /// the function create the orders list and initilizes it with 20 orders
    /// </summary>
    public static void CreateOrderList()
    {
        string[] CustomerName = { "aaa", "bbb", "ccc" };
        string[] CustomerAddress = { "ddd", "eee", "fff" };
        string[] CustomerEmail = { "ggg", "hhh", "iii" };
        for (int i = 0; i < 20; i++)
        {
            Random rand = new Random();
            //orders[i] = new Order();
            DO.Order o = new();
            int indexName = (int)rand.NextInt64(CustomerName.Length);
            int indexAdress = (int)rand.NextInt64(CustomerAddress.Length);
            int indexEmail = (int)rand.NextInt64(CustomerEmail.Length);
            o.CustomerName = CustomerName[indexName];
            o.CustomerAddress = CustomerAddress[indexAdress];
            o.CustomerEmail = CustomerEmail[indexEmail];
            o.OrderDate = DateTime?.Today;
            TimeSpan shipSpan = TimeSpan.FromDays((int)rand.NextInt64(0, 10));
            TimeSpan deliverySpan = TimeSpan.FromDays((int)rand.NextInt64(10, 25));
            //orders[i].ShipDate = orders[i].OrderDate + shipSpan;
            //orders[i].DeliveryDate = orders[i].ShipDate + deliverySpan;
            o.OrderDate = DateTime.Now;
            if (i % 10 < 8)  // 80% have ship date
                o.ShipDate = o.OrderDate + shipSpan;
            else
                o.ShipDate = DateTime?.MinValue;
            if (i % 10 < 6)
            { // 60% from them have delivery date
                if (o.ShipDate == DateTime?.MinValue)
                    o.ShipDate = o.OrderDate + shipSpan;
                o.DeliveryDate = o.ShipDate + deliverySpan;
            }
            else
                o.DeliveryDate = DateTime?.MinValue;
            XElement? rootConfig = XDocument.Load(@"..\..\..\..\xml\config.xml").Root;
            XElement? id = rootConfig?.Element("orderId");
            int oId = Convert.ToInt32(id?.Value);
            o.OrderID = oId;
            oId++;
            id.Value = oId.ToString();
            rootConfig?.Save("../../../../xml/config.xml");
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
            DO.OrderItem oi = new();
            //int OrderIndex = (int)rand.NextInt64(Config.OrderIndex);
            int numOfProducts = (int)rand.NextInt64(1, 4);
            for (int j = 0; j < numOfProducts; j++)
            {
                int ProductIndex = (int)rand.NextInt64(products.Count());
                int OrderIndex = (int)rand.NextInt64(orders.Count());
                if (products[ProductIndex].InStock == 0) //limits the amount of products to the amount in stock
                    continue;
                DO.Product p = new();
                p = products[ProductIndex];
                oi.ProductID = products[ProductIndex].ID;
                oi.OrderID = orders[OrderIndex].OrderID;
                oi.Amount = (int)rand.NextInt64(1, products[ProductIndex].InStock);
                //p.InStock -= oi.Amount;
                oi.Price = oi.Amount * products[ProductIndex].Price;
                //products[ProductIndex] = p;
                XElement? rootConfig = XDocument.Load(@"..\..\..\..\xml\config.xml").Root;
                XElement? id = rootConfig?.Element("orderItemId");
                int oiId = Convert.ToInt32(id?.Value);
                oi.ID = oiId;
                oiId++;
                id.Value = oiId.ToString();
                rootConfig?.Save("../../../../xml/config.xml");
                orderItems.Add(oi);
                i++;
            }
        }
    }



*/

    /*static public class Config
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
    }*/

    


    #endregion
}
