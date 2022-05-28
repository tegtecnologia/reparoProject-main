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
            ViewBag.Instrumentos = new Instrumento().Listar();
            return View();
        }

        /*[HttpPost]
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
        }*/

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

        [HttpPost]
        [ValidateInput(false)]
        public void FiltrarInstrumento()
        {
            var idDoInstrumentoBuscado = Convert.ToInt32(Request["instrumento"]);
            var servicosDoInstrumentoBuscado = new Habilidade().ListarPorInstrumento(idDoInstrumentoBuscado);
            var servicos = new Servico().Listar();
            TempData["servicosDoInstrumento"] = servicosDoInstrumentoBuscado;
            TempData["servicos"] = servicos;
            var servico = Request["idServicoFinal"];
            var instrumento = Request["idInstrumentoFinal"];
            TempData["instrumentoEscolhido"] = instrumento;
            TempData["servicoEscolhido"] = servico;

            if(servico == "")
            {
                servico = "0";
            }

            var listaHabilidadesFiltradas = new Habilidade().ListarPorInstrumentoAndServico(servico, instrumento);
            int habilidadeBuscada = 0;
            if(listaHabilidadesFiltradas.Count >= 1)
            {
                habilidadeBuscada = listaHabilidadesFiltradas[0].id;
            }

            var luthiersHabilitados = new HabilidadePorLuthier().ListarPorHabilidade(habilidadeBuscada);

            var vara = new Luthier();
            List<Business.Luthier> luthiersPreparados = new List<Business.Luthier> { };
            foreach (var luthier in luthiersHabilitados)
            {
                var luthierPreparado = new Luthier();
                luthierPreparado.id = luthier.idLuthier;
                luthiersPreparados.Add(luthierPreparado);
            }
            var instrumentoBuscado = "";
            var todosInstrumentos = new Instrumento().Listar();
            foreach(var i in todosInstrumentos)
            {
                if(i.id.ToString() == instrumento)
                {
                    instrumentoBuscado = i.nome;
                }
            }
            var servicoBuscado = "";
            var servicosTodos = new Servico().Listar();
            foreach(var s in servicosTodos)
            {
                if(s.id.ToString() == servico)
                {
                    servicoBuscado = s.descricao;
                }
            }

            TempData["instrumentoBuscado"] = instrumentoBuscado;
            TempData["servicoBuscado"] = servicoBuscado;
            TempData["luthiersPreparados"] = luthiersPreparados;

            if (Request["idServicoFinal"] != "")
            {
                Response.Redirect("/luthier/encontrar");
            }
            else
            {
                TempData["especifiqueServico"] = "Você precisa selecionar um tipo de serviço...";
                Response.Redirect("/");
            }
        }
    }
}