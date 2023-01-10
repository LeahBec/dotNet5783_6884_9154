using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.customer
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        int orderID;
        BO.Order o;
        BLApi.IBL bl;
        public OrderTracking(BLApi.IBL _bl ,int _orderID)
        {
            this.bl = _bl;
            this.orderID = _orderID;
            this.o = bl.order.GetOrderDetails(this.orderID);
            InitializeComponent();
            DataContext= this.o;
        }

        private void show_order_details(object sender, RoutedEventArgs e)
        {
            Window w = new PL.OrderWindow(this.bl, this.o, true);
            w.Show();
            this.Close();
        }
    }
}
