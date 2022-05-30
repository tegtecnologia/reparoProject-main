using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Endereco
    {
        public int id { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public int cep { get; set; }
        public int numero { get; set; }
        public int idElemento { get; set; }

        public void SaveCliente()
        {
            new Database.Endereco().SalvarCliente(this.logradouro, this.bairro, this.cidade, this.uf, this.cep, this.idElemento, this.numero);
        }

        public void SaveLuthier()
        {
            new Database.Endereco().SalvarLuthier(this.logradouro, this.bairro, this.cidade, this.uf, this.cep, this.idElemento, this.numero);
        }

        public List<Endereco> BuscaEnderecosPorIdCliente(int id)
        {
            var listaEnderecos = new List<Endereco>();
            var enderecoDb = new Database.Endereco();
            foreach (DataRow row in enderecoDb.BuscaEnderecosDeClientePorId(id).Rows)
            {
                var endereco = new Endereco();
                endereco.id = Convert.ToInt32(row["id"]);
                endereco.logradouro = row["logradouro"].ToString();
                endereco.bairro = row["bairro"].ToString();
                endereco.cidade = row["cidade"].ToString();
                endereco.uf = row["uf"].ToString();
                endereco.cep = Convert.ToInt32(row["cep"]);
                endereco.idElemento = Convert.ToInt32(row["idCliente"]);
                endereco.numero = Convert.ToInt32(row["numero"]);
                listaEnderecos.Add(endereco);
            }
            return listaEnderecos;
        }

        public List<Endereco> BuscaEnderecosPorIdLuthier(int id)
        {
            var listaEnderecos = new List<Endereco>();
            var enderecoDb = new Database.Endereco();
            foreach (DataRow row in enderecoDb.BuscaEnderecosDeLuthierPorId(id).Rows)
            {
                var endereco = new Endereco();
                endereco.id = Convert.ToInt32(row["id"]);
                endereco.logradouro = row["logradouro"].ToString();
                endereco.bairro = row["bairro"].ToString();
                endereco.cidade = row["cidade"].ToString();
                endereco.uf = row["uf"].ToString();
                endereco.cep = Convert.ToInt32(row["cep"]);
                endereco.idElemento = Convert.ToInt32(row["idLuthier"]);
                if(row["numero"] != null && row["numero"].ToString().Length > 0)
                {
                    endereco.numero = Convert.ToInt32(row["numero"]);
                }
                else
                {
                    endereco.numero = 0;
                }
                listaEnderecos.Add(endereco);
            }
            return listaEnderecos;
        }
    }
}