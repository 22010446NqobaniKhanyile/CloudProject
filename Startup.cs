using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cloud22010446_Dut4life.Startup))]
namespace Cloud22010446_Dut4life
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
