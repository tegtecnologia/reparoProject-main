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
            List<Business.Luthier> listaDoLuthier = luthier;
            var dadosDoLuthier = new Luthier().BuscaDadosPorId(id);
            
            ViewBag.Luthier = luthier;
            ViewBag.DadosDoLuthier = dadosDoLuthier;
            ViewBag.Servicos = new Servico().Listar();
            return View();
        }
    }
}