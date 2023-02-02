using BO;
using DalFacade.DO;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualBasic;

namespace PL;

/// <summary>
/// Interaction logic for AddUpdateProduct.xaml
/// </summary>
public partial class ProductWindow : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    BO.Product p = new BO.Product();
    BO.Product pro = new BO.Product();
    BO.Cart cart = new BO.Cart();
    PO.Cart c = new PO.Cart();
    bool inputProReadOnly { get; set; }
    public bool isCustomer { get; set; }
    int id;
    PO.Product p_ = new PO.Product();
    ObservableCollection<PO.ProductForList> list_p;
    private BO.Product ConvertToBo(PO.Product Pp)
    {
        BO.Product item = new()
        {
            ID = Pp.ID,
            Name = Pp.Name,
            Price = Pp.Price,
            Category = (BO.Category)(eCategory)Pp.Category,
            inStock = Pp.inStock
        };
        return item;
    }

    private PO.ProductForList ConvertPFLToP(PO.Product p)
    {
        PO.ProductForList item = new();
        item.ID = p.ID;
        item.Name = p.Name;
        item.Price = p.Price;
        item.Category = (DalFacade.DO.eCategory)p.Category;
        return item;
    }

    private PO.Product ConvertPToPFL(PO.ProductForList p)
    {
        PO.Product item = new();
        item.ID = p.ID;
        item.Name = p.Name;
        item.Price = p.Price;
        item.Category = (Category)p.Category;

        return item;
    }
    private BO.ProductForList ConvertPFLToB(BO.Product p)
    {
        BO.ProductForList item = new();
        item.ID = p.ID;
        item.Name = p.Name;
        item.Price = p.Price;
        item.Category = p.Category;
        return item;
    }

    public ProductWindow(BLApi.IBL bl, PO.Product pro, bool _isCustomer, PO.Cart _c, ObservableCollection<PO.ProductForList> _list_p = null)
    {

        try
        {
            this.isCustomer = _isCustomer;
            InitializeComponent();
            this.bl = bl;
            this.c = _c;
            this.id = pro.ID;
            if (_list_p == null) this.list_p = new();
            else this.list_p = _list_p;
            
            categorySelectorBox.IsReadOnly = isCustomer;
           /* input_product_instock.IsReadOnly = isCustomer;
            input_product_price.IsReadOnly = isCustomer;
            input_product_name.IsReadOnly = isCustomer;*/

            if (isCustomer) deleteProductBtn.Visibility = Visibility.Hidden;
            if (isCustomer) updateProductBtn.Visibility = Visibility.Hidden;
            if (!isCustomer) addBtn.Visibility = Visibility.Hidden;
            categorySelectorBox.ItemsSource = Enum.GetValues(typeof(BO.Category));

            if (pro.ID != 0)
            {
                BO.Product prod = bl.product.GetProductCustomer(pro.ID);
                this.pro = prod;
                this.DataContext = this.pro;
                addProductBtn.Visibility = Visibility.Hidden;
                if (pro.Category == null)
                    throw new PLEmptyCategoryField();
                if (pro.Name == "")
                    throw new PLEmptyNameField();
                if (pro.inStock.ToString() == "")
                    throw new PLEmptyAmountField();
                if (pro.Price.ToString() == "")
                    throw new PLEmptyPriceField();
                if (pro.Price < 0 || pro.Price > 100000)
                    throw new PlInvalidValueExeption("price");
                if (pro.Name.GetType().Name != "String" || pro.Name.Length > 50)
                    throw new PlInvalidValueExeption("name");
                if (pro.inStock.GetType().Name != "Int32" || pro.inStock > 5000000)
                    throw new PlInvalidValueExeption("amount");
            }
            //else if() // if product is ordered do not show the delete btn
            else
            {
                this.DataContext = this.pro;
                updateProductBtn.Visibility = Visibility.Hidden;
                deleteProductBtn.Visibility = Visibility.Hidden;
            }
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message);
        }
    }


    private void categorySelectorBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

        p.Category = (BO.Category)categorySelectorBox.SelectedItem;
    }

    private void addProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            updateProductBtn.Visibility = Visibility.Hidden;
            p_.ID = 10;
            p_.Price = this.pro.Price;
            p_.inStock = this.pro.inStock;
            p_.Name = this.pro.Name;
            p_.Category = this.pro.Category;
            this.pro = ConvertToBo(p_);///////////////////
            int id = bl.product.AddProduct(this.pro);
            this.p_.ID = id;
            this.list_p.Add(ConvertPFLToP(this.p_));
            this.Close();
            //    AdminWindow w = new AdminWindow(bl, this.cart,this.list_p);
            // w.Show();
        }
        catch (BO.blInvalidAmountToken ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlInvalidPriceToken ex)
        {
            MessageBox.Show(ex.Message);

        }
        catch (BO.BlInvalidNameToken ex)
        {
            MessageBox.Show(ex.Message);

        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);

        }
    }

    private void updateProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            addProductBtn.Visibility = Visibility.Hidden;
            p_.ID = this.pro.ID; 
            p_.Price = this.pro.Price;
            p_.inStock = this.pro.inStock;
            p_.Name = this.pro.Name;
            p_.Category = this.pro.Category;
            this.pro = ConvertToBo(p_);
            list_p.Remove(list_p.Where(i => i.ID == p_.ID).Single());
            list_p.Add(ConvertPFLToP(this.p_));
            bl.product.Update(ConvertToBo(p_));/////

            this.Close();
        }
        catch (BO.blInvalidAmountToken ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlInvalidPriceToken ex)
        {
            MessageBox.Show(ex.Message);

        }
        catch (BO.BlInvalidNameToken ex)
        {
            MessageBox.Show(ex.Message);

        }
        catch (BO.BlInvalidIdToken ex)
        {
            MessageBox.Show(ex.Message);

        }
        catch (BO.BlEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message);

        }

        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);

        }
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
  
    private void deleteProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.product.DeleteProduct(pro.ID);
            /* AdminWindow w = new AdminWindow(bl, this.cart);
             w.Show();*/



            BO.ProductForList ppp = ConvertPFLToB(pro);
            p_ = ConvertPToPFL(ConvertToPo(ppp));
            var a = ConvertPFLToP(p_);
            list_p.Remove(list_p.Where(i => i.ID == a.ID).Single());
            this.Close();
        }

        catch (BO.BlProductExistsInAnOrder ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlInvalidIdToken ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void backToList(object sender, RoutedEventArgs e)
    {
        if (this.isCustomer)
        {
            Window w = new CustomerProductList(bl, this.c);
            w.Show();
            this.Close();
        }
        else
        {
            Window w = new AdminWindow(bl, this.c);
            w.Show();
            this.Close();
        }
    }

    private void addToCart(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.cart.AddProductToCart(this.cart, this.id);
            Window w = new CustomerProductList(bl, this.c);
            w.Show();
            this.Close();
        }
        catch (BlOutOfStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}

