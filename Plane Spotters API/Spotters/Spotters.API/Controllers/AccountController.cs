using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spotters.Service.Interface;
using Spotters.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotters.API.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseAPIController
    {
        protected IAUTHService Service { get; set; }
        private IHttpContextAccessor HttpContextAccessor { get; set; }
        public AccountController(IAUTHService service, IHttpContextAccessor httpContextAccessor)
        {
            this.Service = service;
            this.HttpContextAccessor = httpContextAccessor;
        }

        [HttpPost("signin")]
        public async Task<ActionResult> signin(SignInViewModel agent)
        {
            var ipAddress = HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            TokenViewModel token = await Service.LoginUser(agent, ipAddress).ConfigureAwait(true);
            if (token != null)
            {
                return Ok(token);
            }
            else
            {
                return BadRequest("Failed to signup");
            }
        }
    }
}
