using Business;
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

                Response.Redirect("/");
                TempData["pedidoCriado"] = "Pedido criado com sucesso!";
            }
            catch (Exception erro)
            {
                TempData["pedidoNaoCriado"] = "O pedido não foi criado (" + erro.Message + ")!";
            }
        }
    }
}