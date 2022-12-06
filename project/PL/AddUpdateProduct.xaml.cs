﻿using System;
using System.Windows;


namespace PL;

/// <summary>
/// Interaction logic for AddUpdateProduct.xaml
/// </summary>
public partial class AddUpdateProduct : Window
{
    private BLApi.IBL bl;
    BO.Product p = new BO.Product();
    BO.Product pro = new BO.Product();
    public AddUpdateProduct(BLApi.IBL bl, BO.Product pro)
    {

        InitializeComponent();
        this.bl = bl;
        this.pro = pro;
        categorySelectorBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
        if (pro != null)
        {
            categorySelectorBox.SelectedItem = pro.Category;
            input_product_name.Text = pro.Name;
            input_product_instock.Text = pro.inStock.ToString();
            input_product_price.Text = pro.Price.ToString();
        }
    }


    private void categorySelectorBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

        p.Category = (BO.Category)categorySelectorBox.SelectedItem;
    }

    private void addProductBtn_Click(object sender, RoutedEventArgs e)
    {
        p.ID = 10;
        p.Price = double.Parse(input_product_price.Text);
        p.inStock = int.Parse(input_product_instock.Text);
        p.Name = input_product_name.Text;

        bl.product.AddProduct(p);
    }

    private void updateProductBtn_Click(object sender, RoutedEventArgs e)
    {

        pro.Price = double.Parse(input_product_price.Text);
        pro.inStock = int.Parse(input_product_instock.Text);
        pro.Name = input_product_name.Text;

        bl.product.Update(pro);
        BOListWindow w = new BOListWindow(bl);
        w.Show();
    }
}

