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

        private void ReviewBar_OnReview(object sender, int e)
        {
            if(DataContext is Show s)
            {
                SqlNetflixRepository.ReviewShow(SqlNetflixRepository.LoggedInUserID, s.ShowID, e);
                SqlNetflixRepository.PopulateShowInfo(SqlNetflixRepository.LoggedInUserID, s);
            }
        }

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
