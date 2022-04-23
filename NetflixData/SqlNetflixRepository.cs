using DataAccess;
using NetflixData.DataDelegates;
using NetflixData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetflixData
{
    public static class SqlNetflixRepository
    {
        private const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=DB560;Integrated Security=SSPI;";
        private static SqlCommandExecutor executor = new SqlCommandExecutor(connectionString);

        public static int LoggedInUserID { get; private set; }

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

        public static Show GetShow(int userID, int showID)
        {
            var d = new GetShowDataDelegate(userID, showID);
            return executor.ExecuteReader(d);
        }

        public static void PopulateShowInfo(int userID, Show show)
        {
            Show s = GetShow(userID, show.ShowID);

            show.IsMovie = s.IsMovie;
            show.AgeRating = s.AgeRating;
            show.Genres = s.Genres;
            show.Cast = s.Cast;
            show.Director = s.Director;
            show.MyReview = s.MyReview;
            show.AverageReview = s.AverageReview;
        }
        
        public static IReadOnlyList<Show> GetMyFavoriteShows(int userID)
        {
            var d = new GetMyFavoriteShowsDataDelegate(userID);
            return executor.ExecuteReader(d);
        }
        public static IReadOnlyList<Show> GetMyMostWatchedShows(int userID)
        {
            var d = new GetMyMostWatchedShowsDataDelegate(userID);
            return executor.ExecuteReader(d);
        }

        public static IReadOnlyList<Show> GetSimilarShows(int userID, int showID)
        {
            var d = new GetSimilarShowsDataDelegate(userID, showID);
            return executor.ExecuteReader(d);
        }

        public static IReadOnlyList<Genre> GetAllGenres()
        {
            var d = new GetAllGenresDataDelegate();
            return executor.ExecuteReader(d);
        }

        public static IReadOnlyList<Genre> GetTopGenres(int UserID)
        {
            var d = new GetTopGenresDataDelegate(UserID);
            return executor.ExecuteReader(d);
        }

        public static IReadOnlyList<WatchLog> GetWatchLogs(int userID, int showID)
        {
            var d = new GetWatchLogsDataDelegate(userID, showID);
            return executor.ExecuteReader(d);
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

        public static void RemoveWatchLog(int watchLogID)
        {
            var d = new RemoveWatchLogDataDelegate(watchLogID);
            executor.ExecuteNonQuery(d);
        }

        public static bool LoginUser(string username)
        {
            var d = new LoginUserDataDelegate(username);
            var userID = executor.ExecuteReader(d);
            if (userID.HasValue) LoggedInUserID = userID.Value;
            return userID.HasValue;
        }

        public static bool RegisterUser(string username)
        {
            var d = new RegisterUserDataDelegate(username);
            var userID = executor.ExecuteReader(d);
            if (userID.HasValue) LoggedInUserID = userID.Value;
            return userID.HasValue;
        }

        public static void AddWatchLog(int UserID, int showID)
        {
            var d = new AddWatchLogDataDelegate(UserID, showID);
            executor.ExecuteNonQuery(d);
        }
    }
}
