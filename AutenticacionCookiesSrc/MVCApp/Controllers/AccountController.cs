using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RCLSharedComponents.Controllers;
using RCLSharedComponents.ViewModels;

namespace MVCApp.Controllers
{
    public class AccountController : AccountControllerBase
    {
        public IActionResult Login(string returnUrl)
        {
            var Model = new UserCredentials { ReturnUrl = returnUrl };
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserCredentials credentials)
        {
            return await DoLocalLogin(credentials);
        }
    }
}
