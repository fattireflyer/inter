using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Unica.Models;

namespace Unica.Data
{
    public class VeiculoData : Data
    {
        public void Create (Veiculo veiculo)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;
            sqlCommand.CommandText = @"EXEC cadVei @placa, @descricao, @valor_diaria, @lugares, @carga, @categoria_codigo, @status";

            sqlCommand.Parameters.AddWithValue("@placa", veiculo.Placa);
            sqlCommand.Parameters.AddWithValue("@descricao", veiculo.Descricao);
            sqlCommand.Parameters.AddWithValue("@valor_diaria", veiculo.ValorDiaria);
            sqlCommand.Parameters.AddWithValue("@lugares", veiculo.Lugares);
            sqlCommand.Parameters.AddWithValue("@carga", veiculo.Carga);
            sqlCommand.Parameters.AddWithValue("@categoria_codigo", veiculo.Categoria.Codigo);
            sqlCommand.Parameters.AddWithValue("@tipo_codigo", veiculo.Tipo.Codigo);
            sqlCommand.Parameters.AddWithValue("@status", veiculo.Status);

            sqlCommand.ExecuteNonQuery();
        }

        public List<Veiculo> Read ()
        {
            List<Veiculo> lista = null;
            try{
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = base.DbConnection;
                    sqlCommand.CommandText = @"SELECT * FROM v_veiculos";
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    lista = new List<Veiculo>();

                    while(reader.Read())
                    {
                        Veiculo veiculo             = new Veiculo();
                        veiculo.Placa               = (string)reader["placa"];
                        veiculo.Codigo              = (int)reader["codigo"];
                        veiculo.Descricao           = (string)reader["descricao"];
                        veiculo.ValorDiaria         = (double)reader["valor_diaria"];
                        veiculo.Lugares             = (int)reader["lugares"];
                        veiculo.Carga               = (float)reader["carga"];
                        veiculo.Categoria.Codigo    = (int)reader["categoria_codigo"];
                        veiculo.Categoria.Descricao = (string)reader["categoria"];
                        veiculo.Tipo.Codigo         = (int)reader["tipo_codigo"];    
                        veiculo.Tipo.Descricao      = (string)reader["tipo"];
                        veiculo.Status              = (StatusVeiculo)reader["status"];
        
                        lista.Add(veiculo);
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
        
        
        public Veiculo ReadById(int codigo){
            string codigoString = Convert.ToString(codigo);
            return Read("codigo", codigoString);
        }
        
        public Veiculo ReadbyPlaca (string placa){return Read("placa",placa);}

        private Veiculo Read (string tipo, string stringBusca)
        {
            Veiculo veiculo = null;

            string cmdTxt = new StringBuilder("SELECT *  from v_veiculos WHERE codigo = @").Append(tipo).ToString();
            SqlCommand sqlCommand = new SqlCommand(cmdTxt, base.DbConnection );
            sqlCommand.Parameters.AddWithValue("@"+tipo, stringBusca);
            
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if(reader.Read())
            {
                veiculo = new Veiculo();
                veiculo.Codigo              = (int)reader["codigo"];
                veiculo.Placa               = (string)reader["placa"];
                veiculo.Descricao           = (string)reader["descricao"];
                veiculo.ValorDiaria         = (double)reader["valor_diaria"];
                veiculo.Lugares             = (int)reader["lugares"];
                veiculo.Carga               = (float)reader["carga"];
                veiculo.Categoria.Codigo    = (int)reader["categoria_codigo"];
                veiculo.Categoria.Descricao = (string)reader["categoria"];
                veiculo.Tipo.Codigo         = (int)reader["tipo_codigo"];
                veiculo.Tipo.Descricao      = (string)reader["ipo"];
                veiculo.Status              = (StatusVeiculo)reader["status"];
            }    
            return veiculo;
        }

        public void Update (Veiculo veiculo)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;

            sqlCommand.CommandText = 
            @"EXEC altVei  @placa, @descricao,  @valor_diaria, @lugares, @carga, @categoria_codigo, @tipo_codigo, @status, @codigo; ";

            sqlCommand.Parameters.AddWithValue("@codigo", veiculo.Codigo);
            sqlCommand.Parameters.AddWithValue("@placa", veiculo.Placa);
            sqlCommand.Parameters.AddWithValue("@descricao", veiculo.Descricao);
            sqlCommand.Parameters.AddWithValue("@valor_diaria", veiculo.ValorDiaria);
            sqlCommand.Parameters.AddWithValue("@lugares", veiculo.Lugares);
            sqlCommand.Parameters.AddWithValue("@carga", veiculo.Carga);
            sqlCommand.Parameters.AddWithValue("@categoria_codigo", veiculo.Categoria.Codigo);
            sqlCommand.Parameters.AddWithValue("@tipo_codigo", veiculo.Tipo.Codigo);
            sqlCommand.Parameters.AddWithValue("@status", veiculo.Status);
            sqlCommand.ExecuteNonQuery();     
        }
        public void Delete (int codigo)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;
            sqlCommand.CommandText = @"DELETE FROM veiculos WHERE codigo = @codigo";
            sqlCommand.Parameters.AddWithValue("@codigo", codigo);
            sqlCommand.ExecuteNonQuery();


        }
    }


}
