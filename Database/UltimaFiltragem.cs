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
    public class UltimaFiltragem
    {
        private string sqlConn()
        {
            return ConfigurationManager.AppSettings["sqlConn"];
        }

        public void Salvar(int idUsuario, int idUltimoInstrumentoPesq, int idUltimoServicoPesq)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "insert into logUltFiltragem (idUsuario, idUltimoServicoPesq, idUltimoInstrumentoPesq, dataFiltragem) values(" + idUsuario + ", " + idUltimoServicoPesq + ", " + idUltimoInstrumentoPesq + ", getdate())";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable BuscaUltimaFiltragemPorContaLogada(string idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "select top 1 * from logUltFiltragem where idUsuario = " + idUsuario + " order by dataFiltragem desc";
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