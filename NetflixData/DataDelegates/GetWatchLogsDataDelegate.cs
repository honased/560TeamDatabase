using DataAccess;
using NetflixData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixData.DataDelegates
{
    class GetWatchLogsDataDelegate : DataReaderDelegate<IReadOnlyList<WatchLog>>
    {
        private readonly int userID;
        private readonly int showID;

        public GetWatchLogsDataDelegate(int userID, int showID)
            : base("Flix.GetWatchLogs")
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

        public override IReadOnlyList<WatchLog> Translate(SqlCommand command, IDataRowReader reader)
        {
            var logs = new List<WatchLog>();

            while(reader.Read())
            {
                var log = new WatchLog(reader.GetInt32("WatchLogID"));
                log.ShowID = reader.GetInt32("ShowID");
                log.WatchedOn = reader.GetDateTimeOffset("WatchedOn");
                logs.Add(log);
            }

            return logs;
        }
    }
}
