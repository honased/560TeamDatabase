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
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : UserControl
    {
        public class LoginEventArgs
        {
            public bool IsRegister { get; set; }
            public string Username { get; set; }
        }

        public event EventHandler<LoginEventArgs> OnLogin;

        public LoginScreen()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            OnLogin?.Invoke(this, new LoginEventArgs() { IsRegister = false, Username = UsernameText.Text });
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            OnLogin?.Invoke(this, new LoginEventArgs() { IsRegister = true, Username = UsernameText.Text });
        }

        public void Clear()
        {
            UsernameText.Clear();
        }
    }
}
