using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yachne.Authentication;
using Yachne.Core.Entities;
using Yachne.WebApi.Models;
using static Yachne.Core.YachneEnums;

namespace Yachne.WebApi.Filters
{
    public class YachneAuthorizationFilter : IAuthorizationFilter
    {
        public ITokenManager tokenManager;
        public YachneContext yachneContext;
        public AuthManager authManager;
        public YachneAuthorizationFilter(YachneContext yachneContext, AuthManager authManager, ITokenManager tokenManager)
        {
            this.yachneContext = yachneContext;
            this.tokenManager = tokenManager;
            this.authManager = authManager;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.FilterDescriptors.FirstOrDefault(n => n.Filter is AllowAnonymousFilter);
            if (allowAnonymous != null)
            {
                return;
            }

            var result = new BasicResult();
            var token = string.Empty;
            UserEntity userInfo = null;

            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                userInfo = tokenManager.ResolveToken<UserEntity>(token);
            }

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                result.SetErrorResult((int)WebApiStatusCode.InValidLoginStatus, "登陆异常, 请重新登陆");
                context.Result = new ObjectResult(result);

                if (userInfo != null && authManager.TokenDict.ContainsKey(userInfo.Id))
                {
                    // 清理过期token
                    authManager.TokenDict.Remove(userInfo.Id);
                }
            }
            else
            {
                if (!authManager.TokenDict.ContainsKey(userInfo.Id) ||
                    (authManager.TokenDict.ContainsKey(userInfo.Id) && !authManager.TokenDict[userInfo.Id].Equals(token)))
                {
                    result.SetErrorResult((int)WebApiStatusCode.InValidLoginStatus, "登陆异常，请重新登陆");
                    context.Result = new ObjectResult(result);
                }
                else
                {
                    yachneContext.UserInfo = userInfo;
                }
            }
        }
    }
}
