using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chirper_5._0.Startup))]
namespace Chirper_5._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
