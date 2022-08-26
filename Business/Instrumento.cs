using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Instrumento
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int status { get; set; }

        public List<Instrumento> Listar()
        {
            var lista = new List<Instrumento>();
            var itensDoBanco = new Database.Instrumento();
            foreach (DataRow row in itensDoBanco.ListaInstrumentos().Rows)
            {
                var instrumento = new Instrumento();
                instrumento.id = Convert.ToInt32(row["id"]);
                instrumento.nome = row["nome"].ToString();
                instrumento.status = Convert.ToInt32(row["status"]);
                lista.Add(instrumento);
            }
            return lista;
        }

        public List<Instrumento> ListarAtivos()
        {
            var lista = new List<Instrumento>();
            var itensDoBanco = new Database.Instrumento();
            foreach (DataRow row in itensDoBanco.ListaInstrumentosAtivos().Rows)
            {
                var instrumento = new Instrumento();
                instrumento.id = Convert.ToInt32(row["id"]);
                instrumento.nome = row["nome"].ToString();
                instrumento.status = Convert.ToInt32(row["status"]);
                lista.Add(instrumento);
            }
            return lista;
        }

        public void Save()
        {
            new Database.Instrumento().Salvar(this.nome);
        }

        public static object BuscaUltimoCadastrado()
        {
            var instrumento = new Instrumento();
            var instrumentoDb = new Database.Instrumento();
            foreach (DataRow row in instrumentoDb.BuscaPorUltimoIdCadastrado().Rows)
            {
                instrumento.id = Convert.ToInt32(row["id"]);
                instrumento.nome = row["nome"].ToString();
                instrumento.status = Convert.ToInt32(row["status"]);
            }
            return instrumento;
        }

        public List<Instrumento> ListarPorId(int idInstrumento)
        {
            var lista = new List<Instrumento>();
            var instrummentoDb = new Database.Instrumento();
            foreach (DataRow row in instrummentoDb.BuscaPorId(idInstrumento).Rows)
            {
                var instrumento = new Instrumento();
                instrumento.id = Convert.ToInt32(row["id"]);
                instrumento.nome = row["nome"].ToString();
                instrumento.status = Convert.ToInt32(row["status"]);
                lista.Add(instrumento);
            }
            return lista;
        }

        public void DesativarPorId()
        {
            new Database.Instrumento().Desativar(this.id);
        }

        public void AtivarPorId()
        {
            new Database.Instrumento().Ativar(this.id);
        }
    }
}