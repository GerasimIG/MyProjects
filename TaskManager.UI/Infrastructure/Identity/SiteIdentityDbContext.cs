using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TaskManager.UI.Infrastructure.Identity
{
    public class SiteIdentityDbContext : IdentityDbContext<SiteUser>
    {
        public SiteIdentityDbContext()
            : base("TaskManagerIdentityDb")
        {
            Database.SetInitializer<SiteIdentityDbContext>(new
            SiteIdentityDbInitializer());
        }
        public static SiteIdentityDbContext Create()
        {
            return new SiteIdentityDbContext();
        }
    }
}