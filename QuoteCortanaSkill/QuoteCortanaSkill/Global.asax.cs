using System.Web;
using System.Web.Http;

namespace QuoteCortanaSkill
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
