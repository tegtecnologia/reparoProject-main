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

        public List<Instrumento> Listar()
        {
            var lista = new List<Instrumento>();
            var itensDoBanco = new Database.Instrumento();
            foreach (DataRow row in itensDoBanco.ListaInstrumentos().Rows)
            {
                var instrumento = new Instrumento();
                instrumento.id = Convert.ToInt32(row["id"]);
                instrumento.nome = row["nome"].ToString();
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
                lista.Add(instrumento);
            }
            return lista;
        }

        public void DeletarPorId()
        {
            new Database.Instrumento().Deletar(this.id);
        }
    }
}