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
    public partial class ShowLibrary : UserControl
    {
        public event RoutedEventHandler ShowClicked;

        public ShowLibrary()
        {
            InitializeComponent();
        }

        private void ShowBox_Clicked(object sender, RoutedEventArgs e)
        {
            if(sender is ShowBox sb)
            {
                ShowClicked.Invoke(sender, e);
            }
        }
    }
}
