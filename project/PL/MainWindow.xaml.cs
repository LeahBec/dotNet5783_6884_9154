using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Bl bl = new BlImplementation.Bl();
        BLApi.IBL? bl = BLApi.Factory.get();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void showProductBtn_Click(object sender, RoutedEventArgs e)
        {
            BOListWindow w = new BOListWindow(bl);
            w.Show();
            this.Close();
        }

        private void displayAdminWindow(object sender, RoutedEventArgs e)
        {
            AdminWindow w = new AdminWindow(bl);
            w.Show();
            this.Close();
        }
    }
}
