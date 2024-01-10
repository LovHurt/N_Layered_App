using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Core.CrossCuttingConcerns.Logging
{

    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Log.Information($"Executing {context.ActionDescriptor.DisplayName} with parameters {string.Join(", ", context.ActionArguments)}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Log.Information($"Executed {context.ActionDescriptor.DisplayName}");

            if (context.Exception != null)
            {
                Log.Error($"Exception occurred: {context.Exception}");
            }          
        }
    }
}
