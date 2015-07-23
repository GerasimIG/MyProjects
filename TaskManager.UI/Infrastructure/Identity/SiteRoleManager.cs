using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace TaskManager.UI.Infrastructure.Identity
{
    public class SiteRoleManager : RoleManager<SiteRole>
    {

        public SiteRoleManager(IQueryableRoleStore<SiteRole> site) : base(site) { }

        public static SiteRoleManager Create(
                IdentityFactoryOptions<SiteRoleManager> options,
                IOwinContext context)
        {
            return new SiteRoleManager(new
                RoleStore<SiteRole>(context.Get<SiteIdentityDbContext>()));
        }
    }
}
