using NetflixLibrary.Models;
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

        public event RoutedEventHandler Clicked
        {
            add { AddHandler(ClickedEvent, value); }
            remove { RemoveHandler(ClickedEvent, value); }
        }

        public ShowBox()
        {
            InitializeComponent();
        }

        private void OnLeftClick(object sender, MouseButtonEventArgs e)
        {
            if(DataContext is Show s)
            {
                SqlShowRepository.PopulateShowInfo(s);

                RoutedEventArgs routedEventArgs = new RoutedEventArgs(ClickedEvent);
                RaiseEvent(routedEventArgs);
            }
        }
    }
}
