using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Contato
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string numero { get; set; }
        public int celular { get; set; }
        public int idCliente { get; set; }
        public int idLuthier { get; set; }

        public void SaveCliente()
        {
            new Database.Contato().SalvarCliente(this.codigo, this.numero, this.celular, this.idCliente);
        }
        
        public void SaveLuthier()
        {
            new Database.Contato().SalvarLuthier(this.codigo, this.numero, this.celular, this.idLuthier);
        }

        public List<Contato> BuscaContatosDeClientesPorId(int id)
        {
            var listaContatos = new List<Contato>();
            var contatoDb = new Database.Contato();
            foreach (DataRow row in contatoDb.BuscaContatosDeCliente(id).Rows)
            {
                var contato = new Contato();
                contato.id = Convert.ToInt32(row["id"]);
                contato.codigo = row["codigo"].ToString();
                contato.numero = row["numero"].ToString();
                contato.celular = Convert.ToInt32(row["celular"]);
                contato.idCliente = Convert.ToInt32(row["idCliente"]);

                listaContatos.Add(contato);
            }
            return listaContatos;
        }

        public List<Contato> BuscaContatosDeLuthiersPorId(int id)
        {
            var listaContatos = new List<Contato>();
            var contatoDb = new Database.Contato();
            foreach (DataRow row in contatoDb.BuscaContatosDeLuthier(id).Rows)
            {
                var contato = new Contato();
                contato.id = Convert.ToInt32(row["id"]);
                contato.codigo = row["codigo"].ToString();
                contato.numero = row["numero"].ToString();
                contato.celular = Convert.ToInt32(row["celular"]);
                contato.idLuthier = Convert.ToInt32(row["idLuthier"]);

                listaContatos.Add(contato);
            }
            return listaContatos;
        }
    }
}