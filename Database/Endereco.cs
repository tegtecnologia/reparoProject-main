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

        public void SalvarCliente(string logradouro, string bairro, string cidade, string uf, int cep, int idCliente, int numero)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "insert into enderecosClientes values ('" + logradouro + "', '" + bairro + "', '" + cidade + "', '" + uf + "', " + cep + ", " + idCliente + ", " + numero + ")";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SalvarLuthier(string logradouro, string bairro, string cidade, string uf, int cep, int idCliente, int numero)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "insert into enderecosLuthiers values ('" + logradouro + "', '" + bairro + "', '" + cidade + "', '" + uf + "', " + cep + ", " + idCliente + ", " + numero + ")";
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

        public DataTable BuscaEnderecosDeLuthierPorId(int idLuthier)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from enderecosLuthiers where idLuthier = " + idLuthier;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public void AtualizarEndCliente(int idCliente, int idEndereco, string cep, string numero, string logradouro, string bairro, string cidade, string uf)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "update enderecosClientes set logradouro = '" + logradouro + "', bairro = '" + bairro + "', cidade = '" + cidade + "', uf = '" + uf + "', cep = " + cep + ", numero = " + numero + " where idCliente = " + idCliente + " and id = " + idEndereco;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AtualizarEndLuthier(int idLuthier, int idEndereco, string cep, string numero, string logradouro, string bairro, string cidade, string uf)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "update enderecosLuthiers set logradouro = '" + logradouro + "', bairro = '" + bairro + "', cidade = '" + cidade + "', uf = '" + uf + "', cep = " + cep + ", numero = " + numero + " where idLuthier = " + idLuthier + " and id = " + idEndereco;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}