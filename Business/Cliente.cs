using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Cliente
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string img { get; set; }
        public int usuario { get; set; }

        public void Save()
        {
            new Database.Cliente().Salvar(this.nome, this.cpf, this.email, this.usuario);
        }

        public static object BuscaUltimoClienteCadastrado()
        {
            var cliente = new Cliente();
            var clienteDb = new Database.Cliente();
            foreach (DataRow row in clienteDb.BuscaPorUltimoIdCadastrado().Rows)
            {
                cliente.id = Convert.ToInt32(row["id"]);
                cliente.cpf = row["cpf"].ToString();
                cliente.email = row["email"].ToString();
                cliente.img = row["img"].ToString();
                cliente.usuario = Convert.ToInt32(row["usuario"]);
            }
            return cliente;
        }

        public List<Cliente> BuscarPorId(int id)
        {
            var lista = new List<Cliente>();
            var clienteDb = new Database.Cliente();
            foreach (DataRow row in clienteDb.BuscaPorId(id).Rows)
            {
                var cliente = new Cliente();
                cliente.id = Convert.ToInt32(row["id"]);
                cliente.nome = row["nome"].ToString();
                cliente.cpf = row["cpf"].ToString();
                cliente.email = row["email"].ToString();
                cliente.img = row["img"].ToString();
                cliente.usuario = Convert.ToInt32(row["usuario"]);
                lista.Add(cliente);
            }
            return lista;
        }

        public List<Cliente> ListarTodosClientes()
        {
            var lista = new List<Cliente>();
            var clienteDb = new Database.Cliente();
            foreach (DataRow row in clienteDb.BuscaTodosClientes().Rows) 
            {
                var cliente = new Cliente();
                cliente.id = Convert.ToInt32(row["id"]);
                cliente.nome = row["nome"].ToString();
                cliente.cpf = row["cpf"].ToString();
                cliente.email = row["email"].ToString();
                cliente.img = row["img"].ToString();
                cliente.usuario = Convert.ToInt32(row["usuario"]);
                lista.Add(cliente);
            }
            return lista;
        }

    }
}