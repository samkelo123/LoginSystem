using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Loginsystem2.Startup))]
namespace Loginsystem2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
