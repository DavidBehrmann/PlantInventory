using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlantInventory.MVC.Startup))]
namespace PlantInventory.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
