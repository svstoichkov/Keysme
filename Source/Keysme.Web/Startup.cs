using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Keysme.Web.Startup))]

namespace Keysme.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
