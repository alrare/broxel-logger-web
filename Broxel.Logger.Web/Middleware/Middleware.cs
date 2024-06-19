using Broxel.Logger.Web.Filter;
using Grpc.Net.Client.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace Broxel.Logger.Web.Middleware
{
    public  class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }
}

