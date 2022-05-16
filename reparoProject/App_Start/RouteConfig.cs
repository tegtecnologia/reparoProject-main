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
                new { controller = "Home", action = "Filtrar" }
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
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
