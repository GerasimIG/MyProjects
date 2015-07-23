using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskManager.UI.Infrastructure.Identity
{
    public class SiteRole : IdentityRole 
    {
        public SiteRole() : base() { }
        public SiteRole(string name) : base(name) { }
    }
}