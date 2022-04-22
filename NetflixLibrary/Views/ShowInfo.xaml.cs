using NetflixLibrary.Models;
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
            }
            else
            {
                dockPanel.Visibility = Visibility.Hidden;
            }
        }
    }
}
