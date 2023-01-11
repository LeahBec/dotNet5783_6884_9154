using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;
using BLApi;
using DalFacade.DO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

//using PL.Order;

/// <summary>
/// Interaction logic for BOListWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    private BO.Product p = new BO.Product();
    private BO.Order o = new BO.Order();
    BO.Cart cart= new BO.Cart();
    ObservableCollection<PO.ProductForList> List_p = new();
    IEnumerable<BO.ProductForList> list1;
    PO.ProductForList pro = new PO.ProductForList();
    /*var data =new
     {
        orders =  IEnumerable<BO.OrderForList>,
         products = IEnumerable<PO.ProductForList>
     };*/
    private PO.Product ConvertToPoPro(BO.Product Pp)
    {
        PO.Product item = new()
        {
            ID = Pp.ID,
            Name = Pp.Name,
            Price = Pp.Price,
            Category = (BO.Category)(eCategory)Pp.Category,
            inStock = Pp.inStock
        };
        return item;
    }

    public AdminWindow(BLApi.IBL bl, BO.Cart c)
    {
        try
        {
            InitializeComponent();
            this.bl = bl;
            this.cart = c;
            OrdersListview.ItemsSource = bl.order.GetOrderList();
            list1 = bl.product.GetProductList();
            //ProductsListview.ItemsSource = bl.product.GetProductList();
            convertList();
            this.DataContext = this.List_p;
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




    private ObservableCollection<PO.ProductForList> convertList()
    {
        PO.ProductForList i = new PO.ProductForList();
        foreach (BO.ProductForList tmp in list1)
        {
            i = ConvertToPo(tmp);
            List_p.Add(i);
        }
        return List_p;
    }



    private PO.ProductForList ConvertToPo(BO.ProductForList Bp)
    {
        PO.ProductForList item = new()
        {
            ID = Bp.ID,
            Name = Bp.Name,
            Price = Bp.Price,
            Category = (eCategory)Bp.Category
        };
        return item;
    }
     private BO.ProductForList ConvertToBo(PO.ProductForList Pp)
    {
       BO.ProductForList item = new()
        {
            ID = Pp.ID,
            Name = Pp.Name,
            Price = Pp.Price,
            Category = (BO.Category)(eCategory)Pp.Category
        };
        return item;
    }

    private void itemClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            // p.ID = sender.AnchorItem.
            //BO.ProductForList  poo= ProductsListview.SelectedItem;
            int pId = (ProductsListview.SelectedItem as PO.ProductForList).ID;
            p = bl.product.GetProductCustomer(pId);
            Window window = new ProductWindow(bl, ConvertToPoPro(p), false, this.cart);
            // addProductBtn.Visibility = Visibility.Hidden;
            window.Show();
            //InitializeComponent();
            list1 = bl.product.GetProductList();
            convertList();
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
            Window window = new ProductWindow(bl, ConvertToPoPro(p), false, this.cart);
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
            Window window = new OrderWindow(bl, o, false,this.cart);
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

