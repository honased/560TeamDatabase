using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixLibrary.DataDelegates
{
    internal class AddWatchLogDataDelegate : DataDelegate
    {
        private readonly int showID;
        private readonly int userID;

        public AddWatchLogDataDelegate(int userID, int showID)
            : base("Flix.AddWatchLog")
        {
            this.showID = showID;
            this.userID = userID;
        }

        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);
            command.Parameters.AddWithValue("UserID", userID);
            command.Parameters.AddWithValue("ShowID", showID);
            

        }
        
    }
}
