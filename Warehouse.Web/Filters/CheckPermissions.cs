using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Warehouse.Web.Filters
{
    public class CheckPermissions : Attribute, IAuthorizationFilter
    {
        private readonly string _permission;
        public CheckPermissions(string permission)
        {
            _permission = permission;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = CheckUserPermisionByClaim(context.HttpContext.User, _permission);
            if (!isAuthorized)
            {
                context.Result = new RedirectResult("~/Identity/Account/AccessDenied");
            }
        }

        private bool CheckUserPermisionByClaim(ClaimsPrincipal user, string permission)
        {
            //TODO: Check in db if (authenticated) user has authorization to this action.
            return user.Claims.Where(c => c.Value == permission).Select(c=>c.Value).Any();
        }
    }
}
