using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixLibrary.DataDelegates
{
    internal class LoginUserDataDelegate : DataReaderDelegate<bool>
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

            command.Parameters.AddWithValue("username", username);
            
        }
        public override bool Translate(SqlCommand command, IDataRowReader reader)
        {
            return reader.Read();

        }
    }
}
