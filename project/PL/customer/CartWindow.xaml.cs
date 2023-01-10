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
    public CartWindow(BLApi.IBL _bl, BO.Cart cart)
    {
        InitializeComponent();
        this.bl = _bl;
        this.c = cart;
        this.DataContext= this.c;
    }

    public void BackToList(object sender, RoutedEventArgs e)
    {
        Window w = new CustomerProductList(bl, this.c);
        w.Show();
        this.Close();
    }

    private void decreaseProductBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void addProductBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
