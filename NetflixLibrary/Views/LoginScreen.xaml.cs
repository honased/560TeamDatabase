// View for logging in and registering a user.

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
        /// <summary>
        /// The login event arguments.
        /// </summary>
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

        /// <summary>
        /// Invoke the login event with register set to false.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void LoginPressed(object sender, RoutedEventArgs e)
        {
            OnLogin?.Invoke(this, new LoginEventArgs() { IsRegister = false, Username = UsernameText.Text });
        }

        /// <summary>
        /// Invoke the login event with register set to true.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void RegisterPressed(object sender, RoutedEventArgs e)
        {
            OnLogin?.Invoke(this, new LoginEventArgs() { IsRegister = true, Username = UsernameText.Text });
        }

        /// <summary>
        /// Clear out the username field.
        /// </summary>
        public void Clear()
        {
            UsernameText.Clear();
        }

        /// <summary>
        /// Check for an enter press, and if so, trigger a login pressed event.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void CheckEnter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                LoginPressed(null, new RoutedEventArgs());
            }
        }
    }
}
