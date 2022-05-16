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
    }
}