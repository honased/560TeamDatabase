using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixLibrary.DataDelegates
{
    internal class LoginUserDataDelegate : DataReaderDelegate<int?>
    {
        private readonly string username;
        public LoginUserDataDelegate(string username)
            : base("Flix.LoginUser")
        {
            this.username = username;
        }

        public override void PrepareCommand(SqlCommand command)
        {
            base.PrepareCommand(command);

            command.Parameters.AddWithValue("Username", username);
        }
        public override int? Translate(SqlCommand command, IDataRowReader reader)
        {
            if(reader.Read())
            {
                return reader.GetInt32("UserID");
            }

            return null;
        }
    }
}
