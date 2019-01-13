using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yachne.WebApi.Models;
using static Yachne.Core.YachneEnums;

namespace Yachne.WebApi.Filters
{
    public class YachneAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.FilterDescriptors.FirstOrDefault(n => n.Filter is AllowAnonymousFilter);
            if (allowAnonymous != null)
            {
                return;
            }
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                var result = new BasicResult();
                result.SetErrorResult((int)WebApiStatusCode.InValidLoginStatus, "登陆异常, 请重新登陆");
                context.Result = new ObjectResult(result);
            }
        }
    }
}
