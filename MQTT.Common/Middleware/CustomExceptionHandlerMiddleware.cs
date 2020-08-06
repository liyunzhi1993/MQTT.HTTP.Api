using MQTT.API.Common.Enum;
using MQTT.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Common.Middleware
{
    /// <summary>
    /// 自定义异常中间件
    /// </summary>
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var request = context.Request;
            response.ContentType = "application/json: charset=UTF-8";
            if (request.Query.ContainsKey("error"))
            {
                await response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    error = new
                    {
                        message = exception.Message,
                        exception = exception.GetType().Name,
                        source = exception.Source,
                        stack = exception.StackTrace
                    }
                }));
            }
            else
            {
                await response.WriteAsync(JsonConvert.SerializeObject(new BaseOutModel<object>
                {
                    Code=0,
                    Message= "请求参数有误"
                }));
            }
            
        }
    }
}
