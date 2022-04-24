// View that displays a collection of shows as ShowBoxes.

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
    /// Interaction logic for ShowLibrary.xaml
    /// </summary>
    public partial class ShowDisplay : UserControl
    {
        public event RoutedEventHandler ShowClicked;

        public ShowDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoke the event ShowClicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBoxClicked(object sender, RoutedEventArgs e)
        {
            if(sender is ShowBox sb)
            {
                ShowClicked?.Invoke(sender, e);
            }
        }
    }
}
