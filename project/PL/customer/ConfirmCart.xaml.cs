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
        public ConfirmCart(BLApi.IBL _bl, PO.Cart _c)
        {
            InitializeComponent();
            this.bl = _bl;
            this.c = _c;
            DataContext = this.c;
        }
        private PO.OrderItem converToPoOi(BO.OrderItem oi)
        {
            PO.OrderItem item = new()
            {
                Amount = oi.Amount,
                ID = oi.ID,
                ProductID = oi.ProductID,
                Price = oi.Price,
                ProductName = oi.ProductName
            };
            return item;
        }
        private List<PO.OrderItem> convertToPoOiList(List<BO.OrderItem> loi)
        {
            List<PO.OrderItem> returnList = new();
            foreach (BO.OrderItem oi in loi)
            {
                returnList.Add(converToPoOi(oi));
            }
            return returnList;
        }

        private BO.OrderItem converToBoOi(PO.OrderItem oi)
        {
            BO.OrderItem item = new()
            {
                Amount = oi.Amount,
                ID = oi.ID,
                ProductID = oi.ProductID,
                Price = oi.Price,
                ProductName = oi.ProductName,
                TotalPrice = oi.TotalPrice,
            };
            return item;
        }
        private List<BO.OrderItem> convertToBoOiList(List<PO.OrderItem> loi)
        {
            List<BO.OrderItem> returnList = new();
            foreach (PO.OrderItem oi in loi)
            {
                returnList.Add(converToBoOi(oi));
            }
            return returnList;
        }


        private PO.Cart ConvertToPoCart(BO.Cart ca)
        {
            PO.Cart item = new()
            {
                
            CustomerAddress = ca.CustomerAddress,
            CustomerEmail = ca.CustomerEmail,
            CustomerName = ca.CustomerName,
            TotalPrice = ca.TotalPrice,
            };
            item.Items = convertToPoOiList(ca.items);
            return item;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.c.CustomerEmail == "" || this.c.CustomerAddress == "" || this.c.CustomerName == "")
                {
                    throw new PlInvalidValueExeption("one or more of the customer details is missing");
                }
                BO.Order o = new BO.Order();
                o.CustomerAddress = this.c.CustomerAddress;
                o.CustomerEmail= this.c.CustomerEmail;
                o.CustomerName = this.c.CustomerName;
                o.Items = convertToBoOiList(this.c.Items);
                o.OrderDate = DateTime.Today;
                o.ShipDate=DateTime.MinValue;
                o.DeiveryDate= DateTime.MinValue;
                o.TotalPrice= this.cart.TotalPrice;
                int id = bl.order.AddNewOrder(o);
                MessageBox.Show("The order was successfully created");
                Window w = new OrderTracking(bl, id, this.c);
                w.Show();
                this.Close();
            }
            catch(PlInvalidValueExeption ex){
                MessageBox.Show(ex.Message);
            }
        }
    }
}
