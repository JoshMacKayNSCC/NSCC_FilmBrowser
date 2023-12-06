using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Page homePage = new Pages.Home();
        Page movieCatalogPage = new Pages.MovieCatalog(); // Add this line

        public MainWindow()
        {
            InitializeComponent();
            Frame_PageDisplay.NavigationService.Navigate(homePage);
        }

        private void Navigate_Home(object sender, RoutedEventArgs e)
        {
            Frame_PageDisplay.NavigationService.Navigate(homePage);
        }

        private void Navigate_Directors(object sender, RoutedEventArgs e)
        {
            // Implement this when we have a Directors page
            Frame_PageDisplay.Navigate(new Uri("/Pages/Directors.xaml", UriKind.Relative));
        }

        private void Navigate_Actors(object sender, RoutedEventArgs e)
        {
            // Implement this when we have an Actors page
        }

        private void Navigate_Movie_Catalog(object sender, RoutedEventArgs e)
        {
            Frame_PageDisplay.NavigationService.Navigate(movieCatalogPage);
        }
    }
}