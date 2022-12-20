using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;
using BLApi;
/// <summary>
/// Interaction logic for BOListWindow.xaml
/// </summary>
public partial class BOListWindow : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    private BO.Product p = new BO.Product();
    public BOListWindow(BLApi.IBL bl)
    {
        InitializeComponent();
        this.bl = bl;
        categorySelectorBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
        ProductsListview.ItemsSource = bl.product.GetProductList();
    }
    private void categorySelectorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            BO.Category cat = (BO.Category)categorySelectorBox.SelectedItem;
            var list = bl.product.GetListByCategory(cat);
            ProductsListview.ItemsSource = list;
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void addProductBtn_Click(object sender, RoutedEventArgs e)
    {
        // updateProductBtn.Visibility = Visibility.Hidden;
        try
        {
            Window window = new AddUpdateProduct(bl, p);
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

    private void itemClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            // p.ID = sender.AnchorItem.
            p = bl.product.GetProductManager((ProductsListview.SelectedItem as BO.ProductForList).ID);
            Window window = new AddUpdateProduct(bl, p);
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

    private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}

