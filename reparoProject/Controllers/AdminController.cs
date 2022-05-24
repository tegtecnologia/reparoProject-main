using Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reparoProject.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Instrumento()
        {
            ViewBag.Instrumentos = new Instrumento().Listar();
            ViewBag.Habilidades = new Habilidade().Listar();
            ViewBag.Servicos = new Servico().Listar();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public void CriarInstrumento()
        {
            try
            {
                var instrumento = new Instrumento();
                instrumento.nome = Request["txtNome"];
                instrumento.Save();

                var lastInstrumento = Business.Instrumento.BuscaUltimoCadastrado();
                Business.Instrumento ultimoInstrumentoCadastrado = (Instrumento)lastInstrumento;

                string servicosDoLuthier = Request["servicosSelecionados"];
                string[] listaServicos = new string[] { "" };
                listaServicos = servicosDoLuthier.Split(',');

                foreach (var servico in listaServicos)
                {
                    var habilidade = new Habilidade();
                    habilidade.idInstrumento = ultimoInstrumentoCadastrado.id;
                    habilidade.idServico = Convert.ToInt32(servico);
                    habilidade.Criar();
                }

                Response.Redirect("/admin/instrumentos");
                TempData["instrumentoCadastrado"] = "O instrumento foi cadastrado com sucesso!";
            }
            catch (Exception erro)
            {
                TempData["instrumentoNaoCadastrado"] = "O instrumento não pôde ser cadastrado! (" + erro.Message + ")!";
            }
        }

            public ActionResult Servico()
        {
            return View();
        }

        public ActionResult Habilidade()
        {
            return View();
        }

        public ActionResult ExcluirInstrumento(int idInstrumento)
        {
            JsonResult result = new JsonResult();

            var instrumentoQueSeraRemovido = new Instrumento().ListarPorId(idInstrumento);

            var habsAEliminar = new Habilidade().ListarPorInstrumento(idInstrumento);
            foreach(var hab in habsAEliminar)
            {
                var habEliminarEmLuthiers = new HabilidadePorLuthier();
                habEliminarEmLuthiers.idHabilidade = hab.id;
                habEliminarEmLuthiers.Deletar();
            }

            var habilidadesDoInstrumento = new Habilidade();
            habilidadesDoInstrumento.idInstrumento = idInstrumento;
            habilidadesDoInstrumento.Deletar();

            var instrumento = new Instrumento();
            instrumento.id = idInstrumento;
            instrumento.DeletarPorId();

            result = this.Json(JsonConvert.SerializeObject(instrumentoQueSeraRemovido), JsonRequestBehavior.AllowGet);
            
            return result;
        }
    }
}