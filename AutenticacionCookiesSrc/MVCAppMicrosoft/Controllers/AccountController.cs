using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Mvc;
using RCLSharedComponents.Controllers;
using RCLSharedComponents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCAppMicrosoft.Controllers
{
    public class AccountController : AccountControllerBase
    {
        public IActionResult Login(string returnUrl = "/home")
        {
            return DoExternalLogin(returnUrl, MicrosoftAccountDefaults.AuthenticationScheme, AuthenticationProvider.Microsoft);
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
