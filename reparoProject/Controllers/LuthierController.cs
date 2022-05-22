using Business;
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
    }
}