using Simulator;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        BLApi.IBL bl;

        BackgroundWorker worker;
        //====== disable the option of closing the window =======
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        //=====================================================
        private Stopwatch stopWatch;
        private bool isTimerRun;
        //BackgroundWorker worker;
        Duration duration;
        DoubleAnimation doubleanimation;
        ProgressBar ProgressBar;
        Tuple<BO.Order, int> dcT;

        public SimulatorWindow(BLApi.IBL Bl)
        {
            InitializeComponent();
            bl = Bl;
            Loaded += ToolWindow_Loaded;
            // workerStart();
            TimerStart();
            //worker.RunWorkerAsync();

            ProgressBarStart();
        }
        void ProgressBarStart()
        {
            ProgressBar = new ProgressBar();
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Orientation = Orientation.Horizontal;
            ProgressBar.Width = 500;
            ProgressBar.Height = 30;
            duration = new Duration(TimeSpan.FromSeconds(20));
            doubleanimation = new DoubleAnimation(200.0, duration);
            ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            SBar.Items.Add(ProgressBar);
        }

        void TimerStart()
        {
            stopWatch = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += TimerDoWork;
            worker.ProgressChanged += TimerProgressChanged;
            worker.WorkerReportsProgress = true;
            //Simulator.Simulator.StartSimulator();
            stopWatch.Restart();
            isTimerRun = true;
            worker.RunWorkerAsync();
        }
        //void workerStart()
        //{
        //    worker = new BackgroundWorker();
        //    worker.DoWork += WorkerDoWork;
        //    worker.WorkerReportsProgress = true;
        //    worker.WorkerSupportsCancellation = true;
        //    // worker.ProgressChanged += workerProgressChanged;
        //    worker.RunWorkerCompleted += RunWorkerCompleted;
        //    worker.RunWorkerAsync();
        //}
        void TimerDoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.ProgressChange += changeOrder;
            Simulator.Simulator.StopSimulator += Stop;
            Simulator.Simulator.run();
            while (isTimerRun)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        private void changeOrder(object sender, EventArgs e)
        {
            if (!(e is Details))
                return;
            Details details = e as Details;
            dcT = new Tuple<BO.Order, int>(details.order, details.seconds / 1000);
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(changeOrder, sender, e);
            }
            else
            {
                DataContext = dcT;
            }
        }


        //void WorkerDoWork(object sender, DoWorkEventArgs e)
        //{
        //    while (!worker.CancellationPending)
        //    {
        //        worker.ReportProgress(1);
        //        Thread.Sleep(1000);
        //    }
        //}

        void TimerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            SimulatorTXTB.Text = timerText;
        }
        //void workerProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    ProgressBar.Value = e.ProgressPercentage;
        //}
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void StopSimulatorBTN_Click(object sender, RoutedEventArgs e)
        {
            //if (worker.WorkerSupportsCancellation == true)
            //    // Cancel the asynchronous operation.
            //    worker.CancelAsync();
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
            Simulator.Simulator.DoStop();
            this.Close();
        }

        //    void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //    {
        //        Simulator.Simulator.StopSimulator();
        //        this.Close();
        //    }
        //}
        public void Stop(object sender, EventArgs e)
        {
            Simulator.Simulator.ProgressChange -= changeOrder;
            Simulator.Simulator.StopSimulator -= Stop;
            while (!CheckAccess())
            {
                Dispatcher.BeginInvoke(changeOrder, sender, e);
            }
            Close();

        }
    }
}