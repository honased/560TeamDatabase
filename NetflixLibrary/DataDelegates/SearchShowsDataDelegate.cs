using DataAccess;
using NetflixLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixLibrary.DataDelegates
{
    internal class SearchShowsDataDelegate : DataReaderDelegate<IReadOnlyList<Show>>
    {
        private readonly int userID;
        private readonly string title;
        private readonly int? releaseYear;

        public SearchShowsDataDelegate(int userID, string title, int? releaseYear)
            : base("Flix.SearchShows")
        {
            this.userID = userID;
            this.title = title;
            this.releaseYear = releaseYear;
        }

        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);

            command.Parameters.AddWithValue("UserID", userID);
            command.Parameters.AddWithValue("Title", title);
            if(releaseYear.HasValue) command.Parameters.AddWithValue("ReleaseYear", releaseYear.Value);
            else command.Parameters.AddWithValue("ReleaseYear", DBNull.Value);
        }

        public override IReadOnlyList<Show> Translate(SqlCommand command, IDataRowReader reader)
        {
            var shows = new List<Show>();

            while(reader.Read())
            {
                shows.Add(new Show() { Title = reader.GetString("Title") });
            }

            return shows;
        }
    }
}
