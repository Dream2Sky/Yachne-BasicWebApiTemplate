using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yachne.Application.Account.Dtos;
using Yachne.Authentication;
using Yachne.Core;
using Yachne.EntityFrameworkCore;
using Yachne.Exceptions;
using Yachne.Common.Encrypt;

namespace Yachne.Application.Account
{
    public class AccountServices : IAccountServices
    {
        private YachneDBContext yachneDBContext;
        private ITokenManager tokenManager;
        private IConfiguration configuration;
        public AccountServices(YachneDBContext yachneDBContext, ITokenManager tokenManager, IConfiguration configuration)
        {
            this.yachneDBContext = yachneDBContext;
            this.tokenManager = tokenManager;
            this.configuration = configuration;
        }

        public LoginOutput GetAuthToken(LoginInput input)
        {
            var user = yachneDBContext.UserEntities.FirstOrDefault(n => n.UserName.Equals(input.UserName) && n.IsForbidden == false);
            if (user == null || !user.Password.Equals(EncryptProvider.MD5Encrypt_32(input.Password + user.Salt)))
            {
                throw new UserFriendlyException((int)YachneEnums.WebApiStatusCode.LoginFaild, "用户名密码错误, 登陆失败");
            }
            return new LoginOutput() { UserId = user.Id, AccessToken = tokenManager.GetToken(user, configuration["JWT:SecurityKey"], DateTime.UtcNow.AddMinutes(1)) };
        }
    }
}
