using System;
using System.Collections.Generic;
using System.Text;
using Yachne.Application.Account.Dtos;

namespace Yachne.Application.Account
{
    public interface IAccountServices
    {
        LoginOutput GetAuthToken(LoginInput input);
    }
}
