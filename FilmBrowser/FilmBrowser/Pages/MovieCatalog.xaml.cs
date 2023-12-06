using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FinalProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FinalProject.Models;
using System.Threading.Tasks; // Added for Task

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData(); // Load initial data
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the search text on the UI thread
            var searchQuery = SearchBox.Text;

            // Show the progress bar and disable the search button
            SearchProgressBar.Visibility = Visibility.Visible;
            SearchProgressBar.IsIndeterminate = true;
            SearchButton.IsEnabled = false;

            // Perform the search operation asynchronously
            await Task.Run(() => LoadData(searchQuery));

            // After the search is complete, update the UI on the UI thread
            Dispatcher.Invoke(() =>
            {
                // Update the ListView's ItemsSource
                MoviesListView.ItemsSource = _movieGroups;

                // Hide the progress bar and re-enable the search button
                SearchProgressBar.Visibility = Visibility.Collapsed;
                SearchProgressBar.IsIndeterminate = false;
                SearchButton.IsEnabled = true;
            });
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

            // The UI update has been moved to the Dispatcher.Invoke in the SearchButton_Click method
        }
    }

    public class MovieGroup
    {
        public string GroupHeader { get; set; }
        public List<Title> Movies { get; set; }
    }
}
