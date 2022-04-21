﻿using NetflixLibrary.Models;
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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
        private SearchBar.SearchEventArgs lastSearch;

        public Search()
        {
            InitializeComponent();

            lastSearch = new SearchBar.SearchEventArgs();
            lastSearch.Director = "";
            lastSearch.ReleaseYear = "";
            lastSearch.Title = "";
        }

        private void SearchBar_OnSearch(object sender, SearchBar.SearchEventArgs e)
        {
            int? releaseYear;

            if (e.ReleaseYear.Trim() == "") releaseYear = null;
            else
            {
                if (int.TryParse(e.ReleaseYear, out int actualYear))
                {
                    releaseYear = actualYear;
                }
                else
                {
                    MessageBox.Show("Error! Please either leave release year blank or use a number!");
                    return;
                }
            }

            Display.DataContext = new PaginatedShows(SqlShowRepository.SearchShows(1, e.Title, e.Director, releaseYear, null, false));
            lastSearch = e;
        }

        private void Display_ShowClicked(object sender, RoutedEventArgs e)
        {
            if (sender is UserControl uc && uc.DataContext is Show s)
            {
                e.Handled = true;
                ShowInfo.DataContext = s;
            }
        }

        public void Refresh()
        {
            //SearchBar_OnSearch(this, lastSearch);
        }
    }
}