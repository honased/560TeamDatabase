using DataAccess;
using NetflixData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixData.DataDelegates
{
    internal class GetAllGenresDataDelegate : DataReaderDelegate<IReadOnlyList<Genre>>
    {
        public GetAllGenresDataDelegate()
            : base("Flix.GetAllGenres")
        {
            
        }
        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);

            
        }
        public override IReadOnlyList<Genre> Translate(SqlCommand command, IDataRowReader reader)
        {
            var Genres = new List<Genre>();

            while (reader.Read())
            {
                var genre = new Genre(reader.GetInt32("GenreID"));
                genre.Name = reader.GetString("Genre");
                
                Genres.Add(genre);
            }

            return Genres;
        }
    }
}
