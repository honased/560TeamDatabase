using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using NetflixLibrary.Models;

namespace NetflixLibrary.DataDelegates
{
    public class GetSimilarShowsDataDelegate : DataReaderDelegate<IReadOnlyList<Show>>
    {
        private readonly int userID;
        private readonly int showID;

        public GetSimilarShowsDataDelegate(int userID, int showID) : base("Flix.GetSimilarShows")
        {
            this.userID = userID;
            this.showID = showID;
        }

        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);

            command.Parameters.AddWithValue("UserID", userID);
            command.Parameters.AddWithValue("ShowID", showID);
        }

        public override IReadOnlyList<Show> Translate(SqlCommand command, IDataRowReader reader)
        {
            var shows = new List<Show>();

            while (reader.Read())
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
