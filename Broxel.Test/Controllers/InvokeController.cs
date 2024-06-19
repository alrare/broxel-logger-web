using Broxel.Logger.Web.Filter;
using Broxel.Test.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Broxel.Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvokeController : ControllerBase
    {
        private readonly ILogger<InvokeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        
        public InvokeController(ILogger<InvokeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [TypeFilter(typeof(FilterLogger))]
        [HttpPost(Name = "GetInvokeService")]
        public async Task<string> GetInvokeService()
        { 
            await InvokeService();           
            return "ApiUno invoca a ApiDos";
        }

        private async Task InvokeService()
        {
            var httpClient = _httpClientFactory.CreateClient();
            //foreach (var header in Request.Headers)
            //{
            //    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
            //}
            _ = await httpClient.GetStringAsync("http://localhost:5181/WeatherForecast");
        }
    }
}