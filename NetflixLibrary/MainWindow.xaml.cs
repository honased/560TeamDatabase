using NetflixLibrary.Models;
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
        }

        private void LoginScreen_OnLogin(object sender, Views.LoginScreen.LoginEventArgs e)
        {
            var username = e.Username.Trim();
            if (username == "")
            {
                MessageBox.Show("Error! Please enter in a valid username.");
                return;
            }

            if(!e.IsRegister)
            {
                if (SqlShowRepository.LoginUser(username))
                {
                    Login.Visibility = Visibility.Collapsed;
                    Application.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show($"Error! The username '{username}' was not found.");
                }
            }
            else
            {
                if (SqlShowRepository.RegisterUser(username))
                {
                    Login.Visibility = Visibility.Collapsed;
                    Application.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show($"Error! The username '{username}' is already taken.");
                }
            }
        }
    }
}
