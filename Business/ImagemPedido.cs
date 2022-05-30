using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ImagemPedido
    {
        public int id { get; set; }
        public int idImagem { get; set; }
        public int idPedido { get; set; }
        public string nomeImagem { get; set; }
        public string caminhoImagem { get; set; }
        public string tipoImg { get; set; }
        public DateTime dataUpload { get; set; }

        public void Salvar()
        {
            new Database.ImagemPedido().Salvar(this.idPedido, this.nomeImagem, this.caminhoImagem, this.tipoImg);
        }

        public List<ImagemPedido> BuscarPorPedido(int id)
        {
            var lista = new List<ImagemPedido>();
            var imgPedidosDb = new Database.ImagemPedido();
            foreach (DataRow row in imgPedidosDb.BuscaPorPedido(id).Rows)
            {
                var imagemPedido = new ImagemPedido();
                imagemPedido.id = Convert.ToInt32(row["id"]);
                imagemPedido.idPedido = Convert.ToInt32(row["idPedido"]);
                imagemPedido.nomeImagem = row["nomeImagem"].ToString();
                imagemPedido.caminhoImagem = row["caminhoImagem"].ToString();
                imagemPedido.tipoImg = row["tipoImg"].ToString();
                imagemPedido.dataUpload = Convert.ToDateTime(row["dataUpload"]);
                lista.Add(imagemPedido);
            }
            return lista;
        }
    }
}