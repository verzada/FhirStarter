using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FHIRLight.Library.Spark.Engine.Extensions;

namespace FHIRLight.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //protected void Application_Start()
        //{
        //    GlobalConfiguration.Configure(WebApiConfig.Register);
        //}

        protected void Application_Start()
        {
        
            GlobalConfiguration.Configure(this.Configure);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void Configure(HttpConfiguration config)
        {
          //  UnityConfig.RegisterComponents(config);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            config.AddFhir();
        }
    }
}
