using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetsDairy.Startup))]
namespace PetsDairy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
