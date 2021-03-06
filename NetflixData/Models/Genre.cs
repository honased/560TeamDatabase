using System;
using System.Collections.Generic;
using System.Text;

namespace NetflixData.Models
{
    public class Genre
    {
        public int GenreID { get; }
        public string Name { get; set; }

        public Genre(int genreID)
        {
            this.GenreID = genreID;
        }

        public override string ToString()
        {
            return Name.Trim();
        }
    }
}
