using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Unica.Models;
namespace Unica.Data
{
    public class ClienteData : Data
    {
        public void Create(Cliente cliente)
        {

            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = base.DbConnection;

            sqlCommand.CommandText =
            @"Exec cadCli 
                @nome, @telefone, @email, @logradouro, @numero, @complemento, @bairro,@cidade, @estado, @cep, @status,  @cnpj, @razao_social";


            sqlCommand.Parameters.AddWithValue("@nome", cliente.Nome);
            sqlCommand.Parameters.AddWithValue("@telefone", cliente.Telefone);
            sqlCommand.Parameters.AddWithValue("@email", cliente.Email);
            sqlCommand.Parameters.AddWithValue("@logradouro", cliente.Logradouro);
            sqlCommand.Parameters.AddWithValue("@numero", cliente.Numero);
            sqlCommand.Parameters.AddWithValue("@complemento", cliente.Complemento);
            sqlCommand.Parameters.AddWithValue("@bairro", cliente.Bairro);
            sqlCommand.Parameters.AddWithValue("@cidade", cliente.Cidade);
            sqlCommand.Parameters.AddWithValue("@estado", cliente.Estado);
            sqlCommand.Parameters.AddWithValue("@cep", cliente.Cep);
            sqlCommand.Parameters.AddWithValue("@status", 1);
            sqlCommand.Parameters.AddWithValue("@cnpj", cliente.Cnpj);
            sqlCommand.Parameters.AddWithValue("@razao_social", cliente.RazaoSocial);

            sqlCommand.ExecuteNonQuery();


        }

        public List<Cliente> Read()
        {
            List<Cliente> lista = null;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = base.DbConnection;
                sqlCommand.CommandText = @"SELECT * FROM v_clientes WHERE status = 1";

                SqlDataReader reader = sqlCommand.ExecuteReader();

                lista = new List<Cliente>();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.Id = (int)reader["id"];
                    cliente.Nome = (string)reader["nome"];
                    cliente.Telefone = (string)reader["telefone"];
                    cliente.Email = (string)reader["email"];
                    cliente.Logradouro = (string)reader["logradouro"];
                    cliente.Numero = (string)reader["cep"];
                    cliente.Complemento = (string)reader["complemento"];
                    cliente.Bairro = (string)reader["bairro"];
                    cliente.Cidade = (string)reader["cidade"];
                    cliente.Estado = (string)reader["estado"];
                    cliente.Cep = (string)reader["cep"];
                    cliente.Cnpj = (string)reader["cnpj"];
                    cliente.RazaoSocial = (string)reader["razao_social"];
                    cliente.Status = (int)reader["status"];

                    lista.Add(cliente);
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

        public Cliente ReadById(int id)
        {
            string IdString = Convert.ToString(id);
            return Read(IdString);
        }

        public Cliente ReadByCnpj(string cnpj)
        {
            return Read(cnpj);
        }


        private Cliente Read(string stringBusca)
        {
            Cliente cliente = null;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;



            sqlCommand.CommandText = @"SELECT *  from v_clientes WHERE id = @" + stringBusca;
            sqlCommand.Parameters.AddWithValue("@" + stringBusca, stringBusca);


            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                cliente = new Cliente();
                cliente.Id = (int)reader["id"];
                cliente.Nome = (string)reader["nome"];
                cliente.Telefone = (string)reader["telefone"];
                cliente.Email = (string)reader["email"];
                cliente.Logradouro = (string)reader["logradouro"];
                cliente.Numero = (string)reader["cep"];
                cliente.Complemento = (string)reader["complemento"];
                cliente.Bairro = (string)reader["bairro"];
                cliente.Cidade = (string)reader["cidade"];
                cliente.Estado = (string)reader["estado"];
                cliente.Cep = (string)reader["cep"];
                cliente.Cnpj = (string)reader["cnpj"];
                cliente.RazaoSocial = (string)reader["razao_social"];
            }
            return cliente;
        }

        public void Update(Cliente cliente)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;

            sqlCommand.CommandText =
            @"EXEC altCli  @id, @nome,  @telefone,  @email,  @logradouro, @numero,  @complemento, 
             @bairro, @cidade,  @estado, @cep,  @status, @cnpj, @razao_social";

            sqlCommand.Parameters.AddWithValue("@id", cliente.Id);
            sqlCommand.Parameters.AddWithValue("@nome", cliente.Nome);
            sqlCommand.Parameters.AddWithValue("@telefone", cliente.Telefone);
            sqlCommand.Parameters.AddWithValue("@email", cliente.Email);
            sqlCommand.Parameters.AddWithValue("@logradouro", cliente.Logradouro);
            sqlCommand.Parameters.AddWithValue("@numero", cliente.Numero);
            sqlCommand.Parameters.AddWithValue("@complemento", cliente.Complemento);
            sqlCommand.Parameters.AddWithValue("@bairro", cliente.Bairro);
            sqlCommand.Parameters.AddWithValue("@cidade", cliente.Cidade);
            sqlCommand.Parameters.AddWithValue("@estado", cliente.Estado);
            sqlCommand.Parameters.AddWithValue("@cep", cliente.Cep);
            sqlCommand.Parameters.AddWithValue("@status", cliente.Status);
            sqlCommand.Parameters.AddWithValue("@cnpj", cliente.Cnpj);
            sqlCommand.Parameters.AddWithValue("@razao_social", cliente.RazaoSocial);

            sqlCommand.ExecuteNonQuery();
        }
        public void Deactivate(int id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;

            sqlCommand.CommandText = @" EXEC deactivatePes @id";
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
