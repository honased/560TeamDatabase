using System;
using System.Collections.Generic;
using System.Text;

namespace NetflixLibrary.Data
{
    public class Show
    {
        public string Title { get; set; }
        public bool IsMovie { get; set; }
        public int ReleaseYear { get; set; }
        public string AgeRating { get; set; }

        public string MovieString => IsMovie ? "Movie" : "TV Show";
    }
}
