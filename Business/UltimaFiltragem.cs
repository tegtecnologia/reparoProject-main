using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UltimaFiltragem
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int idUltimoServicoPesq { get; set; }
        public int idUltimoInstrumentoPesq { get; set; }
        public DateTime dataFiltragem { get; set; }

        public void Save()
        {
            new Database.UltimaFiltragem().Salvar(this.idUsuario, this.idUltimoInstrumentoPesq, this.idUltimoServicoPesq);
        }

        public static object BuscaUltimaFiltragemPorConta(string idUsuario)
        {
            var ultimaFiltragem = new UltimaFiltragem();
            var ultimaFiltragemDb = new Database.UltimaFiltragem();
            foreach (DataRow row in ultimaFiltragemDb.BuscaUltimaFiltragemPorContaLogada(idUsuario).Rows)
            {
                ultimaFiltragem.id = Convert.ToInt32(row["id"]);
                ultimaFiltragem.idUsuario = Convert.ToInt32(row["idUsuario"]);
                ultimaFiltragem.idUltimoServicoPesq = Convert.ToInt32(row["idUltimoServicoPesq"]);
                ultimaFiltragem.idUltimoInstrumentoPesq = Convert.ToInt32(row["idUltimoInstrumentoPesq"]);
                ultimaFiltragem.dataFiltragem = Convert.ToDateTime(row["dataFiltragem"]);
            }
            return ultimaFiltragem;
        }
    }
}