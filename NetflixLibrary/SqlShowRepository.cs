﻿using DataAccess;
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
        private static SqlCommandExecutor executor = new SqlCommandExecutor(connectionString);

        public static IReadOnlyList<Show> SearchShows(int userID, string title, string director, int? releaseYear, int? genreID, bool inLibrary)
        {
            var d = new SearchShowsDataDelegate(userID, title, director, releaseYear, genreID, inLibrary);
            return executor.ExecuteReader(d);
        }

        public static void ReviewShow(int userID, int showID, int review)
        {
            var d = new ReviewShowDataDelegate(userID, showID, review);
            executor.ExecuteNonQuery(d);
        }
    }
}
