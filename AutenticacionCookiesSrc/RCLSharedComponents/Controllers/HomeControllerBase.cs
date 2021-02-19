using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCLSharedComponents.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLSharedComponents.Controllers
{
    public class HomeControllerBase : Controller
    {
        public IActionResult GetData()
        {
            return View("ShowMessage", "This is the Public Data!!!");
        }

        [Authorize(Policy = RolePolicies.Admin)]
        public IActionResult GetAdminData()
        {
            return View("ShowMessage", "This is the Admin Data!!!");
        }

        [Authorize(Policy = RolePolicies.Accountant)]
        public IActionResult GetAccountantData()
        {
            return View("ShowMessage", "This is the Accountant Data!!!");
        }

        [Authorize(Policy = RolePolicies.Seller)]
        public IActionResult GetSellerData()
        {
            return View("ShowMessage", "This is the Seller Data!!!");
        }
    }
}
