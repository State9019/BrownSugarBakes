using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrownSugarBakes.UI.MVC.Startup))]
namespace BrownSugarBakes.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
