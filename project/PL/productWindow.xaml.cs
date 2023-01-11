using BO;
using System;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for AddUpdateProduct.xaml
/// </summary>
public partial class ProductWindow : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    BO.Product p = new BO.Product();
    BO.Product pro = new BO.Product();
    bool isCustomer;
    BO.Cart cart = new BO.Cart();
    int id;
    PO.Product p_;
    public ProductWindow(BLApi.IBL bl, BO.Product pro, bool _isCustomer, BO.Cart c)
    {
        try
        {
            this.isCustomer = _isCustomer;
            InitializeComponent();
            this.bl = bl;
            this.cart = c;
            this.id = pro.ID;
            categorySelectorBox.IsReadOnly = isCustomer;
            input_product_instock.IsReadOnly = isCustomer;
            input_product_price.IsReadOnly = isCustomer;
            input_product_name.IsReadOnly = isCustomer;
            if (isCustomer) deleteProductBtn.Visibility = Visibility.Hidden;
            if (isCustomer) updateProductBtn.Visibility = Visibility.Hidden;
            if(!isCustomer) addBtn.Visibility = Visibility.Hidden;
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
            p.ID = 10;
            p.Price = this.pro.Price;
            p.inStock = this.pro.inStock;
            p.Name = this.pro.Name;
            p.Category = this.pro.Category;
            bl.product.AddProduct(p);
            AdminWindow w = new AdminWindow(bl, this.cart);
            w.Show();
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
            pro.Price = this.pro.Price;
            pro.inStock = this.pro.inStock;
            pro.Name = this.pro.Name;
            pro.Category = this.pro.Category;
            bl.product.Update(pro);/////
            p_.GetNew(pro);
            AdminWindow w = new AdminWindow(bl, this.cart);
            w.Show();
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

    private void deleteProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.product.DeleteProduct(pro.ID);
            AdminWindow w = new AdminWindow(bl, this.cart);
            w.Show();
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
            Window w = new CustomerProductList(bl,this.cart);
            w.Show();
            this.Close();
        }
        else
        {
            Window w = new AdminWindow(bl, this.cart);
            w.Show();
            this.Close();
        }
    }

    private void addToCart(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.cart.AddProductToCart(this.cart, this.id);
            Window w = new CustomerProductList(bl, this.cart);
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

