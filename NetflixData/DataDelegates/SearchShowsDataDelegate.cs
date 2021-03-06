using DataAccess;
using NetflixData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixData.DataDelegates
{
    internal class SearchShowsDataDelegate : DataReaderDelegate<IReadOnlyList<Show>>
    {
        private readonly int userID;
        private readonly string title;
        private readonly string director;
        private readonly int? releaseYear;
        private readonly int? genre;
        private readonly bool searchLibrary;

        public SearchShowsDataDelegate(int userID, string title, string director, int? releaseYear, int? genre, bool searchLibrary)
            : base("Flix.SearchShows")
        {
            this.userID = userID;
            this.title = title;
            this.director = director;
            this.releaseYear = releaseYear;
            this.genre = genre;
            this.searchLibrary = searchLibrary;
        }

        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);

            command.Parameters.AddWithValue("UserID", userID);
            command.Parameters.AddWithValue("Title", title);
            command.Parameters.AddWithValue("Director", director);
            
            if(releaseYear.HasValue) command.Parameters.AddWithValue("ReleaseYear", releaseYear.Value);
            else command.Parameters.AddWithValue("ReleaseYear", DBNull.Value);
            
            if (genre.HasValue) command.Parameters.AddWithValue("GenreID", genre.Value);
            else command.Parameters.AddWithValue("GenreID", DBNull.Value);

            command.Parameters.AddWithValue("SearchLibrary", searchLibrary);
        }

        public override IReadOnlyList<Show> Translate(SqlCommand command, IDataRowReader reader)
        {
            var shows = new List<Show>();

            while(reader.Read())
            {
                var show = new Show(reader.GetInt32("ShowID"));
                show.Title = reader.GetString("Title");
                show.ReleaseYear = reader.GetInt32("ReleaseYear");

                shows.Add(show);
            }

            return shows;
        }
    }
}
