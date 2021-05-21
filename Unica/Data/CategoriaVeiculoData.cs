using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Unica.Models;

namespace Unica.Data
{
    public class CategoriaVeiculoData : Data
    {
        public void Create(String descricaoCategoria)
        {

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;
            sqlCommand.CommandText = @"INSERT INTO categorias VALUES (@descricao)";
            sqlCommand.Parameters.AddWithValue("@descricao", descricaoCategoria);
            sqlCommand.ExecuteNonQuery();
        }

        public List<CategoriaVeiculo> Read()
        {
            List<CategoriaVeiculo> lista = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = base.DbConnection;
                sqlCommand.CommandText = @"SELECT * FROM categorias ORDER BY descricao";

                SqlDataReader reader = sqlCommand.ExecuteReader();

                lista = new List<CategoriaVeiculo>();
                while(reader.Read())
                {
                    CategoriaVeiculo categoria = new CategoriaVeiculo();

                    categoria.Codigo = (int)reader["codigo"];
                    categoria.Descricao = (string)reader["descricao"];

                    lista.Add(categoria);
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
        public void Update (CategoriaVeiculo categoria)
        {
            SqlCommand sqlCommand  = new SqlCommand();
            sqlCommand.Connection  = base.DbConnection;
            sqlCommand.CommandText = @"UPDATE categorias SET descricao = @descricao WHERE codigo = @codigo";
            sqlCommand.Parameters.AddWithValue("@codigo", categoria.Codigo);
            sqlCommand.Parameters.AddWithValue("@descricao", categoria.Descricao);
            sqlCommand.ExecuteNonQuery();
        }

        public void Delete (int codigo)
        {
            SqlCommand sqlCommand  = new SqlCommand();
            sqlCommand.Connection  = base.DbConnection;
            sqlCommand.CommandText = @"DELETE FROM categorias WHERE codigo = @codigo";
            sqlCommand.Parameters.AddWithValue("@codigo", codigo);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
