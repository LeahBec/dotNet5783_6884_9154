﻿using BlImplementation;
using System;
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

namespace PL.customer
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        int orderID;
        BO.Order o;
        PO.Order order;
        BO.OrderTracking ot;
        BLApi.IBL bl;
        BO.Cart cart = new BO.Cart();
        PO.Cart c = new PO.Cart();

        private PO.Order ConvertToPoOrder(BO.Order Bo)
        {
            PO.Order item = new()
            {
                ID = Bo.ID,
                CustomerName = Bo.CustomerName,
                CustomerAddress = Bo.CustomerAddress,
                CustomerEmail = Bo.CustomerEmail,
                DeiveryDate = (DateTime)Bo.DeiveryDate,
                ShipDate = (DateTime)Bo.ShipDate,
                OrderDate = (DateTime)Bo.OrderDate
            };
            return item;
        }

        public OrderTracking(BLApi.IBL _bl ,int _orderID, PO.Cart _c)
        {
            this.c = _c;  
            this.bl = _bl;
            this.orderID = _orderID;
            this.ot = bl.order.OrderTrack(this.orderID);
            InitializeComponent();
            DataContext= this.ot;
            orderDates.DataContext = new { mylist = new ObservableCollection<Tuple<DateTime?, BO.OrderStatus?>>(ot?.dateAndTrack) 
            };
        }
    

        private void show_order_details(object sender, RoutedEventArgs e)
        {
            this.o = bl.order.GetOrderDetails(this.orderID);
            this.order = ConvertToPoOrder(this.o);
            this.order.Items = this.cart.items;
            Window w = new PL.OrderWindow(this.bl, this.order, true, this.cart);
            w.Show();
            //this.Close();
        }

        private void itemClicked(object sender, MouseButtonEventArgs e)
        {

        }

        private void orderDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
