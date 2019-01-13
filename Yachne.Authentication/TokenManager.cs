using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Yachne.Authentication.Model;

namespace Yachne.Authentication
{
    public class TokenManager : ITokenManager
    {
        public string GetToken<T>(T user, string securityKey, DateTime expireTime) where T : IUser
        {
            var chaims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: YachneAuthDefaults.User, audience: YachneAuthDefaults.Audience, claims: chaims, expires: expireTime, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
