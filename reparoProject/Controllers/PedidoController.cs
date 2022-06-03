using Business;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/UploadedFiles/reqPhotos"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("Arquivo(s) transferidos com sucesso!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public void RealizarPedido()
        {
            try
            {
                var pedido = new Pedido();
                pedido.idCliente = Convert.ToInt32(Request["idCliente"]);
                pedido.idLuthier = Convert.ToInt32(Request["idLuthier"]);
                pedido.descricao = Request["txtSituacao"];
                pedido.enderecoEntrega = Request["txtEndereco"];
                pedido.statusPedido = 1;
                pedido.instrumentoAlvo = Convert.ToInt32(Request["txtInstrumento"]);
                pedido.tipoServico = Convert.ToInt32(Request["txtServico"]);
                pedido.Save();
   
                var thisPedido = new Pedido().BuscarIdUltimoCadastrado(pedido.idCliente, pedido.idLuthier, pedido.descricao, pedido.enderecoEntrega, pedido.statusPedido, pedido.instrumentoAlvo, pedido.tipoServico);
                int idDoPedidoAtual = 0;

                foreach(var pedidoX in thisPedido)
                {
                    idDoPedidoAtual = pedidoX.id;
                }

                string imagensDoPedido = Request["arrayImagens"];
                JavaScriptSerializer j = new JavaScriptSerializer();
                object a = j.Deserialize(imagensDoPedido, typeof(object));
                IList listaImagens = a as IList;

                string caminhoImg = "";
                foreach (var imagem in listaImagens)
                {
                    var imagemPedido = new ImagemPedido();
                    imagemPedido.idPedido = idDoPedidoAtual;
                    Dictionary<string, object> dict = new Dictionary<string, object>() { };
                    dict = (Dictionary<string, object>)imagem;
                    var foos = dict.Values.ToArray();
                    caminhoImg = (string)foos[0];
                    caminhoImg = Path.Combine(Server.MapPath("~/UploadedFiles/reqPhotos"), caminhoImg);
                    imagemPedido.nomeImagem = (string)foos[0];
                    imagemPedido.caminhoImagem = caminhoImg;
                    imagemPedido.tipoImg = (string)foos[1];
                    imagemPedido.dataUpload = DateTime.Now;
                    imagemPedido.Salvar();
                }

                Response.Redirect("/pedido/visualizar/" + idDoPedidoAtual.ToString());                
                TempData["pedidoCriado"] = "Pedido criado com sucesso!";
            }
            catch (Exception erro)
            {
                TempData["pedidoNaoCriado"] = "O pedido não foi criado (" + erro.Message + ")!";
            }
        }

        public ActionResult PedidoEspec(int id)
        {
            var meuPedido = new Pedido().BuscarPedidoPorId(id);
            var pedidoDef = new Pedido();
            List<Pedido> pedido = meuPedido;
            foreach(var p in pedido)
            {
                pedidoDef.id = p.id;
                pedidoDef.idCliente = p.idCliente;
                pedidoDef.idLuthier = p.idLuthier;
                pedidoDef.descricao = p.descricao;
                pedidoDef.valor = p.valor;
                pedidoDef.dataEmissao = p.dataEmissao;
                pedidoDef.dataUltAtualiza = p.dataUltAtualiza;
                pedidoDef.enderecoEntrega = p.enderecoEntrega;
                pedidoDef.statusPedido = p.statusPedido;
                pedidoDef.obsLuthier = p.obsLuthier;
                pedidoDef.instrumentoAlvo = p.instrumentoAlvo;
                pedidoDef.tipoServico = p.tipoServico;
                pedidoDef.avaliacao = p.avaliacao;
            }

            ViewBag.LuthierResponsavel = new Luthier().BuscaDadosPorId(pedidoDef.idLuthier);
            ViewBag.Instrumento = new Instrumento().ListarPorId(pedidoDef.instrumentoAlvo);
            ViewBag.Servico = new Servico().ListarPorId(pedidoDef.tipoServico);
            ViewBag.ImagensDoPedido = new ImagemPedido().BuscarPorPedido(pedidoDef.id);
            ViewBag.Pedido = pedidoDef;
            ViewBag.StatusPedido = new StatusPedido().ListarPorId(pedidoDef.statusPedido);
            ViewBag.ClienteResponsavel = new Cliente().BuscarPorCliente(pedidoDef.idCliente);
            ViewBag.ObservacoesPedido = new ObsLuthier().ListarPorPedido(pedidoDef.id);

            // Encontrando conta logada
            var conta = Business.Conta.BuscaPorStatusLogin(Session.SessionID);
            ViewBag.Conta = conta;
            Business.Conta contaLogada = (Conta)conta;

            // Diferenciando a conta de cliente para luthier
            var clientesCadastrados = new Cliente().ListarTodosClientes();
            List<Business.Cliente> clientes = clientesCadastrados;

            bool validaTipoConta = false;

            var luthiersCadastrados = new Luthier().ListarTodosLuthiers();
            List<Business.Luthier> luthiers = luthiersCadastrados;

            int idClienteLogado = 0;
            int idLuthierLogado = 0;

            foreach (var luthier in luthiersCadastrados)
            {
                if (luthier.usuario == contaLogada.id)
                {
                    idLuthierLogado = luthier.id;
                    break;
                }
                else
                {
                }
            }
            foreach (var cliente in clientesCadastrados)
            {
                if (cliente.usuario == contaLogada.id)
                {
                    validaTipoConta = true;
                    idClienteLogado = cliente.id;
                    break;
                }
                else
                {
                    validaTipoConta = false;
                }
            }
            string tipoConta = "";
            if (validaTipoConta == false)
            {
                tipoConta = "Luthier";
            }
            else
            {
                tipoConta = "Cliente";
            }

            // Conteúdo transportado para a página HTML
            ViewBag.TipoConta = tipoConta;
            ViewBag.IdLuthierLogado = idLuthierLogado;
            ViewBag.IdClienteLogado = idClienteLogado;
            return View();
        }

        public ActionResult AdicionarObs(string conteudo, int idPedido, int idLuthier)
        {
            var p = new Pedido().BuscarPedidoPorId(idPedido);
            var pedidoAtual = new Pedido();
            if(p != null)
            {
                pedidoAtual = p[0];
                pedidoAtual.Atualiza();
            }

            var o = new ObsLuthier().ListarPorPedido(idPedido);
            if (o.Count == 0)
            {
                if (p != null)
                {
                    pedidoAtual = p[0];
                    pedidoAtual.AtualizaPrimeiraVez();
                }
            }

            JsonResult result = new JsonResult();
            var obs = new ObsLuthier();
            obs.Adicionar(conteudo, idPedido, idLuthier);
            result = this.Json(JsonConvert.SerializeObject(obs), JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult AtualizaStatus(int idPedido, int idStatus)
        {
            var p = new Pedido().BuscarPedidoPorId(idPedido);
            var pedidoAtual = new Pedido();

            if (p != null)
                {
                    pedidoAtual = p[0];
                    pedidoAtual.Atualiza();
                    pedidoAtual.AtualizaStatus(idPedido, idStatus);
                }

            JsonResult result = new JsonResult();
            result = this.Json(JsonConvert.SerializeObject(p[0]), JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}