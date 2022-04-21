using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NetflixLibrary.Models
{
    public class Show : INotifyPropertyChanged
    {
        public int ShowID { get; }
        public string Title { get; set; }
        public bool IsMovie { get; set; }
        public int ReleaseYear { get; set; }
        public string AgeRating { get; set; }
        public string Director { get; set; }

        private bool _inLibrary;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool InLibrary
        {
            get => _inLibrary;
            set
            {
                bool notifyChanged = _inLibrary != value;
                _inLibrary = value;
                if (notifyChanged) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InLibraryString"));
            }
        }

        public List<string> Genres { get; set; }

        public List<string> Cast { get; set; }

        public string MovieString => IsMovie ? "Movie" : "TV Show";
        public string ReleasedString => $"{ReleaseYear}";
        public string InLibraryString => InLibrary ? "Remove" : "Add";

        public string GenresString
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                for(int i = 0; i < Genres.Count; i++)
                {
                    if (i < Genres.Count - 1) builder.Append(Genres[i] + ", ");
                    else builder.Append(Genres[i]);
                }

                return builder.ToString().Trim();
            }
        }

        public Show(int showID)
        {
            ShowID = showID;
            Cast = new List<string>();
            Genres = new List<string>();
        }
    }
}
