using Business;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections;
using System.Reflection;

namespace freeCommerce.Controllers
{
    public class AutenticacaoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult CadastroCliente()
        {
            return View();
        }

        public ActionResult CadastroLuthier()
        {
            ViewBag.Instrumentos = new Instrumento().Listar();
            ViewBag.Habilidades = new Habilidade().Listar();
            ViewBag.Servicos = new Servico().Listar();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public void Criar()
        {
            try
            {
                var conta = new Conta();
                conta.usuario = Request["usuario"];
                conta.email = Request["email"];
                conta.senha = Request["senha"];
                conta.Save();
                Response.Redirect("/autenticacao/login");
                TempData["contaCriada"] = "Conta criada com sucesso! Agora você já pode fazer seu login abaixo.";
            }
            catch (Exception erro)
            {
                TempData["contaNaoCriada"] = "A conta não pode ser criada (" + erro.Message + ")!";
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public void CriarCliente()
        {
            try
            {
                var conta = new Conta();
                var contaDois = new Conta();
                conta.usuario = Request["txtUsuario"];
                conta.email = Request["txtEmail"];
                conta.senha = Request["txtSenha"];
                conta.Save();

                var ultimaContaCadastrada = Business.Conta.BuscaUltimoIdCadastrado();
                Business.Conta lastAccountCreated = (Conta)ultimaContaCadastrada;
                var cliente = new Cliente();
                cliente.nome = Request["txtNome"];
                cliente.cpf = Request["txtCpf"];
                cliente.email = Request["txtEmail"];
                cliente.usuario = lastAccountCreated.id;
                cliente.Save();

                var ultimoClienteCadastrado = Cliente.BuscaUltimoClienteCadastrado();
                Business.Cliente lastClientCreated = (Cliente)ultimoClienteCadastrado;
                if(Request["txtCelular"] != null)
                {
                    var celular = new Contato();
                    celular.codigo = Request["txtDddCelular"];
                    celular.numero = Request["txtCelular"];
                    celular.celular = 1;
                    celular.idCliente = lastClientCreated.id;
                    celular.SaveCliente();
                }
                else
                {
                }

                if(Request["txtTelefone"] != null)
                {
                    var telefone = new Contato();
                    telefone.codigo = Request["txtDddTelefone"];
                    telefone.numero = Request["txtTelefone"];
                    telefone.celular = 0;
                    telefone.idCliente = lastClientCreated.id;
                    telefone.SaveCliente();
                }
                else
                {
                }

                var endereco = new Endereco();
                endereco.logradouro = Request["txtLogradouro"];
                endereco.bairro = Request["txtBairro"];
                endereco.cidade = Request["txtCidade"];
                endereco.uf = Request["txtUf"];
                endereco.cep = int.Parse(Request["txtCep"]);
                endereco.numero = int.Parse(Request["txtNumero"]);
                endereco.idElemento = lastClientCreated.id;
                endereco.SaveCliente();

                Response.Redirect("/");
                TempData["contaCriada"] = "Conta criada com sucesso! Agora você já pode fazer seu login abaixo.";
            }
            catch (Exception erro)
            {
                TempData["contaNaoCriada"] = "A conta não pode ser criada (" + erro.Message + ")!";
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public void CriarLuthier()
        {
            try
            {
                var conta = new Conta();
                var contaDois = new Conta();
                conta.usuario = Request["txtUsuario"];
                conta.email = Request["txtEmail"];
                conta.senha = Request["txtSenha"];
                conta.Save();

                var ultimaContaCadastrada = Business.Conta.BuscaUltimoIdCadastrado();
                Business.Conta lastAccountCreated = (Conta)ultimaContaCadastrada;
                var luthier = new Luthier();
                luthier.nome = Request["txtNome"];
                luthier.cpf = Request["txtCpf"];
                luthier.cnpj = Request["txtCnpj"];
                luthier.email = Request["txtEmail"];
                luthier.usuario = lastAccountCreated.id;
                luthier.Save();

                var ultimoLuthierCadastrado = Luthier.BuscaUltimoLuthierCadastrado();
                Business.Luthier lastLuthierCreated = (Luthier)ultimoLuthierCadastrado;
                if (Request["txtCelular"] != null)
                {
                    var celular = new Contato();
                    celular.codigo = Request["txtDddCelular"];
                    celular.numero = Request["txtCelular"];
                    celular.celular = 1;
                    celular.idCliente = lastLuthierCreated.id;
                    celular.SaveLuthier();
                }
                else
                {
                }

                if (Request["txtTelefone"] != null)
                {
                    var telefone = new Contato();
                    telefone.codigo = Request["txtDddTelefone"];
                    telefone.numero = Request["txtTelefone"];
                    telefone.celular = 0;
                    telefone.idCliente = lastLuthierCreated.id;
                    telefone.SaveLuthier();
                }
                else
                {
                }

                var endereco = new Endereco();
                endereco.logradouro = Request["txtLogradouro"];
                endereco.bairro = Request["txtBairro"];
                endereco.cidade = Request["txtCidade"];
                endereco.uf = Request["txtUf"];
                endereco.cep = int.Parse(Request["txtCep"]);
                if(Request["txtNumero"] != null)
                {
                    endereco.numero = int.Parse(Request["txtNumero"]);
                }
                else
                {
                    endereco.numero = 0;
                }
                endereco.idElemento = lastLuthierCreated.id;
                endereco.SaveLuthier();

                string habilidadesDoLuthier = Request["habilidadesSelecionadas"];
                JavaScriptSerializer j = new JavaScriptSerializer();
                object a = j.Deserialize(habilidadesDoLuthier, typeof(object));
                IList listaHabilidadesLuthier = a as IList;
                List<object> listaDois = new List<object> { };

                var listaHabilidades = new List<Habilidade>();
                int iterador = 1;
                foreach (var item in listaHabilidadesLuthier)
                {
                    var habilidade = new Habilidade();
                    Dictionary<string, object> dict = new Dictionary<string, object>() { };
                    dict = (Dictionary<string, object>)item;
                    var foos = dict.Values.ToArray();
                    habilidade.id = iterador;
                    habilidade.idInstrumento = Convert.ToInt32(foos[0]);
                    habilidade.idServico = Convert.ToInt32(foos[1]);
                    listaHabilidades.Add(habilidade);
                    iterador = iterador + 1;
                }

                var listaFinal = new List<Habilidade>();
                foreach (var habilidadeFinal in listaHabilidades)
                {
                    var habilidadeLuthier = new Habilidade();
                    habilidadeLuthier = habilidadeLuthier.BuscarPorInstAndServ(habilidadeFinal.idInstrumento, habilidadeFinal.idServico);
                    listaFinal.Add(habilidadeLuthier);
                }

                foreach(var habDoLuthier in listaFinal)
                {
                    var especialidadeLuthier = new HabilidadePorLuthier();
                    especialidadeLuthier.idLuthier = lastLuthierCreated.id;
                    especialidadeLuthier.idHabilidade = habDoLuthier.id;
                    especialidadeLuthier.Salvar();
                }

                Response.Redirect("/");
                TempData["contaCriada"] = "Conta criada com sucesso! Agora você já pode fazer seu login abaixo.";
            }
            catch (Exception erro)
            {
                TempData["contaNaoCriada"] = "A conta não pode ser criada (" + erro.Message + ")!";
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public void Logar()
        {
            try
            {
                var conta = new Conta();
                conta.usuario = Request["usuario"];
                conta.senha = Request["senha"];
                conta.idSessao = Request["idSessao"];
                conta.Login();
                Response.Redirect("/autenticacao/minhaconta");
                TempData["contaLogada"] = "Conta logada com sucesso!";
            }
            catch (Exception erro)
            {
                TempData["contaNaoCriada"] = "A conta não pode ser logada (" + erro.Message + ")!";
            }
        }
        public ActionResult Conta()
        {
            TempData["imagensDeClientes"] = new ImagemCliente().ListarTodas();
            TempData["imagensDeLuthiers"] = new ImagemLuthier().ListarTodas();

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

            foreach(var luthier in luthiersCadastrados)
            {
                if(luthier.usuario == contaLogada.id)
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
                if(cliente.usuario == contaLogada.id)
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
            if(validaTipoConta == false)
            {
                tipoConta = "Luthier";
            }
            else
            {
                tipoConta = "Cliente";
            }

            // Conteúdo transportado para a página HTML
            ViewBag.TipoConta = tipoConta;
            ViewBag.EstadosDeConta = new EstadoConta().Listar();
            if(tipoConta == "Cliente")
            {
                ViewBag.ClienteDaConta = new Cliente().BuscarPorId(contaLogada.id);
                ViewBag.ContatosDaConta = new Contato().BuscaContatosDeClientesPorId(idClienteLogado);
                ViewBag.EnderecosDaConta = new Endereco().BuscaEnderecosPorIdCliente(idClienteLogado);
                ViewBag.Pedidos = new Pedido().BuscarPedidoPorCliente(idClienteLogado);
            }
            else if(tipoConta == "Luthier")
            {
                ViewBag.LuthierDaConta = new Luthier().BuscarPorIdDoUsuario(contaLogada.id);
                ViewBag.ContatosDaConta = new Contato().BuscaContatosDeLuthiersPorId(idLuthierLogado);
                ViewBag.EnderecosDaConta = new Endereco().BuscaEnderecosPorIdLuthier(idLuthierLogado);
            }
            return View();
        }

        public ActionResult Logado()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult UsuarioLogado()
        {
            var conta = Business.Conta.BuscaPorStatusLogin(Session.SessionID);
            Business.Conta contaLogada = (Conta)conta;
            string nomeUsuarioLogado = contaLogada.usuario;
            return new ContentResult { Content = nomeUsuarioLogado };
        }

        [HttpPost]
        public ActionResult Desconecta()
        {
            var conta = new Conta();
            conta.Desconecta(Session.SessionID);
            ViewBag.PosDesconexao = "Foi";
            string message = "Sucesso!";
            return View();
        }
    }
}