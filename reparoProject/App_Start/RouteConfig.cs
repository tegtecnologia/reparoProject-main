using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace reparoProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "autenticacao-cadastro",
                "autenticacao/cadastro",
                new { controller = "Autenticacao", action = "Cadastro" }
            );

            routes.MapRoute(
                "autenticacao-cadastro-cliente",
                "autenticacao/cadastro/cliente",
                new { controller = "Autenticacao", action = "CadastroCliente" }
            );

            routes.MapRoute(
                "autenticacao-cadastro-luthier",
                "autenticacao/cadastro/luthier",
                new { controller = "Autenticacao", action = "CadastroLuthier" }
            );

            routes.MapRoute(
                "autenticacao-criar",
                "autenticacao/criar",
                new { controller = "Autenticacao", action = "Criar" }
            );

            routes.MapRoute(
                "filtrar-luthier",
                "filtrar",
                new { controller = "Home", action = "FiltrarInstrumento" }
            );

            routes.MapRoute(
                "autenticacao-logar",
                "autenticacao/logar",
                new { controller = "Autenticacao", action = "Logar" }
            );

            routes.MapRoute(
                "autenticacao-logado",
                "autenticacao/logado",
                new { controller = "Autenticacao", action = "Logado" }
            );

            routes.MapRoute(
                "autenticacao-conta",
                "autenticacao/minhaconta",
                new { controller = "Autenticacao", action = "Conta" }
            );

            routes.MapRoute(
                "autenticacao-desconecta",
                "autenticacao/desconecta",
                new { controller = "Autenticacao", action = "Desconecta" }
            );

            routes.MapRoute(
                "autenticacao-login",
                "autenticacao/login",
                new { controller = "Autenticacao", action = "Login" }
            );

            routes.MapRoute(
                "luthier-encontrar",
                "luthier/encontrar",
                new { controller = "Luthier", action = "Filtrados" }
            );

            routes.MapRoute(
                "luthier-perfil",
                "luthier/perfil/{id}",
                new { controller = "Luthier", action = "Perfil" }
            );

            routes.MapRoute(
                "cliente-anexar-porId",
                "cliente/{id}/anexar",
                new { controller = "Cliente", action = "AnexarPorId", id = 0 }
            );

            routes.MapRoute(
                "luthier-anexar-porId",
                "luthier/{id}/anexar",
                new { controller = "Luthier", action = "AnexarPorId", id = 0 }
            );

            routes.MapRoute(
                "admin-portal",
                "admin/portal",
                new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
                "admin-instrumentos",
                "admin/instrumentos",
                new { controller = "Admin", action = "Instrumento" }
            );

            routes.MapRoute(
                "admin-criar-instrumento",
                "admin/cadastro/criarinstrumento",
                new { controller = "Admin", action = "CriarInstrumento" }
            );

            routes.MapRoute(
                "admin-cadastro-servico",
                "admin/cadastro/servico",
                new { controller = "Admin", action = "Servico" }
            );

            routes.MapRoute(
                "admin-cadastro-habilidade",
                "admin/cadastro/habilidade",
                new { controller = "Admin", action = "Habilidade" }
            );

            routes.MapRoute(
                "pedido-solicitar",
                "pedido/solicitar",
                new { controller = "Pedido", action = "Solicitar" }
            );

            routes.MapRoute(
                "pedido-criar",
                "pedido/criar",
                new { controller = "Pedido", action = "CriarPedido" }
            );

            routes.MapRoute(
                "pedido-gerenciamento",
                "pedido/visualizar/{id}",
                new { controller = "Pedido", action = "PedidoEspec"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
