using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
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

namespace FilmBrowser.Pages
{
    /// <summary>
    /// Interaction logic for Directors.xaml
    /// </summary>
    public partial class Directors : Page
    {
        ImdbContext _context = new ImdbContext();
        CollectionViewSource directorsViewSource= new CollectionViewSource();
        public Directors()
        {
            InitializeComponent();
            directorsViewSource=(CollectionViewSource)FindResource(nameof(directorsViewSource));
            _context.Names.Take(500).Load();
           
;
        }
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            //ignore case by normalising to lower
            string searchTerm = searchBox.Text.ToLower();
            var catalogueQuery = from name in _context.Names.Local
                                 where name.PrimaryName.ToLower().Contains(searchTerm) && name.PrimaryProfession.ToLower().Contains("director")
                                 group name by name.PrimaryName.ToUpper().Substring(0, 1) into directorGroup
                                 select new
                                 {
                                     Index = directorGroup.Key,
                                     directorCount = directorGroup.Count().ToString(),
                                     director = directorGroup.ToList<Name>()
                                 };


            directorsListView.ItemsSource = catalogueQuery.ToList();

        }
    }
}
