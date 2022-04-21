using DataAccess;
using NetflixLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixLibrary.DataDelegates
{
    internal class GetShowDataDelegate : DataReaderDelegate<Show>
    {
        private readonly int showID;

        public GetShowDataDelegate(int showID)
            : base("Flix.GetShow")
        {
            this.showID = showID;
        }

        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);

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

            string[] genres = reader.GetString("Genres").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            show.Genres.AddRange(genres);

            string[] cast = reader.GetString("Cast").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            show.Cast.AddRange(cast);

            show.Director = reader.GetString("Directors");

            return show;
        }
    }
}
