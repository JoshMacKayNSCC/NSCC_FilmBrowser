using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Pages;
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
    /// Interaction logic for Actors.xaml
    /// </summary>
    public partial class Actors : Page
    {

        ImdbContext _context = new ImdbContext();
        CollectionViewSource actorsViewSource = new CollectionViewSource();

        public Actors()
        {
            InitializeComponent();
            actorsViewSource = (CollectionViewSource)FindResource(nameof(actorsViewSource));
            _context.Names.Take(1000).Load();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //ignore case by normalising to lower
            string searchTerm = SearchBox.Text.ToLower();
            var catalogueQuery = from name in _context.Names.Local
                                 where name.PrimaryName.ToLower().Contains(searchTerm) && name.PrimaryProfession.ToLower().Contains("actor")
                                 group name by name.PrimaryName.ToUpper().Substring(0, 1) into actorGroup
                                 select new
                                 {
                                     Index = actorGroup.Key,
                                     actorCount = actorGroup.Count().ToString(),
                                     actor = actorGroup.ToList<Name>()
                                 };

        
            actorsListView.ItemsSource = catalogueQuery.ToList();
        }

    }
}

