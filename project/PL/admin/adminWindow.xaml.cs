using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;
using BLApi;
using DalFacade.DO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

//using PL.Order;

/// <summary>
/// Interaction logic for BOListWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    private BO.Product p = new BO.Product();
    private BO.Order o = new BO.Order();
    PO.Cart cart = new PO.Cart();
    public ObservableCollection<PO.ProductForList> List_p { get; set; } = new();
    //ObservableCollection<PO.ProductForList> List_p = new();
    IEnumerable<BO.ProductForList> list1;
    PO.ProductForList pro = new PO.ProductForList();
    IEnumerable<BO.OrderForList> list2;
    public ObservableCollection<PO.OrderForList> List_o { get; set; } = new();
    PO.Order order = new PO.Order();
    /*var data =new
     {
        orders =  IEnumerable<BO.OrderForList>,
         products = IEnumerable<PO.ProductForList>
     };*/
    /*private PO.Product ConvertToPoPro(BO.Product Pp)
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

    private PO.Order ConvertToPoOrder(BO.Order Bo)
    {
        PO.Order item = new()
        {
            ID = Bo.ID,
            CustomerName = Bo.CustomerName,
            CustomerAddress = Bo.CustomerAddress,
            CustomerEmail = Bo.CustomerEmail,
            DeiveryDate = (DateTime?)Bo.DeiveryDate,
            ShipDate = (DateTime?)Bo.ShipDate,
            OrderDate = (DateTime?)Bo.OrderDate
        };
        return item;
    }

    private ObservableCollection<PO.OrderForList> convertListOrder()
    {
        *//*list2.ForEach(item =>
        {
            List_o.Add(ConvertToPoOrder(item));
        });*//*

        
        PO.OrderForList i = new PO.OrderForList();
        foreach (BO.OrderForList tmp in list2)
        {
            i = ConvertToPoOrderForList(tmp);
            List_o.Add(i);
        }
        return List_o;
    }
    private PO.OrderForList ConvertToPoOrderForList(BO.OrderForList boo)
    {
        PO.OrderForList returnOrder = new()
        {
            ID = boo.ID,
            CustomerName = boo.CustomerName,
            TotalPrice = boo.TotalPrice,
            AmountOfItems = boo.AmountOfItems,
            Status = boo.Status,
        };
        return returnOrder;
    }*/
    public AdminWindow(BLApi.IBL bl, PO.Cart c)
    {
        try
        {
            /*if (_list_p == null) this.List_p = new();
            else this.List_p = _list_p;*/
            InitializeComponent();
            this.bl = bl;
            this.cart = c;
            // OrdersListview.ItemsSource = bl.order.GetOrderList();
            list1 = bl.product.GetProductList();
            //ProductsListview.ItemsSource = bl.product.GetProductList();
            List_p = convertList();
            list2 = bl.order.GetOrderList();
            List_o = Common.convertListOrder(list2, List_o);
            Tuple<ObservableCollection<PO.ProductForList>, ObservableCollection<PO.OrderForList>> dcT =
                new Tuple<ObservableCollection<PO.ProductForList>, ObservableCollection<PO.OrderForList>>(this.List_p, this.List_o);
            this.DataContext = dcT;
            //this.DataContext = this.List_p;
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
            Window window = new ProductWindow(bl, Common.ConvertToPoPro(p), false, this.cart, this.List_p);
            // addProductBtn.Visibility = Visibility.Hidden;
            window.Show();
            //InitializeComponent();
            //list1 = bl.product.GetProductList();
            // convertList();
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
            Window window = new ProductWindow(bl, Common.ConvertToPoPro(p), false, this.cart, this.List_p);
            window.Show();
            // InitializeComponent();
            // ProductsListview.ItemsSource = bl.product.GetProductList();
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
            int OId = (OrdersListview.SelectedItem as PO.OrderForList).ID;
            o = bl.order.GetOrderDetails(OId);
            Window window = new OrderWindow(bl, Common.ConvertToPoOrder(o), false, this.cart, this.List_o);
            window.Show();
           // InitializeComponent();
            //  OrdersListview.ItemsSource = bl?.order.GetOrderList();
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

