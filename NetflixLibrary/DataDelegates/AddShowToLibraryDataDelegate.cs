using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data.SqlClient;

namespace NetflixLibrary.DataDelegates
{
    public class AddShowToLibraryDataDelegate : DataDelegate
    {
        private readonly int userID;
        private readonly int showID;

        public AddShowToLibraryDataDelegate(int userID, int showID) : base("Flix.AddShowToLibrary")
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
