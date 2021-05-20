using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Unica.Models;
namespace Unica.Data
{
    public class ClienteData : Data
    {
        public void Create(Cliente cliente)
        {

            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = base.DbConnection;

            sqlCommand.CommandText = @"INSERT INTO pessoas VALUES (@ ";


        }
    }
}
