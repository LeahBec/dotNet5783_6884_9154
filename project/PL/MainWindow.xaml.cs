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
        
/*        private int ID;
*/        public MainWindow()
        {
            InitializeComponent();
        /*    Main.DataContext = new
            {
                ID  =0
            };*/
        }

        private void showProductBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerProductList w = new CustomerProductList(bl);
            w.Show();
            this.Close();
        }

        private void displayAdminWindow(object sender, RoutedEventArgs e)
        {
            AdminWindow w = new AdminWindow(bl);
            w.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void OrderTracking_clicked(object sender, RoutedEventArgs e)
        {
            //this.ID =int.Parse( id.Text);
            Window w = new customer.OrderTracking(bl ,0);
            w.Show();
            this.Hide();
        }
    }
}
