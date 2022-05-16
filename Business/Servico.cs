using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Servico
    {
        public int id { get; set; }
        public string descricao { get; set; }

        public List<Servico> Listar()
        {
            var lista = new List<Servico>();
            var itensDoBanco = new Database.Servico();
            foreach (DataRow row in itensDoBanco.ListaServicos().Rows)
            {
                var servico = new Servico();
                servico.id = Convert.ToInt32(row["id"]);
                servico.descricao = row["descricaoServico"].ToString();
                lista.Add(servico);
            }
            return lista;
        }
    }
}