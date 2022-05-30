using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ObsLuthier
    {
        public int id { get; set; }
        public int idPedido { get; set; }
        public int idLuthier { get; set; }
        public DateTime dataCriacao { get; set; }
        public string conteudo { get; set; }

        public List<ObsLuthier> ListarPorPedido(int idPedido)
        {
            var lista = new List<ObsLuthier>();
            var itensDoBanco = new Database.ObsLuthier();
            foreach (DataRow row in itensDoBanco.ListaObsPorPedido(idPedido).Rows)
            {
                var obs = new ObsLuthier();
                obs.id = Convert.ToInt32(row["id"]);
                obs.idPedido = Convert.ToInt32(row["idPedido"]);
                obs.idLuthier = Convert.ToInt32(row["idLuthier"]);
                obs.dataCriacao = Convert.ToDateTime(row["dataCriacao"]);
                obs.conteudo = row["conteudo"].ToString();
                lista.Add(obs);
            }
            return lista;
        }
    }
}