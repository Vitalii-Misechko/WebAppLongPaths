using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppLongPaths.Startup))]
namespace WebAppLongPaths
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
