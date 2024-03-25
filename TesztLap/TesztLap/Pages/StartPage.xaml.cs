using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TesztLap.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            oop_kep.Source = new BitmapImage(new System.Uri("../Resources/Pictures/kep.jpeg", System.UriKind.Relative));
        }
    }
}
