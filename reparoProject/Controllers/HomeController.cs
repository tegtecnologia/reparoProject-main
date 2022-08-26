using Business;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace reparoProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Instrumentos = new Instrumento().ListarAtivos();
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

            // Encontrando conta logada
            var conta = Business.Conta.BuscaPorStatusLogin(Session.SessionID);
            ViewBag.Conta = conta;
            Business.Conta contaLogada = (Business.Conta)conta;

            if(Convert.ToInt32(instrumento) > 0)
            {
                var lastFiltragem = new UltimaFiltragem();
                lastFiltragem.idUsuario = contaLogada.id;
                lastFiltragem.idUltimoInstrumentoPesq = Convert.ToInt32(instrumento);
                if (servico != "")
                {
                    lastFiltragem.idUltimoServicoPesq = Convert.ToInt32(servico);
                }
                else
                {
                    lastFiltragem.idUltimoServicoPesq = 0;
                }
                lastFiltragem.Save();
            }

            string localizacao = Request["localizacaoUsuario"];
            JavaScriptSerializer j = new JavaScriptSerializer();
            object a = j.Deserialize(localizacao, typeof(object));
            IList localizacaoDoUsuario = a as IList;
            TempData["localizacaoDoUsuario"] = localizacaoDoUsuario;

            if(localizacaoDoUsuario == null)
            {

            }
            else if(localizacaoDoUsuario.Count > 1)
            {
                TempData["latitudeUsuario"] = localizacaoDoUsuario[0];
                TempData["longitudeUsuario"] = localizacaoDoUsuario[1];
            }

            if (servico == "")
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
            int ultimoLuthierEncontrado = 0;
            foreach (var luthier in luthiersHabilitados)
            {
                if (ultimoLuthierEncontrado == luthier.idLuthier)
                {
                }
                else
                {
                    var luthierPreparado = new Luthier();
                    luthierPreparado.id = luthier.idLuthier;
                    luthiersPreparados.Add(luthierPreparado);
                    ultimoLuthierEncontrado = luthier.id;
                }
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