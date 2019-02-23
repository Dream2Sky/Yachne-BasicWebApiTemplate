using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yachne.Core;
using Yachne.Exceptions;
using Yachne.Terminal;

namespace Yachne.WebApi.Controllers
{
    [AllowAnonymous]
    public class ProcessController : YachneControllerBase
    {
        private readonly ITerminalProvider terminalProvider;
        public ProcessController(ITerminalProvider terminalProvider)
        {
            this.terminalProvider = terminalProvider;
        }

        [ActionName("Start")]
        [HttpGet]
        public string Start()
        {
            var pythonPath = string.Empty;
            var pythonHome = Environment.GetEnvironmentVariable("Python_Home");
            if (string.IsNullOrWhiteSpace(pythonHome))
            {
                throw new UserFriendlyException((int)YachneEnums.WebApiStatusCode.GeneralError, "未能找到python运行环境,请设置Python_Home环境变量");
            }
            return terminalProvider.ExecuteCommand($"{pythonHome}\\python.exe", "--version");
        }

    }
}