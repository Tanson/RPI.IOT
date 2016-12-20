using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPI.Web.Startup))]
namespace RPI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
