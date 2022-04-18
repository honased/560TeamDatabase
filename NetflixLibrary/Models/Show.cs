using System;
using System.Collections.Generic;
using System.Text;

namespace NetflixLibrary.Models
{
    public class Show
    {
        public string Title { get; set; }
        public bool IsMovie { get; set; }
        public int ReleaseYear { get; set; }
        public string AgeRating { get; set; }
        public string Director { get; set; }
        public bool InLibrary { get; set; }

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
                foreach (string genre in Genres)
                {
                    builder.Append(genre + " ");
                }

                return builder.ToString().Trim();
            }
        }

        public Show()
        {
            Genres = new List<string>();
            Genres.Add("Horror");
            Genres.Add("Action");

            Cast = new List<string>();
            Cast.Add("Eric Honas");
            Cast.Add("Katia Coleman");
            Cast.Add("Mason Wittman");
        }
    }
}
