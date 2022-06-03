using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Luthier
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string cnpj { get; set; }
        public string email { get; set; }
        public int listaPedidos { get; set; }
        public int usuario { get; set; }
        public int tipoServico { get; set; }
        public double avaliacao { get; set; }
        public string img { get; set; }
        public DateTime dataCriacao { get; set; }
        public List<Luthier> BuscarPorId(int id)
        {
            var lista = new List<Luthier>();
            var luthierDb = new Database.Luthier();
            foreach (DataRow row in luthierDb.BuscaPorId(id).Rows)
            {
                var luthier = new Luthier();
                luthier.id = Convert.ToInt32(row["id"]);
                luthier.nome = row["nome"].ToString();
                luthier.cpf = row["cpf"].ToString();
                luthier.cnpj = row["cnpj"].ToString();
                luthier.email = row["email"].ToString();
                luthier.usuario = Convert.ToInt32(row["usuario"]);
                luthier.dataCriacao = Convert.ToDateTime(row["dataCriacao"]);
                lista.Add(luthier);
            }
            return lista;
        }

        public object BuscaDadosPorId(int id)
        {
            var luthier = new Luthier();
            var luthierDb = new Database.Luthier();
            foreach (DataRow row in luthierDb.BuscarDadosPorId(id).Rows)
            {
                luthier.id = Convert.ToInt32(row["id"]);
                luthier.nome = row["nome"].ToString();
                luthier.cpf = row["cpf"].ToString();
                luthier.cnpj = row["cnpj"].ToString();
                luthier.email = row["email"].ToString();
                luthier.usuario = Convert.ToInt32(row["usuario"]);
                luthier.dataCriacao = Convert.ToDateTime(row["dataCriacao"]);
            }
            return luthier;
        }

        public List<Luthier> FiltrarLuthier(int tipoServico)
        {
            var lista = new List<Luthier>();
            var itensDoBanco = new Database.Luthier();
            foreach (DataRow row in itensDoBanco.FiltroLuthierPorServico(tipoServico).Rows)
            {
                var luthier = new Luthier();
                luthier.id = Convert.ToInt32(row["id"]);
                luthier.nome = row["nome"].ToString();
                luthier.cpf = row["cpf"].ToString();
                luthier.cnpj = row["cnpj"].ToString();
                luthier.email = row["email"].ToString();
                luthier.usuario = Convert.ToInt32(row["usuario"]);
                luthier.dataCriacao = Convert.ToDateTime(row["dataCriacao"]);
                lista.Add(luthier);
            }
            return lista; 
        }

        public void Save()
        {
            new Database.Luthier().Salvar(this.nome, this.cpf, this.cnpj, this.email, this.usuario);
        }

        public static object BuscaUltimoLuthierCadastrado()
        {
            var luthier = new Luthier();
            var luthierDb = new Database.Luthier();
            foreach (DataRow row in luthierDb.BuscaPorUltimoIdCadastrado().Rows)
            {
                luthier.id = Convert.ToInt32(row["id"]);
                luthier.nome = row["nome"].ToString();
                luthier.cpf = row["cpf"].ToString();
                luthier.cnpj = row["cnpj"].ToString();
                luthier.email = row["email"].ToString();
                luthier.usuario = Convert.ToInt32(row["usuario"]);
                luthier.dataCriacao = Convert.ToDateTime(row["dataCriacao"]);
            }
            return luthier;
        }

        public List<Luthier> ListarTodosLuthiers()
        {
            var lista = new List<Luthier>();
            var luthierDb = new Database.Luthier();
            foreach (DataRow row in luthierDb.BuscaTodosLuthiers().Rows)
            {
                var luthier = new Luthier();
                luthier.id = Convert.ToInt32(row["id"]);
                luthier.nome = row["nome"].ToString();
                luthier.cpf = row["cpf"].ToString();
                luthier.cnpj = row["cnpj"].ToString();
                luthier.email = row["email"].ToString();
                luthier.usuario = Convert.ToInt32(row["usuario"]);
                luthier.dataCriacao = Convert.ToDateTime(row["dataCriacao"]);
                lista.Add(luthier);
            }
            return lista;
        }

        public List<Luthier> BuscarPorIdDoUsuario(int id)
        {
            var lista = new List<Luthier>();
            var luthierDb = new Database.Luthier();
            foreach (DataRow row in luthierDb.BuscarPorConta(id).Rows)
            {
                var luthier = new Luthier();
                luthier.id = Convert.ToInt32(row["id"]);
                luthier.nome = row["nome"].ToString();
                luthier.cpf = row["cpf"].ToString();
                luthier.cnpj = row["cnpj"].ToString();
                luthier.email = row["email"].ToString();
                luthier.img = row["img"].ToString();
                luthier.usuario = Convert.ToInt32(row["usuario"]);
                luthier.dataCriacao = Convert.ToDateTime(row["dataCriacao"]);
                lista.Add(luthier);
            }
            return lista;
        }

        public List<Luthier> BuscarTodasInfoPorId(int id)
        {
            var lista = new List<Luthier>();
            var luthierDb = new Database.Luthier();
            foreach (DataRow row in luthierDb.BuscaTudoPorId(id).Rows)
            {
                var luthier = new Luthier();
                luthier.id = Convert.ToInt32(row["id"]);
                luthier.nome = row["nome"].ToString();
                luthier.cpf = row["cpf"].ToString();
                luthier.cnpj = row["cnpj"].ToString();
                luthier.email = row["email"].ToString();
                luthier.usuario = Convert.ToInt32(row["usuario"]);
                luthier.dataCriacao = Convert.ToDateTime(row["dataCriacao"]);
                lista.Add(luthier);
            }
            return lista;
        }
    }
}