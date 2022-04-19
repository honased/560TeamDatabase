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
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        public class SearchEventArgs
        {
            public string Title { get; set; }
            public string Director { get; set; }
            public string ReleaseYear { get; set; }
        }

        public event EventHandler<SearchEventArgs> OnSearch;

        public SearchBar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnSearch?.Invoke(this, new SearchEventArgs() { Director = DirectorText.Text, ReleaseYear = ReleaseYearText.Text, Title = TitleText.Text });
        }
    }
}
