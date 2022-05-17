using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ImagemCliente
    {
        public int idImagem { get; set; }
        public int idCliente { get; set; }
        public string nomeImagem { get; set; }
        public string caminhoImagem { get; set; }
        public string tipoImg { get; set; }
        public DateTime dataUpload { get; set; }

        public void Save()
        {
            new Database.ImagemCliente().SalvarImagemCliente(this.idCliente, this.nomeImagem, this.caminhoImagem, this.tipoImg);
        }

        public List<ImagemCliente> ListarTodas()
        {
            var lista = new List<ImagemCliente>();
            var itensDoBanco = new Database.ImagemCliente();
            foreach (DataRow row in itensDoBanco.ListaImagensDeClientes().Rows)
            {
                var imagem = new ImagemCliente();
                imagem.idImagem = Convert.ToInt32(row["idImg"]);
                imagem.idCliente = Convert.ToInt32(row["idCliente"]);
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