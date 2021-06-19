using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Unica.Models;

namespace Unica.Data
{
    public class VeiculoData : Data
    {
        public void Create(Veiculo veiculo)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;
            sqlCommand.CommandText = @"EXEC cadVei @placa, @descricao, @valor_diaria, @lugares, @carga, @categoria, @tipo, @status, @marca";

            sqlCommand.Parameters.AddWithValue("@placa", veiculo.Placa);
            sqlCommand.Parameters.AddWithValue("@descricao", veiculo.Descricao);
            sqlCommand.Parameters.AddWithValue("@valor_diaria", veiculo.ValorDiaria);
            sqlCommand.Parameters.AddWithValue("@lugares", veiculo.Lugares);
            sqlCommand.Parameters.AddWithValue("@carga", veiculo.Carga);
            sqlCommand.Parameters.AddWithValue("@categoria", veiculo.Categoria);
            sqlCommand.Parameters.AddWithValue("@tipo", veiculo.Tipo);
            sqlCommand.Parameters.AddWithValue("@status", veiculo.Status);
            sqlCommand.Parameters.AddWithValue("@marca", veiculo.Marca);

            sqlCommand.ExecuteNonQuery();
        }

        public List<Veiculo> Read()
        {
            List<Veiculo> lista = null;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = base.DbConnection;
                sqlCommand.CommandText = @"SELECT * FROM v_veiculos";
                SqlDataReader reader = sqlCommand.ExecuteReader();

                lista = new List<Veiculo>();

                while (reader.Read())
                {
                    Veiculo veiculo = new Veiculo();
                    veiculo.Placa = (string)reader["placa"];
                    veiculo.Id = (int)reader["id"];
                    veiculo.Descricao = (string)reader["descricao"];
                    veiculo.Marca = (string)reader["marca"];
                    veiculo.ValorDiaria = (decimal)reader["valor_diaria"];
                    veiculo.Lugares = (int)reader["lugares"];
                    veiculo.Carga = (int)reader["carga"];
                    veiculo.Categoria = (string)reader["categoria"];
                    veiculo.Tipo = (string)reader["tipo"];
                    veiculo.Status = (int)reader["status"];

                    lista.Add(veiculo);
                }
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
            return lista;
        }


        public Veiculo ReadById(int id)
        {
            string idString = Convert.ToString(id);
            return Read("id", idString);
        }

        public Veiculo ReadbyPlaca(string placa) { return Read("placa", placa); }

        private Veiculo Read(string tipo, string stringBusca)
        {
            Veiculo veiculo = null;

            string cmdTxt = new StringBuilder("SELECT *  from v_veiculos WHERE id = @").Append(tipo).ToString();
            SqlCommand sqlCommand = new SqlCommand(cmdTxt, base.DbConnection);
            sqlCommand.Parameters.AddWithValue("@" + tipo, stringBusca);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                veiculo = new Veiculo();
                veiculo.Placa = (string)reader["placa"];
                veiculo.Id = (int)reader["id"];
                veiculo.Descricao = (string)reader["descricao"];
                veiculo.ValorDiaria = (decimal)reader["valor_diaria"];
                veiculo.Lugares = (int)reader["lugares"];
                veiculo.Carga = (int)reader["carga"];
                veiculo.Categoria = (string)reader["categoria"];
                veiculo.Marca = (string)reader["marca"];
                veiculo.Tipo = (string)reader["tipo"];
                veiculo.Status = (int)reader["status"];
            }
            return veiculo;
        }

        public void Update(Veiculo veiculo)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;

            sqlCommand.CommandText =
            @"EXEC altVei  @id, @placa, @descricao, @valor_diaria, @lugares, @carga, @categoria, @tipo, @status, @marca; ";

            sqlCommand.Parameters.AddWithValue("@id", veiculo.Id);
            sqlCommand.Parameters.AddWithValue("@placa", veiculo.Placa);
            sqlCommand.Parameters.AddWithValue("@descricao", veiculo.Descricao);
            sqlCommand.Parameters.AddWithValue("@valor_diaria", veiculo.ValorDiaria);
            sqlCommand.Parameters.AddWithValue("@lugares", veiculo.Lugares);
            sqlCommand.Parameters.AddWithValue("@carga", veiculo.Carga);
            sqlCommand.Parameters.AddWithValue("@categoria", veiculo.Categoria);
            sqlCommand.Parameters.AddWithValue("@tipo", veiculo.Tipo);
            sqlCommand.Parameters.AddWithValue("@status", veiculo.Status);
            sqlCommand.Parameters.AddWithValue("@marca", veiculo.Marca);
            sqlCommand.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;
            sqlCommand.CommandText = @"DELETE FROM veiculos WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.ExecuteNonQuery();


        }
    }


}
