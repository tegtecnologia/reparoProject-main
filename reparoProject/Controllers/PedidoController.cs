using Business;
using System;
using System.Collections.Generic;
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
    }
}