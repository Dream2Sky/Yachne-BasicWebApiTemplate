using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Yachne.Application.Account;
using Yachne.Application.Account.Dtos;
using Yachne.Common;
using Yachne.Common.Captcha;
using Yachne.Core;
using Yachne.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yachne.WebApi.Controllers
{
    [AllowAnonymous]
    public class AuthController : YachneControllerBase
    {
        private IAccountServices accountServices;

        public AuthController(IAccountServices accountServices)
        {
            this.accountServices = accountServices;
        }

        [ActionName("Login")]
        [HttpPost]
        public LoginOutput Login(LoginInput input)
        {
            if (input == null)
            {
                throw new UserFriendlyException((int)YachneEnums.WebApiStatusCode.InValidInput, $"输入参数不正确");
            }

            // Todo: 校验验证码是否正确
            var isEnableCaptcha = bool.Parse(configuration["EnableCaptcha"]);
            if (isEnableCaptcha)
            {
                var captcha = memoryCache.Get(YachneConsts.CaptchaRegionKey + input.CaptchaId);
                if (captcha == null)
                {
                    throw new UserFriendlyException((int)YachneEnums.WebApiStatusCode.CaptchaExpired, $"验证码已过期");
                }

                if (!captcha.ToString().Equals(input.Captcha, StringComparison.OrdinalIgnoreCase))
                {
                    throw new UserFriendlyException((int)YachneEnums.WebApiStatusCode.InValidCaptcha, $"验证码错误");
                }
            }
            var token = accountServices.GetAuthToken(input);
            if (token != null && !string.IsNullOrWhiteSpace(token.AccessToken))
            {
                // 获取token成功之后，覆盖原有相同key的token， 保证当前用户只有唯一有效token
                // 实现同一用户互踢的效果
                authManager.TokenDict[token.UserId] = token.AccessToken;
            }

            return token;
        }

        [ActionName("GetCaptcha")]
        [HttpGet]
        public FileResult GetCaptcha(string captchaId, int width = 100, int height = 50)
        {
            var code = Utils.GetRandomString(4, true);
            memoryCache.Set(YachneConsts.CaptchaRegionKey + captchaId, code, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            var captcha = CaptchaProvider.CreateVerifyCode(code, width, height);

            return new FileContentResult(captcha, "image/png");
        }
    }
}
