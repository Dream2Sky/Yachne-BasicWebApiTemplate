using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yachne.WebApi.Models;

namespace Yachne.WebApi.Attributes
{
    public class BasicResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.Result is EmptyResult)
            {
                context.Result = new ObjectResult(new BasicResult());
            }
            else if (context.Result is ObjectResult)
            {
                context.Result = new ObjectResult(new BasicResult<object>((context.Result as ObjectResult).Value));
            }
            else if (context.Result is ContentResult)
            {
                context.Result = new ObjectResult(new BasicResult<object>((context.Result as ContentResult).Content));
            }
        }
    }
}
