using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Calculator
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
          //  this.RegisterCustomControllerFactory();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void RegisterCustomControllerFactory()
        {
            IControllerFactory factory = new CustomControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
        }

    }
}