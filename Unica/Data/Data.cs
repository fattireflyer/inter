﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace Unica.Data
{
    public abstract class Data : IDisposable
    {
        protected SqlConnection DbConnection;

        protected Data()
        {
            try
            {
                string strConexao = @"Data Source = localhost;
                            Initial Catalog = bdecommerce;
                            Integrated Security = false;
                            User Id = sa;
                            Password = A1b2c3d4e5!";

                DbConnection = new SqlConnection(strConexao);

                DbConnection.Open();
            }
            catch (SqlException ex)
            {
               
                StringBuilder errorMessages = new StringBuilder();

                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());

           
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
