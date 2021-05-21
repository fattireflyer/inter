using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Unica.Models;
namespace Unica.Data
{
    public class FuncionarioData : Data
    {
        public void Create(Funcionario funcionario)
        {

            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = base.DbConnection;

            sqlCommand.CommandText = 
            @"Exec cadFunc 
                @nome, @telefone, @email, @logradouro, @numero, @complemento, @bairro,@cidade, @estado, @cep, @status,  @cpf, @cargo, @usuario, @senha" ;


            sqlCommand.Parameters.AddWithValue("@nome", funcionario.Nome);
            sqlCommand.Parameters.AddWithValue("@telefone", funcionario.Telefone);
            sqlCommand.Parameters.AddWithValue("@email", funcionario.Email);
            sqlCommand.Parameters.AddWithValue("@logradouro", funcionario.Logradouro);
            sqlCommand.Parameters.AddWithValue("@numero", funcionario.Numero);
            sqlCommand.Parameters.AddWithValue("@complemento", funcionario.Complemento);
            sqlCommand.Parameters.AddWithValue("@bairro", funcionario.Bairro); 
            sqlCommand.Parameters.AddWithValue("@cidade", funcionario.Cidade);
            sqlCommand.Parameters.AddWithValue("@estado", funcionario.Estado);
            sqlCommand.Parameters.AddWithValue("@cep", funcionario.Cep);
            sqlCommand.Parameters.AddWithValue("@status", 1);
            sqlCommand.Parameters.AddWithValue("@cpf", funcionario.Cpf);
            sqlCommand.Parameters.AddWithValue("@cargo", funcionario.Cargo);
            sqlCommand.Parameters.AddWithValue("@usuario", funcionario.Usuario);
            sqlCommand.Parameters.AddWithValue("@senha", funcionario.Senha);            

            sqlCommand.ExecuteNonQuery();


        }

        public List<Funcionario> Read ()
        {
            List<Funcionario> lista = null;
            try{
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = base.DbConnection;
                    sqlCommand.CommandText = @"SELECT * FROM v_funcionarios WHERE status = 1";

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    lista = new List<Funcionario>();

                    while(reader.Read())
                    {
                        Funcionario funcionario     = new Funcionario();
                        funcionario.Codigo      = (int)reader["codigo"];
                        funcionario.Nome        = (string)reader["nome"];
                        funcionario.Telefone    = (string)reader["telefone"];
                        funcionario.Email       = (string)reader["email"];
                        funcionario.Logradouro  = (string)reader["logradouro"];
                        funcionario.Numero      = (string)reader["cep"];
                        funcionario.Complemento = (string)reader["complemento"];
                        funcionario.Bairro      = (string)reader["bairro"];
                        funcionario.Cidade      = (string)reader["cidade"];    
                        funcionario.Estado      = (string)reader["estado"];
                        funcionario.Cep         = (string)reader["cep"];
                        funcionario.Cpf         = (string)reader["cpf"];
                        funcionario.Cargo       = (string)reader["cargo"];

                        lista.Add(funcionario);
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

        public Funcionario ReadById(int id){
            string IdString = Convert.ToString(id);
            return Read(IdString);
        }
        
        public Funcionario ReadByCnpj(string cpf){
            return Read(cpf);
        }


        private Funcionario Read (string stringBusca)
        {
            Funcionario funcionario = null;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;


        
            sqlCommand.CommandText = @"SELECT *  from v_funcionarios WHERE codigo = @"+stringBusca;
            sqlCommand.Parameters.AddWithValue("@"+stringBusca, stringBusca);
            

            SqlDataReader reader = sqlCommand.ExecuteReader();

            if(reader.Read())
            {
                funcionario = new Funcionario();
                funcionario.Codigo      = (int)reader["codigo"];
                funcionario.Nome        = (string)reader["nome"];
                funcionario.Telefone    = (string)reader["telefone"];
                funcionario.Email       = (string)reader["email"];
                funcionario.Logradouro  = (string)reader["logradouro"];
                funcionario.Numero      = (string)reader["cep"];
                funcionario.Complemento = (string)reader["complemento"];
                funcionario.Bairro      = (string)reader["bairro"];
                funcionario.Cidade      = (string)reader["cidade"];    
                funcionario.Estado      = (string)reader["estado"];
                funcionario.Cep         = (string)reader["cep"];
                funcionario.Cpf         = (string)reader["cpf"];
                funcionario.Cargo       = (string)reader["cargo"];
                funcionario.Usuario     = (string)reader["usuario"];
                funcionario.Senha       = (string)reader["senha"];
            }    
            return funcionario;
        }

        public void Update (Funcionario funcionario)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;

            sqlCommand.CommandText = 
            @"EXEC altFunc  @codigo, @nome,  @telefone,  @email,  @logradouro, @numero,  @complemento, 
             @bairro, @cidade,  @estado, @cep,  @status, @cpf, @cargo";

            sqlCommand.Parameters.AddWithValue("@codigo", funcionario.Codigo);
            sqlCommand.Parameters.AddWithValue("@nome", funcionario.Nome);
            sqlCommand.Parameters.AddWithValue("@telefone", funcionario.Telefone);
            sqlCommand.Parameters.AddWithValue("@email", funcionario.Email);
            sqlCommand.Parameters.AddWithValue("@logradouro", funcionario.Logradouro);
            sqlCommand.Parameters.AddWithValue("@numero", funcionario.Numero);
            sqlCommand.Parameters.AddWithValue("@complemento", funcionario.Complemento);
            sqlCommand.Parameters.AddWithValue("@bairro", funcionario.Bairro); 
            sqlCommand.Parameters.AddWithValue("@cidade", funcionario.Cidade);
            sqlCommand.Parameters.AddWithValue("@estado", funcionario.Estado);
            sqlCommand.Parameters.AddWithValue("@cep", funcionario.Cep);
            sqlCommand.Parameters.AddWithValue("@status", funcionario.Status);
            sqlCommand.Parameters.AddWithValue("@cpf", funcionario.Cpf);
            sqlCommand.Parameters.AddWithValue("@cargo", funcionario.Cargo);
            sqlCommand.Parameters.AddWithValue("@senha", funcionario.Senha);         
            sqlCommand.Parameters.AddWithValue("@usuario", funcionario.Usuario);  

            sqlCommand.ExecuteNonQuery();     
        }
        public void Deactivate (int codigo)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;

            sqlCommand.CommandText = @" EXEC deactivatePes @codigo, @status";
            sqlCommand.Parameters.AddWithValue("@codigo", codigo);
            sqlCommand.Parameters.AddWithValue("@status", 2);
            sqlCommand.ExecuteNonQuery();   
        }
    }
}
