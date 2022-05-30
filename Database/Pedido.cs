using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Pedido
    {
        private string sqlConn()
        {
            return ConfigurationManager.AppSettings["sqlConn"];
        }

        public void Salvar(int idCliente, int idLuthier, string descricao, string enderecoEntrega, int statusPedido, int instrumentoAlvo, int servicoAlvo)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "insert into pedidos (idCliente, idLuthier, descricaoSituacao, dataEmissao, enderecoEntrega, statusPedido, instrumentoAlvo, tipoServico) values (" + idCliente + ", " + idLuthier + ", '" + descricao +  "', getdate(), '" + enderecoEntrega + "', " + statusPedido + ", " + instrumentoAlvo + ", " + servicoAlvo + ")";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable BuscarIdUltimoPedido(int idCliente, int idLuthier, string descricao, string enderecoEntrega, int statusPedido, int instrumentoAlvo, int tipoServico)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from pedidos where idCliente = " + idCliente + " and idLuthier = " + idLuthier + " and descricaoSituacao = '" + descricao + "' and enderecoEntrega = '" + enderecoEntrega + "' and instrumentoAlvo = " + instrumentoAlvo + " and tipoServico = " + tipoServico;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable BuscaPorId(int id)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from pedidos where id = " + id;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable BuscaPorCliente(int id)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from pedidos where idCliente = " + id;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable BuscaPorLuthier(int idLuthier)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from pedidos where idLuthier = " + idLuthier;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public void Atualizar(int idPedido)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "update pedidos set dataUltAtualiza = getdate() where id = " + idPedido;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AtualizarStatus(int idPedido)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "update pedidos set statusPedido = 2 where id = " + idPedido;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}