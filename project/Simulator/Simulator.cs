/*using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace Simulator
{
    public static class Simulator
    {
        private static Thread thread;

        static Simulator()
        {
            
            thread = new Thread(run);
            thread.Start();
            BackgroundWorker back=new();
            back.RunWorkerAsync();

        }
        public static void run()
        {

        }

        private static void setTextInvok_opt1(string obj)
        {

        }
    }
}*/


using BLApi;
using System.Diagnostics;
namespace Simulator;
public static class Simulator
{


    static BO.Order order;
    static Thread myThread { get; set; }
    static Stopwatch myStopWatch { get; set; }
    static event EventHandler propsChanged;
    static string? previousState;
    static string? afterState;
    static bool finishFlag = false;

    public static event EventHandler Stop;
    public static void StartSimulator()
    {
        myThread = new Thread(Simulation);
        myThread.Start();
    }
    public static void run()
    {
        Thread mainThreads = new Thread(new ThreadStart(chooseOrder));
        return;
    }


    private static void Simulation()
    {

    }
    public static void StopSimulator()
    {
        //myThread.Join();
        finishFlag = true;
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
                ; //call DoStop(), program has to finish, no more orders.
            else
            {
                previousState = bl.order.GetOrderDetails((int)id).Status.ToString();
                Random rand = new Random();
                int num = rand.Next(1000, 5000);
                Thread.Sleep(num);
                afterState = (previousState == "Payed" ? bl.order.UpdateOrderShipping((int)id) : bl.order.UpdateOrderDelivery((int)id)).Status.ToString();
            }
        }
        return;
    }
}
