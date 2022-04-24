// View for controlling a collection of pages of shows.

using NetflixData.Models;
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
    /// Interaction logic for PageControl.xaml
    /// </summary>
    public partial class PageControl : UserControl
    {
        public PageControl()
        {
            InitializeComponent();
            LeftButton.IsEnabled = false;
            RightButton.IsEnabled = false;
            PageText.IsEnabled = false;
        }

        /// <summary>
        /// On enter, attempt to set the page to the given input. If the
        /// input is not a number, a message box is displayed.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event argumnts</param>
        private void CheckEnter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && DataContext is PaginatedShows ps)
            {
                if (int.TryParse(PageText.Text, out int page))
                {
                    ps.Page = page;
                    PageText.Text = ps.PageDisplay;
                    LeftButton.IsEnabled = ps.Page > 1;
                    RightButton.IsEnabled = ps.Page < ps.PageCount;
                }
                else MessageBox.Show("Error! Please enter a valid number");
            }
        }

        /// <summary>
        /// Refresh the pagination display when the data context changes.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(DataContext is PaginatedShows ps)
            {
                PageText.Text = ps.PageDisplay;
                LeftButton.IsEnabled = false;
                RightButton.IsEnabled = ps.PageCount > 1;
                PageText.IsEnabled = true;
            }
            else
            {
                LeftButton.IsEnabled = false;
                RightButton.IsEnabled = false;
                PageText.IsEnabled = false;
            }
        }

        /// <summary>
        /// When the left button is pressed, try to decrement
        /// the page. Disable/enable the left and right buttons
        /// accordingly.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void LeftClick(object sender, RoutedEventArgs e)
        {
            if(DataContext is PaginatedShows ps)
            {
                ps.Page -= 1;
                LeftButton.IsEnabled = ps.Page > 1;
                RightButton.IsEnabled = ps.Page < ps.PageCount;
                PageText.Text = ps.PageDisplay;
            }
        }

        /// <summary>
        /// When the right button is pressed, try to increment
        /// the page. Disable/enable the left and right buttons
        /// accordingly.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void RightClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PaginatedShows ps)
            {
                ps.Page += 1;
                LeftButton.IsEnabled = ps.Page > 1;
                RightButton.IsEnabled = ps.Page < ps.PageCount;
                PageText.Text = ps.PageDisplay;
            }
        }
    }
}
