using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Data.Models.DBContext
{
    public class AUTHIdentityInitializer
    {
        private UserManager<ApplicationUser> UserMgr;
        private RoleManager<IdentityRole> RoleMgr;

        public AUTHIdentityInitializer(UserManager<ApplicationUser> userMgr,
            RoleManager<IdentityRole> roleMgr)
        {
            this.UserMgr = userMgr;
            this.RoleMgr = roleMgr;
        }

        public async Task Seed()
        {

            if (!(await RoleMgr.RoleExistsAsync("Administrator").ConfigureAwait(true)))
            {
                var role = new IdentityRole("Administrator");
                await RoleMgr.CreateAsync(role).ConfigureAwait(true);
            }

            if (!(await RoleMgr.RoleExistsAsync("Regular").ConfigureAwait(true)))
            {
                var role = new IdentityRole("Regular");
                await RoleMgr.CreateAsync(role).ConfigureAwait(true);
            }

            if ((await UserMgr.FindByNameAsync("admin1").ConfigureAwait(true)) == null)
            {
                ApplicationUser authUser = new ApplicationUser()
                {
                    UserName = "admin1@gmail.com",
                    Email = "admin1@gmail.com",
                    FirstName = "Admin1",
                    LastName = ""
                };
                //Abcd-1234567
                IdentityResult result = await UserMgr.CreateAsync(authUser, "3IW5AtFst13f").ConfigureAwait(true);

                IdentityResult roleResult = await UserMgr.AddToRoleAsync(authUser, "Regular").ConfigureAwait(true);

                if (!result.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to seed user and roles");
                }
            }
        }
    }
}
