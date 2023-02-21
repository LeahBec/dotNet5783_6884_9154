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


using System.Diagnostics;
namespace Simulator;
public static class Simulator
{


    static BO.Order order;
    static Thread myThread { get; set; }
    static Stopwatch myStopWatch { get; set; }

    static event EventHandler stop;
    static event EventHandler propsChanged;
    public static void StartSimulator()
    {
        myThread = new Thread(Simulation);
        myThread.Start();
    }

    public static void StopSimulator()
    {
        //myThread.Join();

    }

    private static void Simulation()
    {

    }
}
