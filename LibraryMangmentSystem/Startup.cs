using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryMangmentSystem.Startup))]
namespace LibraryMangmentSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
