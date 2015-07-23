using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using TaskManager.UI.Infrastructure.Identity;

namespace TaskManager.UI.Infrastructure.Identity
{
    public class SiteAuthProvider : OAuthAuthorizationServerProvider
    {

        public override async Task GrantResourceOwnerCredentials(
                OAuthGrantResourceOwnerCredentialsContext context)
        {

            SiteUserManager siteUserMgr =
                context.OwinContext.Get<SiteUserManager>("AspNet.Identity.Owin:"
                    + typeof(SiteUserManager).AssemblyQualifiedName);

            SiteUser user = await siteUserMgr.FindAsync(context.UserName,
                context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant",
                    "The username or password is incorrect");
            }
            else
            {
                ClaimsIdentity ident = await siteUserMgr.CreateIdentityAsync(user,
                        "Custom");
                AuthenticationTicket ticket
                    = new AuthenticationTicket(ident, new AuthenticationProperties());
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(ident);
            }
        }

        public override Task ValidateClientAuthentication(
                OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}
