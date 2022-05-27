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
    public class ImagemPedido
    {
        private string sqlConn()
        {
            return ConfigurationManager.AppSettings["sqlConn"];
        }

        public void SalvarImagemPedido(int idCliente, string nomeImagem, string caminhoImagem, string tipoImg)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
                string queryString = "insert into imagensPedidos values (" + idCliente + ", '" + nomeImagem + "', '" + caminhoImagem + "', getdate(), '" + tipoImg + "')";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}