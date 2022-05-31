using Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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