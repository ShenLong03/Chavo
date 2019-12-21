using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chavo.ECommerce.Startup))]
namespace Chavo.ECommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
