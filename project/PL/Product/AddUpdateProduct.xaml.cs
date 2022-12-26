﻿using System;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for AddUpdateProduct.xaml
/// </summary>
public partial class AddUpdateProduct : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    BO.Product p = new BO.Product();
    BO.Product pro = new BO.Product();

    public AddUpdateProduct(BLApi.IBL bl, BO.Product pro)
    {
        InitializeComponent();
        this.bl = bl;
        this.pro = pro;
        categorySelectorBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
        if (pro.ID != 0)
        {
            categorySelectorBox.SelectedItem = pro.Category;
            input_product_name.Text = pro.Name;
            input_product_instock.Text = pro.inStock.ToString();
            input_product_price.Text = pro.Price.ToString();
            addProductBtn.Visibility = Visibility.Hidden;
            if (pro.Category == null)
                throw new PLEmptyCategoryField();
            if (pro.Name == "")
                throw new PLEmptyNameField();
            if (pro.inStock.ToString() == "")
                throw new PLEmptyAmountField();
            if (pro.Price.ToString() == "")
                throw new PLEmptyPriceField();
            if (pro.Price < 0 || pro.Price > 100000 || pro.Price.GetType().ToString() != "double")
                throw new PlInvalidValueExeption("price");
            if (pro.Name.GetType().ToString() != "string" || pro.Name.Length > 50)
                throw new PlInvalidValueExeption("name");
             if (pro.inStock.GetType().ToString() != "int" || pro.inStock>5000000)
                throw new PlInvalidValueExeption("amount");

        }
        //else if() // if product is ordered do not show the delete btn
        else
        {
            input_product_price.Text = "0";
            input_product_instock.Text = "0";
            categorySelectorBox.Text = "None";
            updateProductBtn.Visibility = Visibility.Hidden;
            deleteProductBtn.Visibility = Visibility.Hidden;
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
            p.Price = double.Parse(input_product_price.Text);
            p.inStock = int.Parse(input_product_instock.Text);
            p.Name = input_product_name.Text;
            bl.product.AddProduct(p);
            BOListWindow w = new BOListWindow(bl);
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
            pro.Price = double.Parse(input_product_price.Text);
            pro.inStock = int.Parse(input_product_instock.Text);
            pro.Name = input_product_name.Text;
            pro.Category = (BO.Category)categorySelectorBox.SelectedItem;
            bl.product.Update(pro);
            BOListWindow w = new BOListWindow(bl);
            w.Show();
            this.Close();
        }
        catch ( BO.blInvalidAmountToken ex)
        {
            MessageBox.Show(ex.Message);
        } 
        catch ( BO.BlInvalidPriceToken ex)
        {
            MessageBox.Show(ex.Message);

        }
        catch ( BO.BlInvalidNameToken ex)
        {
            MessageBox.Show(ex.Message);

        }
        catch ( BO.BlInvalidIdToken ex)
        {
            MessageBox.Show(ex.Message);

        }
        catch ( BO.BlEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message);

        }
        catch ( BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);

        }
    }

    private void deleteProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.product.DeleteProduct(pro.ID);
        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
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
     
    }
}
