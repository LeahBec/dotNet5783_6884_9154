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
        }
    }
}
