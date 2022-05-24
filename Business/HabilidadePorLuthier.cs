using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class HabilidadePorLuthier
    {
        public int id { get; set; }
        public int idLuthier { get; set; }
        public int idHabilidade { get; set; }

        public List<HabilidadePorLuthier> ListarPorHabilidade(int habilidadeBuscada)
        {
            var habilidadePorLuthier = new List<HabilidadePorLuthier>();
            var dadosDb = new Database.HabilidadePorLuthier();
            foreach (DataRow row in dadosDb.BuscarPorHabilidade(habilidadeBuscada).Rows)
            {
                var habilidade = new HabilidadePorLuthier();
                habilidade.id = Convert.ToInt32(row["id"]);
                habilidade.idLuthier = Convert.ToInt32(row["idLuthier"]);
                habilidade.idHabilidade = Convert.ToInt32(row["idHabilidade"]);

                habilidadePorLuthier.Add(habilidade);
            }
            return habilidadePorLuthier;
        }

        public List<HabilidadePorLuthier> ListarPorLuthier(int id)
        {
            var habilidadePorLuthier = new List<HabilidadePorLuthier>();
            var dadosDb = new Database.HabilidadePorLuthier();
            foreach (DataRow row in dadosDb.BuscarPorLuthier(id).Rows)
            {
                var habilidade = new HabilidadePorLuthier();
                habilidade.id = Convert.ToInt32(row["id"]);
                habilidade.idLuthier = Convert.ToInt32(row["idLuthier"]);
                habilidade.idHabilidade = Convert.ToInt32(row["idHabilidade"]);

                habilidadePorLuthier.Add(habilidade);
            }
            return habilidadePorLuthier;
        }

        public void Salvar()
        {
            new Database.HabilidadePorLuthier().Salvar(this.idLuthier, this.idHabilidade);
        }

        public void Deletar()
        {
            new Database.HabilidadePorLuthier().Deletar(this.idHabilidade);
        }
    }
}

