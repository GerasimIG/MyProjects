using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace TaskManager.UI.Infrastructure.Identity
{
    public class SiteIdentityDbInitializer :
        CreateDatabaseIfNotExists<SiteIdentityDbContext>
    {

        protected override void Seed(SiteIdentityDbContext context)
        {

            SiteUserManager userMgr =
                new SiteUserManager(new UserStore<SiteUser>(context));
            SiteRoleManager roleMgr =
                new SiteRoleManager(new RoleStore<SiteRole>(context));

            string roleName = "ApprovedMembers";
            string userName = "John";
            string password = "111111";
            string email = "jhon@example.com";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new SiteRole(roleName));
            }

            SiteUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new SiteUser
                {
                    UserName = userName,
                    Email = email
                }, password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            base.Seed(context);
        }
    }
}
