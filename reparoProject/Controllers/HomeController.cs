using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reparoProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Servicos = new Servico().Listar();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public void Filtrar()
        {
            try
            {
                var luthier = new Luthier().FiltrarLuthier(Convert.ToInt32(Request["servico"]));
                List<Luthier> listaLuthiersFiltrada = luthier;
                TempData["luthiersFiltrados"] = listaLuthiersFiltrada;
                TempData["servicosRegistrados"] = new Servico().Listar();
                ViewBag.LuthiersPosFiltro = listaLuthiersFiltrada;
                Response.Redirect("/luthier/encontrar");
            }
            catch (Exception erro)
            {
                TempData["contaNaoCriada"] = "A conta não pode ser criada (" + erro.Message + ")!";
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}