using Chavo.Web.Helpers;
using Chavo.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Chavo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            CheckRolesAndSuperUser();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperWebProfile.Run();
        }

        private void CheckRolesAndSuperUser()
        {
            UsersHelper.CheckRole("Administrator");
            UsersHelper.CheckRole("Customer");
            UsersHelper.CheckSuperUser();
        }
    }
}
