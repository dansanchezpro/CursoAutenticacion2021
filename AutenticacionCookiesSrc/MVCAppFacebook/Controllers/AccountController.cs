using Microsoft.AspNetCore.Mvc;
using RCLSharedComponents.Controllers;
using RCLSharedComponents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Facebook;
using System.Security.Claims;

namespace MVCAppFacebook.Controllers
{
    public class AccountController : AccountControllerBase
    {
        public IActionResult Login(string returnUrl = "/home")
        {
            return DoExternalLogin(returnUrl, FacebookDefaults.AuthenticationScheme, AuthenticationProvider.Facebook);
        }
        public override async Task<IActionResult> LoginCallback(string returnUrl, AuthenticationProvider authenticationProvider)
        {
            var ExternalUserId = User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).FirstOrDefault();
            return await base.LoginCallback(returnUrl, authenticationProvider);
        }
    }
}
