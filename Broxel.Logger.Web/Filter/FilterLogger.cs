using Broxel.Logger.Web.Configuration;
using Google.Api;
using Google.Rpc;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;


namespace Broxel.Logger.Web.Filter
{
    public class FilterLogger : IAsyncActionFilter, IExceptionFilter
    {

        private readonly ILogger<FilterLogger> _logger;
        private readonly ISettings _settings;

        string methodName = string.Empty;
        string displayName = string.Empty;
        string statusCode = string.Empty;
        string message = string.Empty;
        string stackTrace = string.Empty;

        public FilterLogger(ILogger<FilterLogger> logger, ISettings settings)
        {
            _logger = logger;
            _settings = settings;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            methodName = context.HttpContext.Request.Method.ToString();
            displayName = context.ActionDescriptor.DisplayName.ToString();
            statusCode = context.HttpContext.Response.StatusCode.ToString();

            //if (_settings.IsActiveInf == "true")
            //_logger.LogInformation($"START PROCESS =====> {methodName} {displayName}");

            await next();

            //if (_settings.IsActiveInf == "true")
            _logger.LogInformation($"PROCESS =====> {statusCode} {displayName} {methodName} ");
        }


        public void OnException(ExceptionContext context)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Parameters :[");
            foreach (var item in context.ModelState.Keys)
            {
                stringBuilder.Append($"{item}:{context.ModelState[item]?.RawValue} ,");
            }
            stringBuilder.Append("]");
            string parameters = stringBuilder.ToString();


            methodName = context.HttpContext.Request.Method.ToString();
            displayName = context.ActionDescriptor.DisplayName.ToString();
            message = context.Exception.Message.ToString();
            stackTrace = context.Exception.StackTrace.ToString();

            var error = new Error
            {
                StatusCode = 500,
                Message = context.Exception.Message,
                
            };
            context.Result = new JsonResult(error) { StatusCode = 500 };


            if (parameters == "Parameters :[]")
            {
                //if (_settings.IsActiveWar == "true")
                _logger.LogWarning($"WARNING ==========>  {error.StatusCode} {displayName} {methodName} {message}");
                //if (_settings.IsActiveErr == "true")
                _logger.LogError($"EXCEPTION ==========>  {error.StatusCode} {displayName} {methodName} {message} {stackTrace}");
            }
            else
            {
                //if (_settings.IsActiveWar == "true")
                _logger.LogWarning($"WARNING ==========>  {error.StatusCode} {displayName} {methodName} {parameters} {message}");
                //if (_settings.IsActiveErr == "true")
                _logger.LogError($"EXCEPTION ==========>  {error.StatusCode} {displayName} {methodName} {parameters} {message} {stackTrace}");
            }
        }

        public class Error
        {
            public int StatusCode { get; set; }
            public string Message { get; set; } = string.Empty;
        }

        //int code = 0;
        //var statusCode = HttpStatusCode.InternalServerError;

        //if (context.Exception is NotImplementedException)
        //{para
        //    statusCode = HttpStatusCode.NotImplemented;
        //    code = 500;
        //}

        ////context.HttpContext.Response.ContentType = "application/json";
        //context.HttpContext.Response.StatusCode = (int)code;
    }
}
