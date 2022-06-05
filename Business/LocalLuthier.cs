using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class LocalLuthier
    {
        public int id { get; set; }
        public int idLuthier { get; set; }
        public string nomeLocal { get; set; }
        public string logradouro { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string estado { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }

        public void Salvar()
        {
            new Database.LocalLuthier().SalvarLocal(this.idLuthier, this.nomeLocal, this.logradouro, this.cidade, this.bairro, this.estado, this.longitude, this.latitude);
        }

        public List<LocalLuthier> BuscaPorLuthier(int id)
        {
            var lista = new List<LocalLuthier>();
            var luthierDb = new Database.LocalLuthier();
            foreach (DataRow row in luthierDb.BuscaPorIdLuthier(id).Rows)
            {
                var local = new LocalLuthier();
                local.id = Convert.ToInt32(row["id"]);
                local.idLuthier = Convert.ToInt32(row["idLuthier"]);
                local.nomeLocal = row["nomeLocal"].ToString();
                local.logradouro = row["logradouro"].ToString();
                local.cidade = row["cidade"].ToString();
                local.bairro = row["bairro"].ToString();
                local.estado = row["estado"].ToString();
                local.longitude = row["longitude"].ToString();
                local.latitude = row["latitude"].ToString();
                lista.Add(local);
            }
            return lista;
        }
    }
}