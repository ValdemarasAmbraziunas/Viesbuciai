
using ITPPro.App_Start;
using ITPPro.Data;
using ITPPro.Models;
using ITPPro.Security;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebMatrix.WebData;

namespace ITPPro
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "DARBUOTOJAI", "darbuojo_kodas", "el_pastas", true);

            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());

            Singleton<ContainerBuilder>.Instance.RegisterType<BaseRepository, BaseRepository>();

        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Darbuotojas serializedModel = serializer.Deserialize<Darbuotojas>(ticket.UserData);
               // Klientas serializedModel2 = serializer.Deserialize <Klientas>(ticket.UserData);
                if (serializedModel != null)
                {
                    CustomPrincipal principal = new CustomPrincipal(serializedModel.el_pastas);
                    principal.UserId = serializedModel.darbuojo_kodas;
                    principal.RoleId = serializedModel.darbuotojo_tipas.id;

                    HttpContext.Current.User = principal;
                }
                
            }
        }
    }
}
