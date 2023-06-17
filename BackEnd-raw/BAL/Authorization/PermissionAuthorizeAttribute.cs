using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using BAL.Services.Interfaces;
using Castle.Core.Internal;

namespace BAL.Authorization
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string[] _permissions;
        private const string ACESS_DENIED = "access denied";
        public PermissionAuthorizeAttribute(params string[] permissions)
        {
            _permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var currentController = context.RouteData.Values["Controller"];
                var rightClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == currentController.ToString().ToLower());
                if (rightClaim != null)
                {
                    string permission = rightClaim.Value;
                    if(_permissions.Where(x => x.ToLower().Equals(ACESS_DENIED)).IsNullOrEmpty() == false)
                    {
                        context.Result = new StatusCodeResult(403);
                    }
                    if(_permissions.Where(x => x.ToLower().Equals(permission.ToLower())).IsNullOrEmpty() == false)
                    {
                        return;
                    }
                }
                else
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
            else
            {
                context.Result = new StatusCodeResult(401);
            }


        }
    }
}
