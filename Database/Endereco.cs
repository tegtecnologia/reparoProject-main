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
    public class Endereco
    {
        private string sqlConn()
        {
            return ConfigurationManager.AppSettings["sqlConn"];
        }

        public void SalvarCliente(string logradouro, string bairro, string cidade, string uf, int cep, int idCliente)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "insert into enderecosClientes values ('" + logradouro + "', '" + bairro + "', '" + cidade + "', '" + uf + "', " + cep + ", " + idCliente + ")";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SalvarLuthier(string logradouro, string bairro, string cidade, string uf, int cep, int idCliente)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "insert into enderecosLuthiers values ('" + logradouro + "', '" + bairro + "', '" + cidade + "', '" + uf + "', " + cep + ", " + idCliente + ")";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable BuscaEnderecosDeClientePorId(int idCliente)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from enderecosClientes where idCliente = " + idCliente;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable BuscaEnderecosDeLuthierPorId(int idCliente)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from enderecosLuthiers where idLuthier = " + idCliente;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }
    }
}