using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FinalProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows.Media;
using System.Collections.Generic;
using FinalProject.Models;

namespace FinalProject.Pages
{
    public partial class MovieCatalog : Page
    {
        public ImdbContext DbContext { get; set; }
        private List<Title> allTitles; // To store all titles

        public MovieCatalog()
        {
            InitializeComponent();
            DbContext = new ImdbContext();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Load data when the page is fully loaded
            allTitles = DbContext.Titles.Include(t => t.Rating).ToList();
            MoviesListView.ItemsSource = allTitles; // Initially display all titles
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = ((TextBox)sender).Text.ToLower();
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var filteredTitles = allTitles
                    .Where(t => t.PrimaryTitle.ToLower().Contains(searchQuery))
                    .ToList();

                MoviesListView.ItemsSource = filteredTitles;
            }
            else
            {
                MoviesListView.ItemsSource = allTitles; // Reset to original list
            }
        }

        private void SearchBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox != null && searchBox.Foreground == Brushes.Gray && searchBox.Text == "Search...")
            {
                searchBox.Text = string.Empty;
                searchBox.Foreground = SystemColors.ControlTextBrush;
            }
        }

        private void SearchBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox != null && string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = "Search...";
                searchBox.Foreground = Brushes.Gray;
            }
        }
    }
}
