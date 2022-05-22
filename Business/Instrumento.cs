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
    }
}