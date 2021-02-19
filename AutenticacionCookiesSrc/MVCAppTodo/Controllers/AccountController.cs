using Microsoft.AspNetCore.Mvc;
using RCLSharedComponents.Controllers;
using RCLSharedComponents.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Twitter;
using RCLSharedComponents.Models;
namespace MVCAppTodo.Controllers
{
    public class AccountController : AccountControllerBase
    {
        [HttpGet]
        public IActionResult Login(string returnUrl = "/home")
        {
            var Model = new UserCredentials
            {
                ReturnUrl = returnUrl,
                ShowExternalLogin = true
            };
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserCredentials credentials, string button)
        {
            IActionResult Result = View();
            if (button == "login")
            {
                Result = await DoLocalLogin(credentials);
            }
            else
            {
                string AuthenticationScheme = GoogleDefaults.AuthenticationScheme;
                AuthenticationProvider AuthenticationProvider = AuthenticationProvider.Google;
                switch (button)
                {
                    case "Facebook":
                        AuthenticationScheme = FacebookDefaults.AuthenticationScheme;
                        AuthenticationProvider = AuthenticationProvider.Facebook;
                        break;
                    case "Microsoft":
                        AuthenticationScheme = MicrosoftAccountDefaults.AuthenticationScheme;
                        AuthenticationProvider = AuthenticationProvider.Microsoft;
                        break;
                    case "Twitter":
                        AuthenticationScheme = TwitterDefaults.AuthenticationScheme;
                        AuthenticationProvider = AuthenticationProvider.Twitter;
                        break;
                }
                Result = DoExternalLogin(credentials.ReturnUrl, AuthenticationScheme, AuthenticationProvider);
            }
            return Result;
        }
    }
}
