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

namespace PL
{
    /// <summary>
    /// Interaction logic for orderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BLApi.IBL? bl = BLApi.Factory.get();
        BO.Order o = new BO.Order();
        BO.Order or = new BO.Order();
        bool isCustomer;
        BO.Cart cart=new BO.Cart();
        public OrderWindow(BLApi.IBL bl, BO.Order ord, bool _isCustomer, BO.Cart c)
        {
            try
            {
                this.isCustomer= _isCustomer;
                InitializeComponent();
                this.bl = bl;
                this.cart= c;
                input_order_ID.IsReadOnly = isCustomer;
                input_order_totalPrice.IsReadOnly = isCustomer;
                input_order_deliveryDate.IsReadOnly = isCustomer;
                input_order_shipDate.IsReadOnly = isCustomer;
                input_order_orderDate.IsReadOnly = isCustomer;
                input_order_customerAddress.IsReadOnly = isCustomer;
                input_order_customerEmail.IsReadOnly = isCustomer;
                input_order_customerName.IsReadOnly = isCustomer;
                BO.Order order = bl.order.GetOrderDetails(ord.ID);
                this.or = order;
                this.DataContext = this.or;
                if(this.isCustomer)
                {
                    updateOrderBtn.IsEnabled = false;
                    updateOrderDeliveryBtn.IsEnabled = false;
                    updateOrderShippingBtn.IsEnabled = false;
                }
/*                if (or.CustomerName == "")
                    throw new PLEmptyNameField();
                if (or.CustomerEmail == "")
                    throw new PLEmptyAmountField();
                if (or.CustomerAdress == "")
                    throw new PLEmptyPriceField();
                if (or.OrderDate < DateTime.MinValue || or.OrderDate > DateTime.Now)
                    throw new PlInvalidValueExeption("orderDate");
                if (or.ShipDate < DateTime.MinValue || or.ShipDate > DateTime.Now)
                    throw new PlInvalidValueExeption("shipDate");
                if (or.DeiveryDate < DateTime.MinValue || or.DeiveryDate > DateTime.Now)
                    throw new PlInvalidValueExeption("deliveryDate");*/
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void updateOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                o.CustomerAddress = this.or.CustomerAddress;
                o.CustomerEmail = this.or.CustomerEmail;
                o.CustomerName = this.or.CustomerName;
                o.OrderDate = this.or.OrderDate;
                o.ShipDate = this.or.ShipDate;
                o.DeiveryDate = this.or.DeiveryDate;
                o.ID = this.or.ID;
                bl.order.UpdateOrderForManager(o);/////
                AdminWindow w = new AdminWindow(bl, this.cart);
                w.Show();
                this.Close();
            }
            catch (BO.blInvalidAmountToken ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BlInvalidPriceToken ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (BO.BlInvalidNameToken ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (BO.BlInvalidIdToken ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (BO.BlEntityNotFoundException ex)
            {
                MessageBox.Show(ex.Message);

            }

            catch (BO.BlDefaultException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void updateOrderShippingBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = this.or.ID;
            bl?.order.UpdateOrderShipping(id);
        }
        private void updateOrderDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = this.or.ID;
            bl?.order.UpdateOrderDelivery(id);
        }

        private void backToList(object sender, RoutedEventArgs e)
        {
            if (!this.isCustomer)
            {
                Window w = new AdminWindow(bl, this.cart);
                w.Show();
                this.Close();
            }
            else
            {
               /* Window w = new customer.OrderTracking(bl);
                w.Show();*/
                this.Close();
            }
        }
    }
}
