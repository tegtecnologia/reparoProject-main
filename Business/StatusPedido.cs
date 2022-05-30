using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class StatusPedido
    {
        public int id { get; set; }
        public string descricao { get; set; }

        public List<StatusPedido> Listar()
        {
            var lista = new List<StatusPedido>();
            var itensDoBanco = new Database.StatusPedido();
            foreach (DataRow row in itensDoBanco.ListaEstadosDePedido().Rows)
            {
                var statusPedido = new StatusPedido();
                statusPedido.id = Convert.ToInt32(row["id"]);
                statusPedido.descricao = row["descricaoStatus"].ToString();
                lista.Add(statusPedido);
            }
            return lista;
        }

        public List<StatusPedido> ListarPorId(int id)
        {
            var lista = new List<StatusPedido>();
            var itensDoBanco = new Database.StatusPedido();
            foreach (DataRow row in itensDoBanco.ListaEstadosDePedidoPorStatus(id).Rows)
            {
                var statusPedido = new StatusPedido();
                statusPedido.id = Convert.ToInt32(row["id"]);
                statusPedido.descricao = row["descricaoStatus"].ToString();
                lista.Add(statusPedido);
            }
            return lista;
        }
    }
}