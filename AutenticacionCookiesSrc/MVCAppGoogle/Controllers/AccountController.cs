using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RCLSharedComponents.Controllers;
using Microsoft.AspNetCore.Authentication.Google;
using RCLSharedComponents.Models;
using System.Security.Claims;

namespace MVCAppGoogle.Controllers
{
    public class AccountController : AccountControllerBase
    {
        public IActionResult Login(string returnUrl = "/home")
        {
            return DoExternalLogin(returnUrl, GoogleDefaults.AuthenticationScheme, AuthenticationProvider.Google);
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
