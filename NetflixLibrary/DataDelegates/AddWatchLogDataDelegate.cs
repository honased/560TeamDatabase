using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixLibrary.DataDelegates
{
    internal class AddWatchLogDataDelegate : DataDelegate
    {
        private readonly string username;
        public AddWatchLogDataDelegate(string username)
            : base("Flix.AddWatchLog")
        {
            this.username = username;
        }

        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);

            command.Parameters.AddWithValue("username", username);

        }
        
    }
}
