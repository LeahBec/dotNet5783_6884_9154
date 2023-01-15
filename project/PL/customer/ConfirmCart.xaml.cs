using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ConfirmCart.xaml
    /// </summary>
    public partial class ConfirmCart : Window
    {

        BLApi.IBL bl;
        BO.Cart cart;
        public ConfirmCart(BLApi.IBL _bl, BO.Cart c)
        {
            InitializeComponent();
            this.bl = _bl;
            this.cart = c;
            DataContext = this.cart;
        }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.cart.CustomerEmail == "" || this.cart.CustomerAddress == "" || this.cart.CustomerName == "")
                {
                    throw new PlInvalidValueExeption("one or more of the customer details is missing");
                }
                BO.Order o = new BO.Order();
                o.CustomerAddress = this.cart.CustomerAddress;
                o.CustomerEmail= this.cart.CustomerEmail;
                o.CustomerName = this.cart.CustomerName;
                o.Items = this.cart.items;
                o.OrderDate = DateTime.Today;
                o.ShipDate=DateTime.MinValue;
                o.DeiveryDate= DateTime.MinValue;
                o.TotalPrice= this.cart.TotalPrice;
                bl.order.AddNewOrder(o);
                MessageBox.Show("The order was successfully created");
                Window w = new OrderTracking(bl, o.ID, this.cart);
                w.Show();
                this.Close();
            }
            catch(PlInvalidValueExeption ex){
                MessageBox.Show(ex.Message);
            }
        }
    }
}
