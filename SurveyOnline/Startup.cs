using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SurveyOnline.Startup))]
namespace SurveyOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
