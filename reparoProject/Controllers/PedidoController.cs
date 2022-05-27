using Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reparoProject.Controllers
{
    public class PedidoController : Controller
    {
        [HttpPost]
        [ValidateInput(false)]
        public void Solicitar()
        {
            TempData["luthierEscolhido"] = new Luthier().BuscaDadosPorId(Convert.ToInt32(Request["idLuthier"]));
            Response.Redirect("/pedido/criar");
        }

        public ActionResult CriarPedido()
        {
            return View();
        }

        public ActionResult AnexarPorPedido(int id)
        {
            ViewBag.IdDoCliente = id;
            return View();
        }

        [HttpPost]
        public ActionResult AnexarFotoPedido(HttpPostedFileBase file)
        {
            try
            {
                int idDoCliente = int.Parse(Request["string"]);
                string nomeDoArquivo = "";
                string caminhoDoArquivo = "";

                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles/reqPhotos"), _FileName);
                    // _FileName = Nome do arquivo
                    // _path = Caminho do arquivo (exemplo: "C:\\Users\\mario\\source\\repos\\freeCommerce\\freeCommerce\\UploadedFiles\\Screenshot_6.png")
                    file.SaveAs(_path);
                    nomeDoArquivo = _FileName;
                    caminhoDoArquivo = _path;
                }
                string dataAgora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var imagemDePedido = new ImagemPedido();
                imagemDePedido.idCliente = idDoCliente;
                imagemDePedido.nomeImagem = nomeDoArquivo;
                imagemDePedido.caminhoImagem = caminhoDoArquivo;
                imagemDePedido.tipoImg = "Imagem de referência";
                imagemDePedido.Save();

                /*var produto = new Item();
                produto.id = 4;
                produto.imgPath = caminhoDoArquivo;
                produto.imgFile = nomeDoArquivo;
                produto.SalvarImg();*/
                ViewBag.Message = "File Uploaded Successfully!!";
                Response.Redirect("/");
                return View();
            }
            catch (Exception err)
            {
                ViewBag.Message = "File upload failed! " + err.Message;
                return View();
            }
        }
    }
}