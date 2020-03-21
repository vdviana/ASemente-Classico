using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechnoDemokratia.Startup))]
namespace TechnoDemokratia
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
