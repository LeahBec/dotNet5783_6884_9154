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

        }
        public static void run()
        {

        }

        private static void setTextInvok_opt1(string obj)
        {

        }
    }
}