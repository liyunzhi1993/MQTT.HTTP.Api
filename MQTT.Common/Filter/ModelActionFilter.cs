using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTT.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MQTT.API.Common.Filter
{
    public class ModelActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments != null && context.ActionArguments.Count > 0)
            {
                var modelState = context.ModelState;
                if (!modelState.IsValid)
                {
                    var error = string.Empty;
                    foreach (var key in modelState.Keys)
                    {
                        var state = modelState[key];
                        if (state.Errors.Any())
                        {
                            error = state.Errors.First().ErrorMessage;
                            break;
                        }
                    }
                    context.Result = new JsonResult(new BaseOutModel<object>
                    {
                        Code = 0,
                        Message = error
                    });
                }
            }
        }
    }
}
