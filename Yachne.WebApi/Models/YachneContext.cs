using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yachne.Authentication.Model;
using Yachne.Core;
using Yachne.Core.Entities;

namespace Yachne.WebApi.Models
{
    public class YachneContext
    {
        protected IHttpContextAccessor httpContextAccessor;
        public YachneContext()
        {
            httpContextAccessor = (IHttpContextAccessor)ServiceLocator.Instance.GetService(typeof(IHttpContextAccessor));
        }

        public IUser UserInfo {
            get {
                return JsonConvert.DeserializeObject<UserEntity>(httpContextAccessor.HttpContext.Session.GetString("UserInfo"));
            }

            set {
                httpContextAccessor.HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(value));
            }
        }

    }
}
