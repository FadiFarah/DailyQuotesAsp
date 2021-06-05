using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuoteWebApp.Startup))]
namespace QuoteWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
