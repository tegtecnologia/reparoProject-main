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
    public class Hash
    {
        private HashAlgorithm _algoritmo;

        public Hash(HashAlgorithm algoritmo)
        {
            _algoritmo = algoritmo;
        }

        public string CriptografarSenha(string senha)
        {
            var encodedValue = Encoding.UTF8.GetBytes(senha);
            var encryptedPassword = _algoritmo.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool VerificarSenha(string senhaDigitada, string senhaCadastrada)
        {
            if (string.IsNullOrEmpty(senhaCadastrada))
                throw new NullReferenceException("Cadastre uma senha.");

            var encryptedPassword = _algoritmo.ComputeHash(Encoding.UTF8.GetBytes(senhaDigitada));

            var sb = new StringBuilder();
            foreach (var caractere in encryptedPassword)
            {
                sb.Append(caractere.ToString("X2"));
            }

            return sb.ToString() == senhaCadastrada;
        }
    }

    public class Conta
    {
        private string sqlConn()
        {
            return ConfigurationManager.AppSettings["sqlConn"];
        }

        public void ConfirmaLogin(string idSession, int id)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "update usuarios set idSessao = '" + idSession + "' where id = " + id;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable BuscaPorUltimoIdCadastrado()
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select top 1 * from usuarios order by id desc";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable ListaUltimoRegistro()
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from usuarios order by id desc";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public void Salvar(string usuario, string email, string senha)
        {
            var hash = new Hash(SHA512.Create());
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "insert into usuarios (usuario, senha, statusLogin, statusConta, nivelAcesso, email) values('" + usuario + "', '" + hash.CriptografarSenha(senha) + "', 0, 1, 1, '" + email + "')";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Logar(string usuario, string senha, string idSessao)
        {
            var hash = new Hash(SHA512.Create());
            string hashTxtSenha = null;
            hashTxtSenha = hash.CriptografarSenha(senha);

            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from usuarios where usuario = '" + usuario + "' and senha = '" + hashTxtSenha + "'";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table == null)
                {
                }
                else
                {
                    string queryStringDois = "update usuarios set statusLogin = 1, idSessao = '" + idSessao + "' where usuario = '" + usuario + "' and senha = '" + hashTxtSenha + "'";
                    SqlCommand commandDois = new SqlCommand(queryStringDois, connection);
                    commandDois.ExecuteNonQuery();
                }
            }
        }

        public void Desconecta(string idSessao)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "update usuarios set idSessao = '', statusLogin = 0 where statusLogin = 1 and idSessao = '" + idSessao + "'";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable BuscaPorContaLogada(string idSessao)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from usuarios where statusLogin = 1 and idSessao = '" + idSessao + "'";
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