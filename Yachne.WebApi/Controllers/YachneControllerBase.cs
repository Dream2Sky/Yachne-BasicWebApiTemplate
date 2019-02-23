using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Yachne.WebApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yachne.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YachneControllerBase : Controller
    {
        protected IHttpContextAccessor httpContextAccessor;
        protected IConfiguration configuration;
        protected ILogger logger;
        protected IMemoryCache memoryCache;
        protected YachneContext yachneContext;
        protected AuthManager authManager;

        public YachneControllerBase()
        {
            // 手动获取注入对象
            httpContextAccessor = (IHttpContextAccessor)ServiceLocator.Instance.GetService(typeof(IHttpContextAccessor));
            logger = (ILogger<YachneControllerBase>)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ILogger<YachneControllerBase>));
            configuration = (IConfiguration)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IConfiguration));
            memoryCache = (IMemoryCache)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IMemoryCache));
            yachneContext = (YachneContext)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(YachneContext));
            authManager = (AuthManager)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(AuthManager));
        }
    }
}
