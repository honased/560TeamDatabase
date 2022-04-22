using NetflixData;
using NetflixData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetflixLibrary.Views
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        public class SearchEventArgs
        {
            public string Title { get; set; }
            public string Director { get; set; }
            public string ReleaseYear { get; set; }
            public int? GenreID { get; set; }

            public SearchEventArgs()
            {
                Title = "";
                Director = "";
                ReleaseYear = "";
                GenreID = null;
            }
        }

        public event EventHandler<SearchEventArgs> OnSearch;

        public SearchBar()
        {
            InitializeComponent();
            Genre.ItemsSource = SqlNetflixRepository.GetAllGenres();
        }

        private void TriggerSearch(object sender, RoutedEventArgs e)
        {
            var args = new SearchEventArgs() { Director = DirectorText.Text, ReleaseYear = ReleaseYearText.Text, Title = TitleText.Text };
            if (Genre.SelectedItem != null && Genre.SelectedItem is Genre g) args.GenreID = g.GenreID;
            OnSearch?.Invoke(this, args);
        }

        private void CheckForEnter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                TriggerSearch(this, null);
            }
        }

        public void ClearOut()
        {
            DirectorText.Clear();
            ReleaseYearText.Clear();
            TitleText.Clear();
            Genre.SelectedItem = null;
        }

        private void ClearGenre(object sender, RoutedEventArgs e)
        {
            Genre.SelectedItem = null;
        }
    }
}
