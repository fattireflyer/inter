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

                SqlCommand sqlVeiculo = new SqlCommand();
                sqlVeiculo.Connection = base.DbConnection;
                sqlVeiculo.CommandText = @"UPDATE veiculos set status = 0 WHERE id = @id";
                sqlVeiculo.Parameters.AddWithValue("@id", reserva.IdVeiculo);
                sqlVeiculo.ExecuteNonQuery();
            }
        }

        public List<ContratoViewModel> Read()
        {
            List<ContratoViewModel> lista = null;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = base.DbConnection;
                sqlCommand.CommandText = @"select c.*, p.nome from contratos c inner join pessoas p on p.id = c.cliente_id where c.status != 3";
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

        public Contrato Read(int id)
        {
            Contrato contrato = new Contrato();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;
            sqlCommand.CommandText = @"select cli.cnpj, c.data_inicial, c.data_final, r.veiculo_id, r.data_saida, r.data_contratada from veiculos v inner join reservas r on r.veiculo_id = v.id inner join contratos c on r.contrato_id = c.id inner join clientes cli on c.cliente_id = cli.pessoa_id where r.contrato_id = @contrato_id";

            sqlCommand.Parameters.AddWithValue("@contrato_id", id);

            List<Reserva> reservas = new List<Reserva>();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                contrato.ClienteCNPJ = (string)reader["cnpj"];
                contrato.DataInicial = (DateTime)reader["data_inicial"];
                contrato.DataFinal = (DateTime)reader["data_final"];
                contrato.Id = id;

                Reserva reserva = new Reserva();
                reserva.IdContrato = id;
                reserva.IdVeiculo = (int)reader["veiculo_id"];
                reserva.DataRetirada = (DateTime)reader["data_saida"];
                reserva.DataFinalContratada = (DateTime)reader["data_contratada"];

                reservas.Add(reserva);
            }

            contrato.ListaReservas = JsonSerializer.Serialize<List<Reserva>>(reservas);

            return contrato;
        }

        public void Update(Contrato contrato)
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
            sqlCommand.CommandText = @"update contratos set valor_total = @valor_total, data_inicial = @data_inicial, cliente_id = @cliente_id, data_final = @data_final where id = @contrato_id";
            sqlCommand.Parameters.AddWithValue("@valor_total", valorTotal);
            sqlCommand.Parameters.AddWithValue("@data_inicial", contrato.DataInicial);
            sqlCommand.Parameters.AddWithValue("@cliente_id", clienteId);
            sqlCommand.Parameters.AddWithValue("@data_final", contrato.DataFinal);
            sqlCommand.Parameters.AddWithValue("@contrato_id", contrato.Id);
            sqlCommand.ExecuteNonQuery();

            SqlCommand sqlReserva = new SqlCommand();
            sqlReserva.Connection = base.DbConnection;
            sqlReserva.CommandText = @"select veiculo_id from veiculos v inner join reservas r on r.veiculo_id = v.id inner join contratos c on r.contrato_id = c.id where r.contrato_id = @contrato_id";
            sqlReserva.Parameters.AddWithValue("@contrato_id", contrato.Id);

            SqlDataReader reader = sqlReserva.ExecuteReader();

            while (reader.Read())
            {
                int veiculoId = (int)reader["veiculo_id"];

                SqlCommand sqlUpdate = new SqlCommand();
                sqlUpdate.Connection = base.DbConnection;
                sqlUpdate.CommandText = @"UPDATE veiculos set status = 1 WHERE id = @id";
                sqlUpdate.Parameters.AddWithValue("@id", veiculoId);
                sqlUpdate.ExecuteNonQuery();
            }

            SqlCommand sqlRemove = new SqlCommand();
            sqlRemove.Connection = base.DbConnection;
            sqlRemove.CommandText = @"DELETE from reservas WHERE contrato_id = @id";
            sqlRemove.Parameters.AddWithValue("@id", contrato.Id);
            sqlRemove.ExecuteNonQuery();

            foreach (var reserva in reservas)
            {
                SqlCommand cmdReserva = new SqlCommand();
                cmdReserva.Connection = base.DbConnection;
                cmdReserva.CommandText = @"insert into reservas values(@contrato_id, @veiculo_id, @data_saida, @data_contratada, @STATUS)";
                cmdReserva.Parameters.AddWithValue("@contrato_id", contrato.Id);
                cmdReserva.Parameters.AddWithValue("@veiculo_id", reserva.IdVeiculo);
                cmdReserva.Parameters.AddWithValue("@data_saida", reserva.DataRetirada);
                cmdReserva.Parameters.AddWithValue("@data_contratada", reserva.DataFinalContratada);
                cmdReserva.Parameters.AddWithValue("@STATUS", 1);
                cmdReserva.ExecuteNonQuery();

                SqlCommand sqlVeiculo = new SqlCommand();
                sqlVeiculo.Connection = base.DbConnection;
                sqlVeiculo.CommandText = @"UPDATE veiculos set status = 0 WHERE id = @id";
                sqlVeiculo.Parameters.AddWithValue("@id", reserva.IdVeiculo);
                sqlVeiculo.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;
            sqlCommand.CommandText = @"UPDATE contratos set status = 3 WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.ExecuteNonQuery();

            SqlCommand sqlVeiculo = new SqlCommand();
            sqlVeiculo.Connection = base.DbConnection;
            sqlVeiculo.CommandText = @"select veiculo_id from veiculos v inner join reservas r on r.veiculo_id = v.id inner join contratos c on r.contrato_id = c.id where r.contrato_id = @contrato_id";
            sqlVeiculo.Parameters.AddWithValue("@contrato_id", id);

            SqlDataReader reader = sqlVeiculo.ExecuteReader();

            while (reader.Read())
            {
                int veiculoId = (int)reader["veiculo_id"];

                SqlCommand sqlUpdate = new SqlCommand();
                sqlUpdate.Connection = base.DbConnection;
                sqlUpdate.CommandText = @"UPDATE veiculos set status = 1 WHERE id = @id";
                sqlUpdate.Parameters.AddWithValue("@id", veiculoId);
                sqlUpdate.ExecuteNonQuery();
            }
        }

        public void Finalizar(int id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = base.DbConnection;
            sqlCommand.CommandText = @"UPDATE contratos set status = 2 WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.ExecuteNonQuery();

            SqlCommand sqlVeiculo = new SqlCommand();
            sqlVeiculo.Connection = base.DbConnection;
            sqlVeiculo.CommandText = @"select veiculo_id from veiculos v inner join reservas r on r.veiculo_id = v.id inner join contratos c on r.contrato_id = c.id where r.contrato_id = @contrato_id";
            sqlVeiculo.Parameters.AddWithValue("@contrato_id", id);

            SqlDataReader reader = sqlVeiculo.ExecuteReader();

            while (reader.Read())
            {
                int veiculoId = (int)reader["veiculo_id"];

                SqlCommand sqlUpdate = new SqlCommand();
                sqlUpdate.Connection = base.DbConnection;
                sqlUpdate.CommandText = @"UPDATE veiculos set status = 1 WHERE id = @id";
                sqlUpdate.Parameters.AddWithValue("@id", veiculoId);
                sqlUpdate.ExecuteNonQuery();
            }
        }
    }


}
