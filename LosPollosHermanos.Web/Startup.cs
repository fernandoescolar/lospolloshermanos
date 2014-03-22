using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LosPollosHermanos.Web.Startup))]
namespace LosPollosHermanos.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
