// View that displays all show info such as reviews, title,
// IsMovie, watchlog, etc.

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
    /// Interaction logic for ShowInfo.xaml
    /// </summary>
    public partial class ShowInfo : UserControl
    {
        public ShowInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On the user choosing a review, the review is
        /// added for the given show.
        /// </summary>
        /// <param name="sender">The review bar</param>
        /// <param name="e">The new review</param>
        private void OnReview(object sender, int e)
        {
            if(DataContext is Show s)
            {
                SqlNetflixRepository.ReviewShow(SqlNetflixRepository.LoggedInUserID, s.ShowID, e);
                SqlNetflixRepository.PopulateShowInfo(SqlNetflixRepository.LoggedInUserID, s);
            }
        }

        /// <summary>
        /// Update the view fields accordingly when the data
        /// context changes.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(DataContext is Show s)
            {
                reviewBar.SetReview(s.MyReview);
                dockPanel.Visibility = Visibility.Visible;
                WatchLogList.ItemsSource = null;
                WatchLogList.ItemsSource = SqlNetflixRepository.GetWatchLogs(SqlNetflixRepository.LoggedInUserID, s.ShowID);
            }
            else
            {
                dockPanel.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Remove a watch log from the user's log diary.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void RemoveLog(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if(sender is Button b && b.DataContext is WatchLog wl)
            {
                SqlNetflixRepository.RemoveWatchLog(wl.WatchLogID);
                WatchLogList.ItemsSource = null;
                WatchLogList.ItemsSource = SqlNetflixRepository.GetWatchLogs(SqlNetflixRepository.LoggedInUserID, wl.ShowID);
            }
        }

        /// <summary>
        /// Add a watch log to the user's log diary.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void AddWatchLog(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if(DataContext is Show s)
            {
                SqlNetflixRepository.AddWatchLog(SqlNetflixRepository.LoggedInUserID, s.ShowID);
                WatchLogList.ItemsSource = null;
                WatchLogList.ItemsSource = SqlNetflixRepository.GetWatchLogs(SqlNetflixRepository.LoggedInUserID, s.ShowID);
            }
        }

        /// <summary>
        /// Finds all similar shows to the currently selected show. Updates
        /// the search tab to display these shows, and gives focus to the
        /// search page.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void FindSimilarShows(object sender, RoutedEventArgs e)
        {
            if(DataContext is Show s)
            {
                var parent = Parent;

                while (parent != null)
                {
                    if (parent is MainWindow mw)
                    {
                        mw.CancelRefresh = true;
                        mw.MainTabs.SelectedIndex = 1;
                        mw.search.SetShows(SqlNetflixRepository.GetSimilarShows(SqlNetflixRepository.LoggedInUserID, s.ShowID));
                        break;
                    }
                    else parent = VisualTreeHelper.GetParent(parent);
                }
            }
        }
    }
}
