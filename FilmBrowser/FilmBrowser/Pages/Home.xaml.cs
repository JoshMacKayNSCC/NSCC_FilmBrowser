using FilmBrowser;
using FinalProject.Data;
using FinalProject.Models;
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

namespace FinalProject.Pages
{
    public partial class Home : Page
    {
        int resultAmount = 3;
        public ImdbContext context;
        public Home()
        {
            InitializeComponent();
            context = new ImdbContext();

            // Most recent movies
            var new_releases =
                (from title in context.Titles
                 where title.TitleType == "movie"
                 orderby title.StartYear descending
                 select title.PrimaryTitle).Take(resultAmount);

            // Top rated movies (with at least 500 votes)
            var top_rated =
                (from title in context.Titles
                 join rating in context.Ratings on title.TitleId equals rating.TitleId
                 where rating.NumVotes >= 500 && title.TitleType == "movie"
                 orderby rating.AverageRating descending
                 select title.PrimaryTitle).Take(resultAmount);

            HomeDisplay hd1 = new HomeDisplay("Newest Movie Releases", new_releases);
            HomeDisplay hd2 = new HomeDisplay("Top Rated Movies", top_rated);
            HomeDisplay hd3 = new HomeDisplay();
            Grid.SetRow(hd1, 0);
            Grid.SetColumn(hd1, 0);
            Grid.SetRow(hd2, 0);
            Grid.SetColumn(hd2, 1);
            Grid.SetRow(hd3, 1);
            Grid.SetColumn(hd3, 0);

            Grid_Display.Children.Add(hd1);
            Grid_Display.Children.Add(hd2);
            Grid_Display.Children.Add(hd3);

        }
    }
}
