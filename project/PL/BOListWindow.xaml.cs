using BlImplementation;
using System.Windows;
using System.Windows.Controls;

namespace PL;
/// <summary>
/// Interaction logic for BOListWindow.xaml
/// </summary>
public partial class BOListWindow : Window
{
    // IBl bl = new IBl();
    public BOListWindow(Bl bl)
    {
        InitializeComponent();
        Bl listBl = bl;
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}

