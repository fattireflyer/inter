using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Unica.Models;

namespace Unica.Data
{
    public class TipoVeiculoData : Data
    {
        public void Create(String descricaoTipo)
        {

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;
            sqlCommand.CommandText = @"INSERT INTO tipos VALUES (@descricao)";
            sqlCommand.Parameters.AddWithValue("@descricao", descricaoTipo);
            sqlCommand.ExecuteNonQuery();
        }

        public List<TipoVeiculo> Read()
        {
            List<TipoVeiculo> lista = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = base.DbConnection;
                sqlCommand.CommandText = @"SELECT * FROM tipos ORDER BY descricao";

                SqlDataReader reader = sqlCommand.ExecuteReader();

                lista = new List<TipoVeiculo>();
                while(reader.Read())
                {
                    TipoVeiculo tipo = new TipoVeiculo();

                    tipo.Id = (int)reader["id"];
                    tipo.Descricao = (string)reader["descricao"];

                    lista.Add(tipo);
                }
            }
            catch(SqlException ex)
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
            return lista;
        }
        public void Update (TipoVeiculo tipo)
        {
            SqlCommand sqlCommand  = new SqlCommand();
            sqlCommand.Connection  = base.DbConnection;
            sqlCommand.CommandText = @"UPDATE tipos SET descricao = @descricao WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@id", tipo.Id);
            sqlCommand.Parameters.AddWithValue("@descricao", tipo.Descricao);
            sqlCommand.ExecuteNonQuery();
        }

        public void Delete (int id)
        {
            SqlCommand sqlCommand  = new SqlCommand();
            sqlCommand.Connection  = base.DbConnection;
            sqlCommand.CommandText = @"DELETE FROM tipos WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
