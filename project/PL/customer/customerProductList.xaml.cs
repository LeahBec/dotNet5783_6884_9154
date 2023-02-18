﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;
using BLApi;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using DalFacade.DO;
using System.ComponentModel;

/// <summary>
/// Interaction logic for BOListWindow.xaml
/// </summary>
public partial class CustomerProductList : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    private BO.Product p = new BO.Product();
    BO.Cart cart = new BO.Cart();
    PO.Cart c = new PO.Cart();
    IEnumerable<BO.ProductForList> list1;
    ObservableCollection<PO.ProductForList> List_p = new();
    PO.ProductForList pro = new PO.ProductForList();
    Window prevWindow;
    public CustomerProductList(BLApi.IBL bl, PO.Cart _c, Window _prevWindow)
    {
        try
        {
            InitializeComponent();
            this.bl = bl;
            this.c = _c;
            this.prevWindow = _prevWindow;
            list1 = bl.product.GetProductList();
            categorySelectorBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            ProductsListview.ItemsSource = bl.product.GetProductList();
            Common.convertList(List_p, list1);
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
            Window window = new ProductWindow(bl, Common.ConvertToPoPro(p), true, this.c, this);
            window.Show();
            InitializeComponent();
            ProductsListview.ItemsSource = bl.product.GetProductList();
            this.Hide();
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
            Window window = new ProductWindow(bl, Common.ConvertToPoPro(p), true, this.c, this);
            // addProductBtn.Visibility = Visibility.Hidden;
            window.Show();
            InitializeComponent();
            ProductsListview.ItemsSource = bl.product.GetProductList();
            this.Hide();
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
        Window w = new CartWindow(bl, this.c);
        w.Show();
        this.Hide();
    }

    private void goBack(object sender, RoutedEventArgs e)
    {
        this.prevWindow.Show();
        this.Close();
    }
}

