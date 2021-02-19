using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLSharedComponents.Policies
{
    public class PoliciesHelper
    {
        public static void AddPolicies(AuthorizationOptions options)
        {
            options.AddPolicy(RolePolicies.Admin,
            RolePolicies.AdminPolicy());
            options.AddPolicy(RolePolicies.Accountant,
            RolePolicies.AccountantPolicy());
            options.AddPolicy(RolePolicies.Seller,
            RolePolicies.SellerPolicy());
        }

    }
}
