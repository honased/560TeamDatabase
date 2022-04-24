// View for displaying all interesting user statistics.

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
    /// Interaction logic for UserStats.xaml
    /// </summary>
    public partial class UserStats : UserControl
    {
        public UserStats()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the view to display the new Favorite Shows,
        /// Most Watched Shows, and Top Genres.
        /// </summary>
        public void Refresh()
        {
            FavoriteShows.DataContext = new PaginatedShows(SqlNetflixRepository.GetMyFavoriteShows(SqlNetflixRepository.LoggedInUserID));
            MostWatchedShows.DataContext = new PaginatedShows(SqlNetflixRepository.GetMyMostWatchedShows(SqlNetflixRepository.LoggedInUserID));
            TopGenres.ItemsSource = null;
            TopGenres.ItemsSource = SqlNetflixRepository.GetTopGenres(SqlNetflixRepository.LoggedInUserID);
        }
    }
}
