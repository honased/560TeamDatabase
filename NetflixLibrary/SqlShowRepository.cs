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
            var shows = executor.ExecuteReader(d);
            foreach(Show s in shows)
            {
                s.InLibrary = inLibrary;
            }
            return shows;
        }

        public static Show GetShow(int showID)
        {
            var d = new GetShowDataDelegate(showID);
            return executor.ExecuteReader(d);
        }

        public static void PopulateShowInfo(Show show)
        {
            Show s = GetShow(show.ShowID);

            show.IsMovie = s.IsMovie;
            show.AgeRating = s.AgeRating;
            show.Genres = s.Genres;
            show.Cast = s.Cast;
            show.Director = s.Director;
        }

        public static void ReviewShow(int userID, int showID, int review)
        {
            var d = new ReviewShowDataDelegate(userID, showID, review);
            executor.ExecuteNonQuery(d);
        }

        public static void AddShowToLibrary(int userID, int showID)
        {
            var d = new AddShowToLibraryDataDelegate(userID, showID);
            executor.ExecuteNonQuery(d);
        }

        public static void RemoveShowFromLibrary(int userID, int showID)
        {
            var d = new RemoveShowFromLibraryDataDelegate(userID, showID);
            executor.ExecuteNonQuery(d);
        }

        public static bool LoginUser(string username)
        {
            var d = new LoginUserDataDelegate(username);
            return executor.ExecuteReader(d);
        }

        public static bool RegisterUser(string username)
        {
            var d = new RegisterUserDataDelegate(username);
            return executor.ExecuteReader(d);
        }

        public static void AddWatchLog(int UserID, int showID)
        {
            var d = new AddWatchLogDataDelegate(UserID, showID);
            executor.ExecuteNonQuery(d);
        }
    }
}
