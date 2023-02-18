﻿using BO;
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
    Window prevWindow;
    bool inputProReadOnly { get; set; }
    public bool isCustomer { get; set; }
    int id;
    PO.Product p_ = new PO.Product();
    ObservableCollection<PO.ProductForList> list_p;
    Tuple<PO.Product, bool> dcT;
    public ProductWindow(BLApi.IBL bl, PO.Product pro, bool _isCustomer, PO.Cart _c, Window prevWindow, ObservableCollection<PO.ProductForList> _list_p = null)
    {
        try
        {
            this.isCustomer = _isCustomer;
            InitializeComponent();
            this.bl = bl;
            this.c = _c;
            this.p_ = pro;
            this.id = pro.ID;
            this.prevWindow = prevWindow;
            if (_list_p == null) this.list_p = new();
            else this.list_p = _list_p;
            categorySelectorBox.IsReadOnly = isCustomer;
            if (!isCustomer) addBtn.Visibility = Visibility.Hidden;
            categorySelectorBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            this.dcT = new Tuple<PO.Product, bool>(this.p_, this.isCustomer);
            this.DataContext = this.dcT;
            if (pro.ID != 0)
            {
                //this.DataContext = this.dcT;

                BO.Product prod = bl.product.GetProductCustomer(pro.ID);
                this.p_ = Common.ConvertToPoPro(prod);
                addProductBtn.Visibility = Visibility.Hidden;
                if (this.p_.Category == null)
                    throw new PLEmptyCategoryField();
                if (this.p_.Name == "")
                    throw new PLEmptyNameField();
                if (this.p_.inStock.ToString() == "")
                    throw new PLEmptyAmountField();
                if (this.p_.Price.ToString() == "")
                    throw new PLEmptyPriceField();
                if (this.p_.Price < 0 || pro.Price > 100000)
                    throw new PlInvalidValueExeption("price");
                if (this.p_.Name.GetType().Name != "String" || pro.Name.Length > 50)
                    throw new PlInvalidValueExeption("name");
                if (this.p_.inStock.GetType().Name != "Int32" || pro.inStock > 5000000)
                    throw new PlInvalidValueExeption("amount");
            }
            //else if() // if product is ordered do not show the delete btn
            else
            {
                //this.DataContext = this.dcT;

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

        p_.Category = (BO.Category)categorySelectorBox.SelectedItem;
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
            this.pro = Common.ConvertToBo(p_);///////////////////
            int id = bl.product.AddProduct(this.pro);
            this.p_.ID = id;
            this.list_p.Add(Common.ConvertPFLToP(this.p_));
            backToList();
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
            this.pro = Common.ConvertToBo(p_);
            list_p.Remove(list_p.Where(i => i.ID == p_.ID).Single());
            list_p.Add(Common.ConvertPFLToP(this.p_));
            bl.product.Update(Common.ConvertToBo(p_));/////

            backToList();
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
            bl?.product.DeleteProduct(pro.ID);
            BO.ProductForList ppp = Common.ConvertPFLToB(pro);
            p_ = Common.ConvertPToPFL(Common.ConvertToPo(ppp));
            var a = Common.ConvertPFLToP(p_);
            list_p.Remove(list_p.Where(i => i.ID == a.ID).Single());
            backToList();
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

    private void backToList()
    {
        if (this.isCustomer)
        {

            this.prevWindow.Show();
            this.Close();
        }
        else
        {
            this.prevWindow.Show();
            this.Close();
        }
    }


    private void addToCart(object sender, RoutedEventArgs e)
    {
        try
        {
            this.cart = Common.ConvertToBoCart(this.c);
            bl.cart.AddProductToCart(this.cart, this.id);
            this.c = Common.ConvertToPoCart(this.cart, this.c);
            backToList();
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

    private void backToList1(object sender, RoutedEventArgs e)
    {
        backToList();
    }
}

