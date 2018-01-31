using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Southwind.NetMvcClient.Startup))]
namespace Southwind.NetMvcClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
