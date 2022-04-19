using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NetflixLibrary.DataDelegates
{
    internal class RegisterUserDataDelegate : DataReaderDelegate<bool>
    {
        private readonly string username;
        public RegisterUserDataDelegate(string username)
            : base("Flix.RegisterUser")
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
