using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yachne.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YachneControllerBase : ControllerBase
    {
        public IConfiguration configuration;

        public YachneControllerBase(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public YachneControllerBase()
        { }
    }
}
