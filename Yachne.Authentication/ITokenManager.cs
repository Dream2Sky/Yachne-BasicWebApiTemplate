using System;
using System.Collections.Generic;
using System.Text;
using Yachne.Authentication.Model;

namespace Yachne.Authentication
{
    public interface ITokenManager
    {
        string GetToken<T>(T user, string securityKey, DateTime expireTime) where T : IUser;
    }
}
