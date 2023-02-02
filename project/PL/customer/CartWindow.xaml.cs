using BlImplementation;
using BO;
using DalFacade.DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    PO.Cart cart;
    private PO.Cart p { get; set; }
    public CartWindow(BLApi.IBL _bl, PO.Cart _cart)
    {
        InitializeComponent();
        this.bl = _bl;
        this.cart = _cart;
        this.p = ConvertToPoCart(this.c);
        this.DataContext= this.p;
    }
    private PO.Cart ConvertToPoCart(BO.Cart Pp)
    {
        PO.Cart item = new()
        {
            CustomerAddress = Pp.CustomerAddress,
            CustomerEmail = Pp.CustomerEmail,
            CustomerName = Pp.CustomerName,
            //Items = Pp.items.ForEach(i => ConvertToPoItem(i)).ToList(),
            Items = convertItemsToPOOI(Pp.items),
            TotalPrice= Pp.TotalPrice,
        };
        return item;
    }
    private List<PO.OrderItem> convertItemsToPOOI(List<BO.OrderItem> oil)
    {
        List<PO.OrderItem> returnlist = new();
        oil.ForEach(item =>
        {
            PO.OrderItem item2 = new()
            { ID = item.ID,
                Amount = item.Amount,
               
                Price = item.Price,
                ProductID = item.ProductID,
                ProductName = item.ProductName,
                TotalPrice = item.TotalPrice
            };
            returnlist.Add(item2);            
        });
        return returnlist;
    }
    private PO.OrderItem ConvertToPoItem(BO.OrderItem Pp)
    {
        PO.OrderItem item = new()
        {
           ID= Pp.ID,
           ProductID= Pp.ProductID,
           ProductName= Pp.ProductName,
           Price= Pp.Price,
           TotalPrice= Pp.TotalPrice,
           Amount= Pp.Amount
        };
        return item;
    }


   /* private List<PO.OrderItem> convertList()
    {
        PO.OrderItem i = new PO.OrderItem();
        foreach (BO.OrderItem tmp in list1)
        {
            i = ConvertToPo(tmp);
            List_p.Add(i);
        }
        return List_p;
    }*/



    public void BackToList(object sender, RoutedEventArgs e)
    {
        Window w = new CustomerProductList(bl, this.cart);
        w.Show();
        this.Close();
    }

    private void decreaseProductBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void addProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.cart.Update(this.c, ((PO.OrderItem)(sender as Button).DataContext).ProductID, ((PO.OrderItem)(sender as Button).DataContext).Amount + 1);
            p = ConvertToPoCart(this.c);
            DataContext = p;
        }catch(BlOutOfStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void cartConfirmation(object sender, RoutedEventArgs e)
    {

        new customer.ConfirmCart(bl, this.cart).Show();
        this.Close();
    }
}
