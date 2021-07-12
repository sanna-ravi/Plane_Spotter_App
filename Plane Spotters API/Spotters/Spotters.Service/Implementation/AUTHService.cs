using Microsoft.AspNetCore.Identity;
using Spotters.Data.Models;
using Spotters.Service.Interface;
using Spotters.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Service.Implementation
{
    public class AUTHService : IAUTHService
    {
        private UserManager<ApplicationUser> UserMgr;
        private SignInManager<ApplicationUser> SignMgr;
        private ITokenService TService;
        public AUTHService(UserManager<ApplicationUser> userMgr, SignInManager<ApplicationUser> signMgr, ITokenService tService)
        {
            this.UserMgr = userMgr;
            this.SignMgr = signMgr;
            this.TService = tService;
        }

        public async Task<TokenViewModel> LoginUser(SignInViewModel creds, String IPAddress)
        {
            if (creds == null)
            {
                return null;
            }

            try
            {
                var user = await UserMgr.FindByNameAsync(creds.Username).ConfigureAwait(true);
                if (user != null)
                {
                    var result = await SignMgr.PasswordSignInAsync(creds.Username, creds.Password, false, false).ConfigureAwait(true);
                    if (result.Succeeded)
                    {
                        var roles = (await UserMgr.GetRolesAsync(user).ConfigureAwait(true));
                        String role = roles?.Count > 0 ? roles[0] : "" ?? "";
                        return await TService.GenerateToken(user.Id, user.FirstName, role, IPAddress).ConfigureAwait(true);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
