using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PrintWayy.SpotWayy.SpotWayyApp.Mapping;

namespace PrintWayy.SpotWayy.SpotWayyApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Configuração do AutoMapper para registrar os profiles de mapeamento
            AutoMapperConfig.RegisterMappings();
        }
    }
}
