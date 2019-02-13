using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pz.Startup))]
namespace pz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
