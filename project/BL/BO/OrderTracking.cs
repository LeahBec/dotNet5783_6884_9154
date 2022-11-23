namespace BO;
public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }
    public List<(DateTime, OrderStatus)> dateAndTrack { get; set; }
    public override string ToString()
    {
        string orderTracking = $@"
        Order id: {ID}
        Status : {Status}
        Order Tracking : {dateAndTrack}";
        return orderTracking;
    }
}

