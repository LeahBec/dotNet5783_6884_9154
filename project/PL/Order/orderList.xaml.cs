using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;
using BLApi;
using PL.Order;

/// <summary>
/// Interaction logic for BOListWindow.xaml
/// </summary>
public partial class orderListWindow : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    private BO.Order o = new BO.Order();
    public orderListWindow(BLApi.IBL bl)
    {
        try
        {
            InitializeComponent();
            this.bl = bl;
            OrdersListview.ItemsSource = bl.order.GetOrderList();
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
   

    private void addOrderBtn_Click(object sender, RoutedEventArgs e)
    {
        // updateProductBtn.Visibility = Visibility.Hidden;
        try
        {
            Window window = new orderWindow(bl, o);
            window.Show();
            InitializeComponent();
            OrdersListview.ItemsSource = bl.order.GetOrderList();
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlExceptionFailedToRead ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void itemClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            // p.ID = sender.AnchorItem.
            o = bl.order.GetOrderDetails((OrdersListview.SelectedItem as BO.ProductForList).ID);
            Window window = new orderWindow(bl, o);
            // addProductBtn.Visibility = Visibility.Hidden;
            window.Show();
            InitializeComponent();
            OrdersListview.ItemsSource = bl.product.GetProductList();
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlExceptionFailedToRead ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}

