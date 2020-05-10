using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(newdip.Startup))]
namespace newdip
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
