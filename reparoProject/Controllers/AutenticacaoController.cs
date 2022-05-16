using Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

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
                endereco.idElemento = lastLuthierCreated.id;
                endereco.SaveLuthier();

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
            string message = "Sucesso!";
            return View();
        }
    }
}