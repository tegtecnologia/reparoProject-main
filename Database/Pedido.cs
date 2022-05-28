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
            var hash = new Hash(SHA512.Create());
            using (SqlConnection connection = new SqlConnection(sqlConn()))
            {
                string queryString = "insert into pedidos (idCliente, idLuthier, descricaoSituacao, dataEmissao, enderecoEntrega, statusPedido, instrumentoAlvo, tipoServico) values (" + idCliente + ", " + idLuthier + ", '" + descricao +  "', getdate(), '" + enderecoEntrega + "', " + statusPedido + ", " + instrumentoAlvo + ", " + servicoAlvo + ")";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}