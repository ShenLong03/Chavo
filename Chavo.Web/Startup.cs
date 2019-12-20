using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chavo.Web.Startup))]
namespace Chavo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
