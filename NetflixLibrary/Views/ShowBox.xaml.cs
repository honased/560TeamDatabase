// View that displays a show as a nice show box

using NetflixData;
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
    /// Interaction logic for Show.xaml
    /// </summary>
    public partial class ShowBox : UserControl
    {
        public static readonly RoutedEvent ClickedEvent = EventManager.RegisterRoutedEvent(
            name: "Clicked",
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedEventHandler),
            ownerType: typeof(ShowBox)
        );

        /// <summary>
        /// Event for being clicked.
        /// </summary>
        public event RoutedEventHandler Clicked
        {
            add { AddHandler(ClickedEvent, value); }
            remove { RemoveHandler(ClickedEvent, value); }
        }

        public ShowBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Further populates the show info and invokes the 
        /// Clicked event.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void OnLeftClick(object sender, MouseButtonEventArgs e)
        {
            if(DataContext is Show s)
            {
                SqlNetflixRepository.PopulateShowInfo(SqlNetflixRepository.LoggedInUserID, s);

                RoutedEventArgs routedEventArgs = new RoutedEventArgs(ClickedEvent);
                RaiseEvent(routedEventArgs);
            }
        }

        /// <summary>
        /// Adds or removes the show from the library based on
        /// if it is in the library or not.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void AddRemoveLibrary(object sender, RoutedEventArgs e)
        {
            if(DataContext is Show s)
            {
                if (!s.InLibrary) SqlNetflixRepository.AddShowToLibrary(SqlNetflixRepository.LoggedInUserID, s.ShowID);
                else SqlNetflixRepository.RemoveShowFromLibrary(SqlNetflixRepository.LoggedInUserID, s.ShowID);

                s.InLibrary = !s.InLibrary;
            }
        }
    }
}
