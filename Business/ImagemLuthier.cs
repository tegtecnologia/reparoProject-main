using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ImagemLuthier
    {
        public int idImagem { get; set; }
        public int idLuthier { get; set; }
        public string nomeImagem { get; set; }
        public string caminhoImagem { get; set; }
        public string tipoImg { get; set; }
        public DateTime dataUpload { get; set; }

        public void Save()
        {
            new Database.ImagemLuthier().SalvarImagemLuthier(this.idLuthier, this.nomeImagem, this.caminhoImagem, this.tipoImg);
        }

        public List<ImagemLuthier> ListarTodas()
        {
            var lista = new List<ImagemLuthier>();
            var itensDoBanco = new Database.ImagemLuthier();
            foreach (DataRow row in itensDoBanco.ListaImagensLuthier().Rows)
            {
                var imagem = new ImagemLuthier();
                imagem.idImagem = Convert.ToInt32(row["idImg"]);
                imagem.idLuthier = Convert.ToInt32(row["idLuthier"]);
                imagem.nomeImagem = row["nomeImagem"].ToString();
                imagem.caminhoImagem= row["caminhoImagem"].ToString();
                imagem.dataUpload = (DateTime)row["dataUpload"];
                imagem.tipoImg = row["tipoImg"].ToString();
                lista.Add(imagem);
            }
            return lista;
        }
    }
}