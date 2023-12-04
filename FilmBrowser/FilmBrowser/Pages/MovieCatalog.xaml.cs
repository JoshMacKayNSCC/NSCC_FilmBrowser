using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FinalProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FinalProject.Models;

namespace FinalProject.Pages
{
    public partial class MovieCatalog : Page
    {
        public ImdbContext DbContext { get; set; }
        private List<MovieGroup> _movieGroups;

        public MovieCatalog()
        {
            InitializeComponent();
            DbContext = new ImdbContext();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadData(); // Load initial data
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData(SearchBox.Text);
        }

        private void LoadData(string searchQuery = null)
        {
            var query = DbContext.Titles.Include(t => t.Rating).AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(t => t.PrimaryTitle.ToLower().Contains(searchQuery.ToLower()));
            }

            _movieGroups = query.ToList()
                .GroupBy(t => t.PrimaryTitle.Substring(0, 1))
                .OrderBy(g => g.Key)
                .Select(g => new MovieGroup
                {
                    GroupHeader = g.Key + " (" + g.Count() + " movies)",
                    Movies = g.ToList()
                }).ToList();

            MoviesListView.ItemsSource = _movieGroups;
        }
    }

    public class MovieGroup
    {
        public string GroupHeader { get; set; }
        public List<Title> Movies { get; set; }
    }
}
