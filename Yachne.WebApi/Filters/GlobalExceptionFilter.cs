using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yachne.Exceptions;
using Yachne.WebApi.Models;
using static Yachne.Core.YachneEnums;

namespace Yachne.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        protected ILogger logger;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var result = new BasicResult();
            if (context.Exception is TimeoutException)
            {
                result.SetErrorResult((int)WebApiStatusCode.TimeOut, "接口超时");
            }
            else if (context.Exception is UserFriendlyException)
            {
                var exception = context.Exception as UserFriendlyException;
                result.SetErrorResult(exception.StatusCode, exception.Message);
            }
            else
            {
                result.SetErrorResult((int)WebApiStatusCode.GeneralError, context.Exception.Message);
            }
            context.Result = new ObjectResult(result);
            Task.Run(() =>
            {
                logger.LogError(context.Exception, context.Exception.Message);
            });
            
        }
    }
}
