using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace TaskManager.UI.Infrastructure.Identity
{
    public class SiteUserManager : UserManager<SiteUser>
    {

        public SiteUserManager(IUserStore<SiteUser> store)
            : base(store) { }

        public static SiteUserManager Create(
                IdentityFactoryOptions<SiteUserManager> options,
                IOwinContext context)
        {

            SiteIdentityDbContext dbContext = context.Get<SiteIdentityDbContext>();
            SiteUserManager manager =
                new SiteUserManager(new UserStore<SiteUser>(dbContext));
            return manager;
        }
    }
}
