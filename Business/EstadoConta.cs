using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class EstadoConta
    {
        public int id { get; set; }
        public string descricao { get; set; }

        public List<Servico> Listar()
        {
            var estadosDeConta = new List<Servico>();
            var itensDoBanco = new Database.EstadoConta();
            foreach (DataRow row in itensDoBanco.ListaEstados().Rows)
            {
                var estadoConta = new Servico();
                estadoConta.id = Convert.ToInt32(row["id"]);
                estadoConta.descricao = row["descSituacaoConta"].ToString();
                estadosDeConta.Add(estadoConta);
            }
            return estadosDeConta;
        }
    }
}