using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tryMVC.Startup))]
namespace tryMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
