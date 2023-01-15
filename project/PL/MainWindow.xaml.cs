using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Bl bl = new BlImplementation.Bl();
        BLApi.IBL bl = BLApi.Factory.get();
        BO.Cart cart = new BO.Cart();
        private int ID;
        public MainWindow()
        {
            InitializeComponent();
        /*    Main.DataContext = new
            {
                ID  =0
            };*/
        }

        private void showProductBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerProductList w = new CustomerProductList(bl,this.cart);
            w.Show();
            this.Close();
        }

        private void displayAdminWindow(object sender, RoutedEventArgs e)
        {
            AdminWindow w = new AdminWindow(bl, this.cart );
            w.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void OrderTracking_clicked(object sender, RoutedEventArgs e)
        {
            //this.ID =int.Parse( id.Text);
            this.ID = int.Parse(id_.Text);
            Window w = new customer.OrderTracking(bl ,this.ID, this.cart);
            w.Show();
            this.Hide();
        }
    }
}
