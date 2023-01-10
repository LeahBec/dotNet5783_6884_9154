using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;
using BLApi;
/// <summary>
/// Interaction logic for BOListWindow.xaml
/// </summary>
public partial class CustomerProductList : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    private BO.Product p = new BO.Product();
    BO.Cart cart = new BO.Cart();
    public CustomerProductList(BLApi.IBL bl, BO.Cart c)
    {
        try
        {
            InitializeComponent();
            this.bl = bl;
            this.cart= c;
            categorySelectorBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            ProductsListview.ItemsSource = bl.product.GetProductList();
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
    private void categorySelectorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            BO.Category cat = (BO.Category)categorySelectorBox.SelectedItem;
            var list = bl?.product.GetListByCategory(cat);
            ProductsListview.ItemsSource = list;
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

    private void addProductBtn_Click(object sender, RoutedEventArgs e)
    {
        // updateProductBtn.Visibility = Visibility.Hidden;
        try
        {
            Window window = new ProductWindow(bl, p, true, this.cart);
            window.Show();
            InitializeComponent();
            ProductsListview.ItemsSource = bl.product.GetProductList();
            this.Close();
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
            p = bl.product.GetProductManager((ProductsListview.SelectedItem as BO.ProductForList).ID);
            Window window = new ProductWindow(bl, p, true, this.cart);
            // addProductBtn.Visibility = Visibility.Hidden;
            window.Show();
            InitializeComponent();
            ProductsListview.ItemsSource = bl.product.GetProductList();
            this.Close();
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

    private void showCart(object sender, RoutedEventArgs e)
    {
        Window w = new CartWindow(bl, this.cart);
        w.Show();
        this.Close();
    }
}

