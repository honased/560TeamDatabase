// View for leaving reviews on shows

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
    /// Interaction logic for ReviewBar.xaml
    /// </summary>
    public partial class ReviewBar : UserControl
    {
        private RadioButton[] buttons;

        public event EventHandler<int> OnReview;

        public ReviewBar()
        {
            InitializeComponent();

            buttons = new RadioButton[] { RButton1, RButton2, RButton3, RButton4, RButton5 };

            foreach(RadioButton b in buttons) b.Checked += RadioChecked;
        }

        /// <summary>
        /// Invokes the review event with the review as the event argument.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void RadioChecked(object sender, RoutedEventArgs e)
        {
            if(sender is RadioButton b)
            {
                int i;
                for (i = 0; i < buttons.Length; i++) if (buttons[i] == b) break;
                if (i == buttons.Length) throw new Exception("Could not find radio button");
                OnReview?.Invoke(this, i + 1);
            }
        }

        /// <summary>
        /// Sets the review to be displayed in the radio button
        /// </summary>
        /// <param name="review">The review between [1..5]</param>
        public void SetReview(int? review)
        {
            if(review.HasValue)
            {
                buttons[review.Value - 1].IsChecked = true;
            }
            else
            {
                foreach (RadioButton b in buttons) b.IsChecked = false;
            }
        }

        /// <summary>
        /// Removes the RadioChecked event
        /// </summary>
        ~ReviewBar()
        {
            foreach (RadioButton b in buttons) b.Checked -= RadioChecked;
        }
    }
}
