using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        PO.Cart c;
        Window prevWindow;
        public ConfirmCart(BLApi.IBL _bl, PO.Cart _c, Window _prevWindow)
        {
            InitializeComponent();
            this.bl = _bl;
            this.c = _c;
            this.prevWindow = _prevWindow;
            DataContext = this.c;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*if (this.c.CustomerEmail == "" || this.c.CustomerAddress == "" || this.c.CustomerName == "")
                {
                    throw new PlInvalidValueExeption("one or more of the customer details is missing");
                }
                BO.Order o = new BO.Order();
                o.CustomerAddress = this.c.CustomerAddress;
                o.CustomerEmail= this.c.CustomerEmail;
                o.CustomerName = this.c.CustomerName;
                o.Items = Common.convertToBoOiList(this.c.Items);
                o.OrderDate = DateTime.Today;
                o.ShipDate=null;
                o.DeiveryDate= null;
                o.TotalPrice= this.c.TotalPrice;
                int id = bl.order.AddNewOrder(o);*/
                int id = bl.cart.CartConfirmation(Common.ConvertToBoCart(this.c), this.c.CustomerName, this.c.CustomerEmail, this.c.CustomerAddress);
                MessageBox.Show("The order was successfully created");
                Window w = new OrderTracking(bl, id, this.c,this);
                w.Show();
                this.Hide();
            }
            catch(CustomerDetailsAreInValid ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(PlInvalidValueExeption ex){
                MessageBox.Show(ex.Message);
            }
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            this.prevWindow.Show();
            this.Close();
        }
    }
}
