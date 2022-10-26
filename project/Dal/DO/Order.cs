

namespace Dal.DO;

public struct Order:IDataObject { 
    public int OrderID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public override string ToString()
    {
        string order = $@"
        Order #{OrderID}:
        Customer Name: {CustomerName}, 
        Customer Adress : {CustomerAdress}
        Customer E-mail : {CustomerEmail}
    	Order Date: {OrderDate}
        Ship Date: {ShipDate}
        Delivery Date: {DeliveryDate}";
        return order;
    }
}

