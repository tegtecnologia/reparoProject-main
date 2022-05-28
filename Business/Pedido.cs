using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Pedido
    {
        public int id { get; set; }
        public int idCliente { get; set; }
        public int idLuthier { get; set; }
        public string descricao { get; set; }
        public double valor { get; set; }
        public DateTime dataEmissao { get; set; }
        public DateTime dataUltAtualiza { get; set; }
        public string enderecoEntrega { get; set; }
        public int statusPedido { get; set; }
        public string obsLuthier { get; set; }
        public int instrumentoAlvo { get; set; }
        public int tipoServico { get; set; }
        public double avaliacao { get; set; }

        public void Save()
        {
            new Database.Pedido().Salvar(this.idCliente, this.idLuthier, this.descricao, this.enderecoEntrega, this.statusPedido, this.instrumentoAlvo, this.tipoServico);
        }

        public List<Pedido> BuscarIdUltimoCadastrado(int idCliente, int idLuthier, string descricao, string enderecoEntrega, int statusPedido, int instrumentoAlvo, int tipoServico)
        {
            var lista = new List<Pedido>();
            var clienteDb = new Database.Pedido();
            foreach (DataRow row in clienteDb.BuscarIdUltimoPedido(idCliente, idLuthier, descricao, enderecoEntrega, statusPedido, instrumentoAlvo, tipoServico).Rows)
            {
                var pedido = new Pedido();
                pedido.id = Convert.ToInt32(row["id"]);
                pedido.idCliente = Convert.ToInt32(row["idCliente"]);
                pedido.idLuthier = Convert.ToInt32(row["idLuthier"]);
                pedido.descricao = row["descricaoSituacao"].ToString();
                pedido.dataEmissao = Convert.ToDateTime(row["dataEmissao"]);
                pedido.enderecoEntrega = row["enderecoEntrega"].ToString();
                pedido.statusPedido = Convert.ToInt32(row["statusPedido"]);
                pedido.instrumentoAlvo = Convert.ToInt32(row["instrumentoAlvo"]);
                pedido.tipoServico = Convert.ToInt32(row["tipoServico"]);
                lista.Add(pedido);
            }
            return lista;
        }
    }
}