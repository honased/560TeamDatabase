// View for displaying a search bar, show info, and show display. Used for
// searching through all shows in the database that are not in a User's library.

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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
        private SearchBar.SearchEventArgs lastSearch;

        public Search()
        {
            InitializeComponent();

            lastSearch = new SearchBar.SearchEventArgs();
            ShowInfo.SimilarShowsBtn.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Attempts to perform a search and populate the show display
        /// based on the search fields.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void OnSearch(object sender, SearchBar.SearchEventArgs e)
        {
            int? releaseYear;

            if (e.ReleaseYear.Trim() == "") releaseYear = null;
            else
            {
                if (int.TryParse(e.ReleaseYear, out int actualYear))
                {
                    releaseYear = actualYear;
                }
                else
                {
                    MessageBox.Show("Error! Please either leave release year blank or use a number!");
                    return;
                }
            }

            SetShows(SqlNetflixRepository.SearchShows(SqlNetflixRepository.LoggedInUserID, e.Title, e.Director, releaseYear, e.GenreID, false));

            lastSearch = e;
        }

        /// <summary>
        /// Updates the show display and page control to use
        /// the new shows collection.
        /// </summary>
        /// <param name="shows">Collection of shows</param>
        public void SetShows(IReadOnlyList<Show> shows)
        {
            Display.DataContext = new PaginatedShows(shows);
            PageControl.DataContext = Display.DataContext;
        }

        /// <summary>
        /// Sets the show to be displayed in the showinfo pane.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void ShowClicked(object sender, RoutedEventArgs e)
        {
            if (sender is UserControl uc && uc.DataContext is Show s)
            {
                e.Handled = true;
                ShowInfo.DataContext = s;
            }
        }

        /// <summary>
        /// Clears the search bar
        /// </summary>
        public void ClearLastSearch()
        {
            searchBar.ClearOut();
            lastSearch = new SearchBar.SearchEventArgs();
        }

        /// <summary>
        /// Refreshes the view completely.
        /// </summary>
        public void Refresh()
        {
            int savedPage = 1;
            if (PageControl.DataContext is PaginatedShows ps) savedPage = ps.Page;
            OnSearch(this, lastSearch);
            ShowInfo.DataContext = null;
            if (PageControl.DataContext is PaginatedShows ps2) ps2.Page = savedPage;
        }
    }
}
