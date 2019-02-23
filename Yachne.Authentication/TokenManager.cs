using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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


        public T ResolveToken<T>(string token) where T : IUser, new()
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                return default(T);
            }

            var jwtToken = handler.ReadJwtToken(token);

            if (jwtToken == null)
            {
                return default(T);
            }
            T t = new T();
            var id = jwtToken.Claims.FirstOrDefault(n => n.Type.Equals(ClaimTypes.NameIdentifier));
            t.Id = id == null ? 0 : int.Parse(id.Value);

            var name = jwtToken.Claims.FirstOrDefault(n => n.Type.Equals(ClaimTypes.Name));
            t.UserName = name == null ? "" : name.Value;

            return t;
        }
    }
}
