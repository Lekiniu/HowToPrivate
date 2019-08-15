using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HowToWebApplication.Startup))]
namespace HowToWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
