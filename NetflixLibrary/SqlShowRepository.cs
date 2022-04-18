using DataAccess;
using NetflixLibrary.DataDelegates;
using NetflixLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetflixLibrary
{
    public static class SqlShowRepository
    {
        private const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=DB560;Integrated Security=SSPI;";
        private static SqlCommandExecutor executor;

        public static void Initialize()
        {
            executor = new SqlCommandExecutor(connectionString);
        }

        public static IReadOnlyList<Show> SearchShows(int userID, string title, int? releaseYear)
        {
            var d = new SearchShowsDataDelegate(userID, title, releaseYear);
            return executor.ExecuteReader(d);
        }
    }
}
