using Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reparoProject.Controllers
{
    public class LuthierController : Controller
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

        public ActionResult Filtrados()
        {
            return View();
        }

        public ActionResult AnexarFotoLuthier()
        {
            return View();
        }

        public ActionResult AnexarPorId(int id)
        {
            ViewBag.IdDoLuthier = id;
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AnexarFotoLuthier(HttpPostedFileBase file)
        {
            try
            {
                int idDoLuthier = int.Parse(Request["string"]);
                string nomeDoArquivo = "";
                string caminhoDoArquivo = "";

                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles/profilePhotos/luthier"), _FileName);
                    // _FileName = Nome do arquivo
                    // _path = Caminho do arquivo (exemplo: "C:\\Users\\mario\\source\\repos\\freeCommerce\\freeCommerce\\UploadedFiles\\Screenshot_6.png")
                    file.SaveAs(_path);
                    nomeDoArquivo = _FileName;
                    caminhoDoArquivo = _path;
                }
                string dataAgora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dataUploadImg = DateTime.Parse(dataAgora);

                var imagemLuthier = new ImagemLuthier();
                imagemLuthier.idLuthier = idDoLuthier;
                imagemLuthier.nomeImagem = nomeDoArquivo;
                imagemLuthier.caminhoImagem = caminhoDoArquivo;
                imagemLuthier.tipoImg = "Foto de perfil";
                imagemLuthier.Save();
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

        public ActionResult Perfil(int id)
        {
            var luthier = new Luthier().BuscarPorId(id);
            var dadosDoLuthier = new Luthier().BuscaDadosPorId(id);

            ViewBag.Instrumentos = new Instrumento().Listar();
            ViewBag.Habilidades = new Habilidade().Listar();
            ViewBag.HabilidadesDoLuthier = new HabilidadePorLuthier().ListarPorLuthier(id);
            ViewBag.Servicos = new Servico().Listar();
            ViewBag.Luthier = luthier;
            ViewBag.DadosDoLuthier = dadosDoLuthier;
            return View();
        }

        public ActionResult EditarEndereco(int idLuthier, int idEndereco, string cep, string numero, string logradouro, string bairro, string cidade, string uf)
        {
            var endereco = new Endereco();
            endereco.AtualizarLuthier(idLuthier, idEndereco, cep, numero, logradouro, bairro, cidade, uf);

            JsonResult result = new JsonResult();
            result = this.Json(JsonConvert.SerializeObject(endereco), JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}