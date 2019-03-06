using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCwithBasicAuth.Startup))]
namespace MVCwithBasicAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
