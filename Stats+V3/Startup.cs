using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stats_V3.Startup))]
namespace Stats_V3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
