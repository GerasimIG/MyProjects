using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using TaskManager.UI.Infrastructure.Identity;

[assembly: OwinStartup(typeof(TaskManager.UI.IdentityConfig))]

namespace TaskManager.UI
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<SiteIdentityDbContext>(
                SiteIdentityDbContext.Create);
            app.CreatePerOwinContext<SiteUserManager>(SiteUserManager.Create);
            app.CreatePerOwinContext<SiteRoleManager>(SiteRoleManager.Create);

            //app.UseCookieAuthentication(new CookieAuthenticationOptions {
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            //});    
            
            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                Provider = new SiteAuthProvider(),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Authenticate")
            });
        }


    }
}
