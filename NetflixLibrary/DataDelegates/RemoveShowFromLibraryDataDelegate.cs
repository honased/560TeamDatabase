using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data.SqlClient;

namespace NetflixLibrary.DataDelegates
{
    public class RemoveShowFromLibraryDataDelegate : DataDelegate
    {
        private readonly int userID;
        private readonly int showID;

        public RemoveShowFromLibraryDataDelegate(int userID, int showID) : base("Flix.RemoveShowFromLibrary")
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
    }
}
