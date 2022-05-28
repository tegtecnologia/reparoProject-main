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
    }
}