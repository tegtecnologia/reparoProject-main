using Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reparoProject.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AnexarFotoCliente()
        {
            return View();
        }

        public ActionResult AnexarPorId(int id)
        {
            ViewBag.IdDoCliente = id;
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AnexarFotoCliente(HttpPostedFileBase file)
        {
            try
            {
                int idDoCliente = int.Parse(Request["string"]);
                string nomeDoArquivo = "";
                string caminhoDoArquivo = "";

                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles/profilePhotos/cliente"), _FileName);
                    // _FileName = Nome do arquivo
                    // _path = Caminho do arquivo (exemplo: "C:\\Users\\mario\\source\\repos\\freeCommerce\\freeCommerce\\UploadedFiles\\Screenshot_6.png")
                    file.SaveAs(_path);
                    nomeDoArquivo = _FileName;
                    caminhoDoArquivo = _path;
                }
                string dataAgora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dataUploadImg = DateTime.Parse(dataAgora);

                var imagemDeCliente = new ImagemCliente();
                imagemDeCliente.idCliente = idDoCliente;
                imagemDeCliente.nomeImagem = nomeDoArquivo;
                imagemDeCliente.caminhoImagem = caminhoDoArquivo;
                imagemDeCliente.tipoImg = "Foto de perfil";
                imagemDeCliente.Save();

                /*var produto = new Item();
                produto.id = 4;
                produto.imgPath = caminhoDoArquivo;
                produto.imgFile = nomeDoArquivo;
                produto.SalvarImg();*/
                ViewBag.Message = "File Uploaded Successfully!!";
                Response.Redirect("/autenticacao/minhaconta");
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