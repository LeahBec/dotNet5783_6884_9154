﻿using System;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for orderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BLApi.IBL? bl = BLApi.Factory.get();
        PO.Order o = new PO.Order();
        BO.Order or = new BO.Order();
        bool isCustomer;
        PO.Cart cart=new PO.Cart();
        ObservableCollection<PO.OrderForList> list_o;
        Window prevWindow;
        Tuple<PO.Order, bool, bool> dcT;
        public OrderWindow(BLApi.IBL bl, PO.Order ord, bool _isCustomer, PO.Cart c, Window prevWindow, ObservableCollection<PO.OrderForList> list = null)
        {
            try
            {
                this.isCustomer= _isCustomer;
                InitializeComponent();
                this.bl = bl;
                this.cart= c;
                this.prevWindow = prevWindow;
                if (list == null) this.list_o = new();
                else this.list_o = list;
                BO.Order order = bl.order.GetOrderDetails(ord.ID);
                this.or = order;
                this.o = Common.ConvertToPoOrder(or, o);
                this.dcT = new Tuple<PO.Order, bool, bool>(this.o, isCustomer, !isCustomer);
                this.DataContext = this.dcT;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void updateOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                list_o.Remove(list_o.Where(i => i.ID == o.ID).Single());
                list_o.Add(Common.ConvertPFLToP(this.o));
                bl.order.UpdateOrderForManager(Common.ConvertToBo(o));
                prevWindow.Show();
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
        private void updateOrderShippingBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = this.or.ID;
                List<PO.OrderItem> list;
                or = Common.ConvertToBo(o);
                list = Common.convertItemsToPOOI(or.Items);
                or = bl?.order.UpdateOrderShipping(id);
                Common.ConvertToPoOrder(or, o);
                o.Items = list;
/*                foreach (var item in list) { o.TotalPrice += item.Price; }
*/                list.ForEach(item => o.TotalPrice += item.Price);
            }
            catch (BO.BlDefaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void updateOrderDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = this.or.ID;
                List<PO.OrderItem> list;
                or = Common.ConvertToBo(o);
                list = Common.convertItemsToPOOI(or.Items);
                or = bl?.order.UpdateOrderDelivery(id);
                Common.ConvertToPoOrder(or, o);
                o.Items = list;
                list.ForEach(item => o.TotalPrice += item.Price);
/*                foreach (var item in list) { o.TotalPrice += item.Price; }
*/            }
            catch(BO.BlDefaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backToList(object sender, RoutedEventArgs e)
        {
            if (!this.isCustomer)
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
    }
}
