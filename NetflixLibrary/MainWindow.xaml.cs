// View for showing the LoginScreen and Main application.
// Handles swapping between views and logging in.

using NetflixData;
using NetflixData.Models;
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

namespace NetflixLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CancelRefresh = false;
        }

        /// <summary>
        /// Prevents a refresh from ocurring on the Search Tab
        /// </summary>
        public bool CancelRefresh { get; set; }

        /// <summary>
        /// Attempts to login or register using data from the login page.
        /// </summary>
        /// <param name="sender">The login page</param>
        /// <param name="e">The LoginEvent Arguments</param>
        private void OnLogin(object sender, Views.LoginScreen.LoginEventArgs e)
        {
            var username = e.Username.Trim();
            if (username == "")
            {
                MessageBox.Show("Error! Please enter in a valid username.");
                return;
            }

            if(!e.IsRegister)
            {
                if (SqlNetflixRepository.LoginUser(username))
                {
                    Login.Visibility = Visibility.Collapsed;
                    Application.Visibility = Visibility.Visible;
                    library.ClearLastSearch();
                    search.ClearLastSearch();
                    library.Refresh();
                    search.Refresh();
                    MainTabs.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show($"Error! The username '{username}' was not found.");
                }
            }
            else
            {
                if (SqlNetflixRepository.RegisterUser(username))
                {
                    Login.Visibility = Visibility.Collapsed;
                    Application.Visibility = Visibility.Visible;
                    library.ClearLastSearch();
                    search.ClearLastSearch();
                    library.Refresh();
                    search.Refresh();
                    MainTabs.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show($"Error! The username '{username}' is already taken.");
                }
            }
        }

        /// <summary>
        /// Signs out the user and returns to the login page.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The routed event arguments</param>
        private void SignOut(object sender, RoutedEventArgs e)
        {
            Application.Visibility = Visibility.Collapsed;
            Login.Visibility = Visibility.Visible;
            lgScreen.Clear();
        }

        /// <summary>
        /// Refresh the library whenever the library tab is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LibrarySelected(object sender, RoutedEventArgs e)
        {
            library.Refresh();
        }

        /// <summary>
        /// Refresh the search page whenenver the search tab is selected.
        /// If CancelRefresh is true, then the refresh is cancelled for
        /// this call.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The routed event arguments</param>
        private void SearchSelected(object sender, RoutedEventArgs e)
        {
            if (!CancelRefresh)
            {
                search.Refresh();
            }
            else CancelRefresh = false;
        }

        /// <summary>
        /// Refresh the user stats page whenever the user stats tab is selected.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The routed event arguments</param>
        private void UserStatsSelected(object sender, RoutedEventArgs e)
        {
            userStats.Refresh();
        }
    }
}
