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

namespace PL;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>


public partial class CartWindow : Window
{
    
    BLApi.IBL bl;
    BO.Cart c;
    public CartWindow(BLApi.IBL _bl)
    {
        InitializeComponent();
        this.bl = _bl;
        this.c = new BO.Cart();

    }

    public void BackToList(object sender, RoutedEventArgs e)
    {
        /* Window w = new CartWindow(bl);
         w.Show();
         this.Close();*/
    }

}
