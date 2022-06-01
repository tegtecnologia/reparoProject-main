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
    public class ImagemLuthier
    {
        private string sqlConn()
        {
            return ConfigurationManager.AppSettings["sqlConn"];
        }

        public void SalvarImagemLuthier(int idLuthier, string nomeImagem, string caminhoImagem, string tipoImg)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
                string queryString = "insert into imagensLuthiers values (" + idLuthier + ", '" + nomeImagem + "', '" + caminhoImagem + "', getdate(), '" + tipoImg + "')";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable ListaImagensLuthier()
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select * from imagensLuthiers";
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