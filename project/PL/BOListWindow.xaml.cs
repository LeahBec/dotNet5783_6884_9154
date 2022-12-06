using BlImplementation;
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
    private BLApi.IBL bl;
    private BO.Product p = new BO.Product();
    public BOListWindow(BLApi.IBL bl)
    {
        InitializeComponent();
        this.bl = bl;
        categorySelectorBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
        ProductsListview.ItemsSource = bl.product.GetProductList();
    }
    
    private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }



    private void categorySelectorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BO.Category cat =(BO.Category) categorySelectorBox.SelectedItem;
        var list = bl.product.GetListByCategory(cat);
        ProductsListview.ItemsSource = list;
    }

    private void addProductBtn_Click(object sender, RoutedEventArgs e)
    {
        Window window = new AddUpdateProduct(bl, p);
        window.Show();
    }

    private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void itemClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

        // p.ID = sender.AnchorItem.
        p = bl.product.GetProductManager((ProductsListview.SelectedItem as BO.ProductForList).ID);
        Window window = new AddUpdateProduct(bl, p);
        window.Show();

        

    }
}

