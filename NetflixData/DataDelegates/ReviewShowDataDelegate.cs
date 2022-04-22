using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixData.DataDelegates
{
    public class ReviewShowDataDelegate : DataDelegate
    {
        private readonly int userID;
        private readonly int showID;
        private readonly int review;

        public ReviewShowDataDelegate(int userID, int showID, int review)
            : base("Flix.ReviewShow")
        {
            this.userID = userID;
            this.showID = showID;
            this.review = review;
        }

        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);

            command.Parameters.AddWithValue("UserID", userID);
            command.Parameters.AddWithValue("ShowID", showID);
            command.Parameters.AddWithValue("Review", review);
        }
    }
}
