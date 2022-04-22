using DataAccess;
using NetflixData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixData.DataDelegates
{
    internal class GetShowDataDelegate : DataReaderDelegate<Show>
    {
        private readonly int userID;
        private readonly int showID;

        public GetShowDataDelegate(int userID, int showID)
            : base("Flix.GetShow")
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

        public override Show Translate(SqlCommand command, IDataRowReader reader)
        {
            if (!reader.Read()) throw new Exception("Could not find show");

            var show = new Show(reader.GetInt32("ShowID"));
            show.Title = reader.GetString("Title");
            show.ReleaseYear = reader.GetInt32("ReleaseYear");
            show.IsMovie = reader.GetValue<bool>("IsMovie");
            show.AgeRating = reader.GetString("AgeRating");

            show.Genres = reader.GetString("Genres").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            show.Cast = reader.GetString("Cast").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            show.Director = reader.GetString("Directors");

            if (reader.IsDBNull("MyReview")) show.MyReview = null;
            else show.MyReview = reader.GetInt32("MyReview");

            if (reader.IsDBNull("AverageReview")) show.AverageReview = null;
            else show.AverageReview = reader.GetValue<double>("AverageReview");

            return show;
        }
    }
}
