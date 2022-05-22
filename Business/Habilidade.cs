using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Business
{
    public class Habilidade
    {
        public int id { get; set; }
        public int idInstrumento { get; set; }
        public int idServico { get; set; }

        public List<Habilidade> Listar()
        {
            var lista = new List<Habilidade>();
            var itensDoBanco = new Database.Habilidade();
            foreach (DataRow row in itensDoBanco.ListaHabilidades().Rows)
            {
                var habilidade = new Habilidade();
                habilidade.id = Convert.ToInt32(row["id"]);
                habilidade.idInstrumento = Convert.ToInt32(row["idInstrumento"]);
                habilidade.idServico = Convert.ToInt32(row["idServico"]);
                lista.Add(habilidade);
            }
            return lista;
        }

        public List<Habilidade> ListarPorInstrumento(int idDoInstrumentoBuscado)
        {
            var lista = new List<Habilidade>();
            var dadosDb = new Database.Habilidade();
            foreach (DataRow row in dadosDb.BuscaPorId(idDoInstrumentoBuscado).Rows)
            {
                var servicosDoInstrumento = new Habilidade();
                servicosDoInstrumento.idInstrumento = idDoInstrumentoBuscado;
                servicosDoInstrumento.idServico = Convert.ToInt32(row["idServico"]);
                lista.Add(servicosDoInstrumento);
            }
            return lista;
        }

        public List<Habilidade> ListarPorInstrumentoAndServico(string servico, string instrumento)
        {
            var habilidadesFiltradas = new List<Habilidade>();
            var contatoDb = new Database.Habilidade();
            foreach (DataRow row in contatoDb.BuscaPorInstrumentoAndServico(servico, instrumento).Rows)
            {
                var habilidade = new Habilidade();
                habilidade.id = Convert.ToInt32(row["id"]);
                habilidade.idInstrumento = Convert.ToInt32(row["idInstrumento"]);
                habilidade.idServico = Convert.ToInt32(row["idServico"]);

                habilidadesFiltradas.Add(habilidade);
            }
            return habilidadesFiltradas;
        }

        public Habilidade BuscarPorInstAndServ(int idInstrumento, int idServico)
        {
            var lista = new List<Habilidade>();
            var habDb = new Database.Habilidade();
            var habilidade = new Habilidade();
            foreach (DataRow row in habDb.BuscarPorInstrumentoAndServico(idInstrumento, idServico).Rows)
            {
                habilidade.id = Convert.ToInt32(row["id"]);
                habilidade.idInstrumento = Convert.ToInt32(row["idInstrumento"]);
                habilidade.idServico = Convert.ToInt32(row["idServico"]);
            }
            return habilidade;
        }
    }
}