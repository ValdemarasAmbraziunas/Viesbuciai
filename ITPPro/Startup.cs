using Microsoft.Owin;
using Owin;

//[assembly: OwinStartupAttribute(typeof(ITPPro.Startup))]
namespace ITPPro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
