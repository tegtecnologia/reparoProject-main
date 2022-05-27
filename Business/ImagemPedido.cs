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
        public int idImagem { get; set; }
        public int idCliente { get; set; }
        public string nomeImagem { get; set; }
        public string caminhoImagem { get; set; }
        public string tipoImg { get; set; }
        public DateTime dataUpload { get; set; }

        public void Save()
        {
            new Database.ImagemPedido().SalvarImagemPedido(this.idCliente, this.nomeImagem, this.caminhoImagem, this.tipoImg);
        }
    }
}