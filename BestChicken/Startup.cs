using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BestChicken.Startup))]
namespace BestChicken
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
