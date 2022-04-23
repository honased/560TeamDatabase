using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using NetflixData;
using NetflixData.Models;
using System.Linq;

namespace NetflixDataTests
{
    [TestClass]
    public class ProcedureTests
    {
        private static string GetTestString() => Guid.NewGuid().ToString("N");

        private TransactionScope transaction;

        [TestInitialize]
        public void InitializeTest()
        {
            transaction = new TransactionScope();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            transaction.Dispose();
        }

        [TestMethod]
        public void GetFavoriteShowsShouldReturnCorrectShows()
        {
            var user = SqlNetflixRepository.RegisterUser(GetTestString());

            //check that when the user has not reiewed any shows there are no favorite shows returned
            var favoriteShows = SqlNetflixRepository.GetMyFavoriteShows(SqlNetflixRepository.LoggedInUserID);
            Assert.Equals(0, favoriteShows.Count);

            //check that when five shows are reviewed those shows are returned as favorite shows
            SqlNetflixRepository.ReviewShow(SqlNetflixRepository.LoggedInUserID, 50, 1);
            SqlNetflixRepository.ReviewShow(SqlNetflixRepository.LoggedInUserID, 51, 2);
            SqlNetflixRepository.ReviewShow(SqlNetflixRepository.LoggedInUserID, 52, 3);
            SqlNetflixRepository.ReviewShow(SqlNetflixRepository.LoggedInUserID, 53, 4);
            SqlNetflixRepository.ReviewShow(SqlNetflixRepository.LoggedInUserID, 54, 5);
            favoriteShows = SqlNetflixRepository.GetMyFavoriteShows(SqlNetflixRepository.LoggedInUserID);
            for (int i = 50; i <= 54; i++)
            {
                Assert.IsTrue(favoriteShows.Any(find => find.ShowID == i));
            }

            //check that only the top rated five shows are returned
            SqlNetflixRepository.ReviewShow(50, 55, 2);
            for(int i = 51; i <= 55; i++)
            {
                Assert.IsTrue(favoriteShows.Any(find => find.ShowID == i));
            }
            Assert.IsFalse(favoriteShows.Any(find => find.ShowID == 50));
        }

        [TestMethod]
        public void GetMostWatchedShowsShouldReturnCorrectShows()
        {
            var user = SqlNetflixRepository.RegisterUser(GetTestString());

            //check that when no shows are watched the most watched shows returns no shows
            var topShows = SqlNetflixRepository.GetMyMostWatchedShows(SqlNetflixRepository.LoggedInUserID);
            Assert.Equals(0, topShows.Count);
            
            //check that if five shows are watched that those five shows will be returned
            for(int i = 1; i <= 5; i++)
            {
                for(int j = 0; j < i; j++)
                {
                    SqlNetflixRepository.AddWatchLog(SqlNetflixRepository.LoggedInUserID, i);
                }
            }
            topShows = SqlNetflixRepository.GetMyMostWatchedShows(SqlNetflixRepository.LoggedInUserID);
            for(int i = 1; i <= 5; i++)
            {
                Assert.IsTrue(topShows.Any(find => find.ShowID == i));
            }
            
            //check that only the top 5 most watched shows are returned
            for(int i = 0; i < 6; i++)
            {
                SqlNetflixRepository.AddWatchLog(SqlNetflixRepository.LoggedInUserID, 6);
            }
            topShows = SqlNetflixRepository.GetMyMostWatchedShows(SqlNetflixRepository.LoggedInUserID);
            for (int i = 2; i <= 6; i++)
            {
                Assert.IsTrue(topShows.Any(find => find.ShowID == i));
            }
            Assert.IsFalse(topShows.Any(find => find.ShowID == 1));
        }

        [TestMethod]
        public void SimilarShowsShouldReturnCorrectShows()
        {
            var user = SqlNetflixRepository.RegisterUser(GetTestString());
            SqlNetflixRepository.AddShowToLibrary(SqlNetflixRepository.LoggedInUserID, 50);
            var show = SqlNetflixRepository.GetShow(SqlNetflixRepository.LoggedInUserID, 50);
            var similarShows = SqlNetflixRepository.GetSimilarShows(SqlNetflixRepository.LoggedInUserID, 50);
            Assert.IsFalse(similarShows.Any(find => find.ShowID == 50));
            var genres = show.Genres;
            foreach (Show simShow in similarShows)
            {
                foreach(string genre in genres)
                {
                    Assert.IsTrue(simShow.Genres.Contains(genre));
                }
            }
        }
    }
}
