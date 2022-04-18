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

            SqlShowRepository.Initialize();

            var list = new List<Show>();
            for(int i = 0; i < 20; i++)
            {
                list.Add(new Show() { IsMovie = i % 2 == 0, Title = $"{i}", ReleaseYear = 1994 + i, AgeRating = "PG-13" });
            }
            DataContext = list;
        }

        private void ShowLibrary_ShowClicked(object sender, RoutedEventArgs e)
        {
            if(sender is UserControl uc && uc.DataContext is Show s)
            {
                e.Handled = true;
                ShowInfo.DataContext = s;
            }
        }
    }
}
