using System;
using FinalProject.Models;
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
using System.IO;
using FinalProject.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;

namespace FilmBrowser
{
    public partial class HomeDisplay : UserControl
    {
        public ImdbContext context;
        public HomeDisplay()
        {
            InitializeComponent();
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;
        }

        public HomeDisplay(string category, IQueryable<String> q)
        {
            InitializeComponent();
            context = new ImdbContext();
            TB_Category.Text = category;
            
            foreach (string query_result in q) {
                TextBlock tb = new TextBlock();
                tb.Text = query_result;
                tb.Style = (Style)FindResource(resourceKey: "Home_QueryResult");
                StackPanel_QueryResults.Children.Add(tb);
            }
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
