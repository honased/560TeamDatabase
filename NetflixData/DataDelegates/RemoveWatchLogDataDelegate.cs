using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data.SqlClient;

namespace NetflixData.DataDelegates
{
    public class RemoveWatchLogDataDelegate : DataDelegate
    {
        private readonly int watchLogID;

        public RemoveWatchLogDataDelegate(int watchLogID) 
            : base("Flix.RemoveWatchLog")
        {
            this.watchLogID = watchLogID;
        }

        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);

            command.Parameters.AddWithValue("WatchLogID", watchLogID);
        }
    }
}
