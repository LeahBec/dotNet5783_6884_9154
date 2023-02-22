using BLApi;
using System.Diagnostics;
namespace Simulator;
public static class Simulator
{


    static BO.Order order;
    static Thread myThread { get; set; }
    static Stopwatch myStopWatch { get; set; }
    static event EventHandler propsChanged;
    private static string? previousState;
    private static string? afterState;
    static bool finishFlag = false;

    public static event EventHandler StopSimulator;
    public static event EventHandler ProgressChange;
    public static void DoStop()
    {
        finishFlag = true;
        StopSimulator("",EventArgs.Empty);
    }
    public static void run()
    {
        Thread mainThreads = new Thread(new ThreadStart(chooseOrder));
        mainThreads.Start();
        return;
    }
    public static void chooseOrder()
    {
        IBL bl = new BlImplementation.Bl();
        int? id;
        while (!finishFlag)
        {
            id = bl.order.ChooseOrder();
            if (id == null)
                DoStop(); //call DoStop(), program has to finish, no more orders.
            else
            {
                BO.Order o = bl.order.GetOrderDetails((int)id);
                previousState = o.Status.ToString();
                Random rand = new Random();
                int num = rand.Next(1000, 5000);
                Details details = new Details(o, num);
                if (ProgressChange != null)
                {
                    ProgressChange(null, details);
                }
                Thread.Sleep(num);
                afterState = (previousState == "Payed" ? bl.order.UpdateOrderShipping((int)id) : bl.order.UpdateOrderDelivery((int)id)).Status.ToString();
            }
        }
        return;
    }
}


public class Details : EventArgs
{
    public BO.Order order;
    public int seconds;
    public Details(BO.Order ord, int sec)
    {
        order = ord;
        seconds = sec;
    }
}