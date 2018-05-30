using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SeregaTest.Startup))]
namespace SeregaTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
