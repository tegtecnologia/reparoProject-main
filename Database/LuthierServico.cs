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
    public class LuthierServico
    {
        private string sqlConn()
        {
            return ConfigurationManager.AppSettings["sqlConn"];
        }

        public void SalvarLuthier(int idServico, int idLuthier)
        {
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "insert into luthiersServicos values (" + idLuthier + ", " + idServico + ")";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}