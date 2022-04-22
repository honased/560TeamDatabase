using DataAccess;
using NetflixData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixData.DataDelegates
{
    internal class GetMyFavoriteShowsDataDelegate : DataReaderDelegate<IReadOnlyList<Show>>
    {
        private readonly int userID;

        public GetMyFavoriteShowsDataDelegate(int userID)
            : base("Flix.GetMyFavoriteShows")
        {
            this.userID = userID;
        }

        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);
            command.Parameters.AddWithValue("UserID", userID);
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
