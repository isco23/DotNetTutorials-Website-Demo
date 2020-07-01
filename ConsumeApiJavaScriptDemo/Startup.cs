using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConsumeApiJavaScriptDemo.Startup))]
namespace ConsumeApiJavaScriptDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
