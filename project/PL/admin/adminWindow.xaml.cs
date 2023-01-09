using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;
using BLApi;
//using PL.Order;

/// <summary>
/// Interaction logic for BOListWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    private BO.Product p = new BO.Product();
    private BO.Order o = new BO.Order();
    public AdminWindow(BLApi.IBL bl)
    {
        try
        {
            InitializeComponent();
            this.bl = bl;
            ProductsListview.ItemsSource = bl.product.GetProductList();
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
   

   
    private void itemClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            // p.ID = sender.AnchorItem.
            int pId = (ProductsListview.SelectedItem as BO.ProductForList).ID;
            p = bl.product.GetProductCustomer(pId);
            Window window = new ProductWindow(bl, p, false);
            // addProductBtn.Visibility = Visibility.Hidden;
            window.Show();
            InitializeComponent();
            ProductsListview.ItemsSource = bl.product.GetProductList();
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

    private void addProBtn_Click(object sender, RoutedEventArgs e)
    {
        // updateProductBtn.Visibility = Visibility.Hidden;
        try
        {
            Window window = new ProductWindow(bl, p, false);
            window.Show();
            InitializeComponent();
            ProductsListview.ItemsSource = bl.product.GetProductList();
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

    private void orderClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            // p.ID = sender.AnchorItem.
            int OId = (OrdersListview.SelectedItem as BO.OrderForList).ID;
            o = bl?.order.GetOrderDetails(OId);
            Window window = new OrderWindow(bl, o, false);
            window.Show();
            InitializeComponent();
            OrdersListview.ItemsSource = bl?.order.GetOrderList();
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
}

