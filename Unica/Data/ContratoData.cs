using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;


using Unica.Models;

namespace Unica.Data
{
    public class ContratoData : Data
    {
        public void Create(Contrato contrato)
        {
            decimal valorTotal = 0;
            List<Reserva> reservas = JsonSerializer.Deserialize<List<Reserva>>(contrato.ListaReservas);
            foreach (var reserva in reservas)
            {
                valorTotal = valorTotal + reserva.Valor;
            }

            int clienteId = 0;
            SqlCommand cmdCliente = new SqlCommand();
            cmdCliente.Connection = base.DbConnection;
            cmdCliente.CommandText = @"Select pessoa_id from clientes Where cnpj = @cnpj";
            cmdCliente.Parameters.AddWithValue("@cnpj", contrato.ClienteCNPJ);
            SqlDataReader readerCliente = cmdCliente.ExecuteReader();
            if (readerCliente.Read())
            {
                clienteId = (int)readerCliente["pessoa_id"];
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;
            sqlCommand.CommandText = @"insert into contratos values (@valor_total, @data_inicial, @cliente_id, @data_final, @status); SELECT SCOPE_IDENTITY();";
            sqlCommand.Parameters.AddWithValue("@valor_total", valorTotal);
            sqlCommand.Parameters.AddWithValue("@data_inicial", contrato.DataInicial);
            sqlCommand.Parameters.AddWithValue("@cliente_id", clienteId);
            sqlCommand.Parameters.AddWithValue("@data_final", contrato.DataFinal);
            sqlCommand.Parameters.AddWithValue("@status", contrato.Status);

            int idContrato = Convert.ToInt32(sqlCommand.ExecuteScalar());
            foreach (var reserva in reservas)
            {
                SqlCommand cmdReserva = new SqlCommand();
                cmdReserva.Connection = base.DbConnection;
                cmdReserva.CommandText = @"insert into reservas values(@contrato_id, @veiculo_id, @data_saida, @data_contratada, @STATUS)";
                cmdReserva.Parameters.AddWithValue("@contrato_id", idContrato);
                cmdReserva.Parameters.AddWithValue("@veiculo_id", reserva.IdVeiculo);
                cmdReserva.Parameters.AddWithValue("@data_saida", reserva.DataRetirada);
                cmdReserva.Parameters.AddWithValue("@data_contratada", reserva.DataFinalContratada);
                cmdReserva.Parameters.AddWithValue("@STATUS", 1);
                cmdReserva.ExecuteNonQuery();
            }
        }

        public List<ContratoViewModel> Read()
        {
            List<ContratoViewModel> lista = null;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = base.DbConnection;
                sqlCommand.CommandText = @"select c.*, p.nome from contratos c inner join pessoas p on p.id = c.cliente_id";
                SqlDataReader reader = sqlCommand.ExecuteReader();

                lista = new List<ContratoViewModel>();

                while (reader.Read())
                {
                    ContratoViewModel contrato = new ContratoViewModel();
                    contrato.Id = (int)reader["id"];
                    contrato.ValorTotal = (decimal)reader["valor_total"];
                    contrato.DataInicial = (DateTime)reader["data_inicial"];
                    contrato.DataFinal = (DateTime)reader["data_final"];
                    contrato.NomeCliente = (string)reader["nome"];
                    contrato.Status = (int)reader["status"];

                    lista.Add(contrato);
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
