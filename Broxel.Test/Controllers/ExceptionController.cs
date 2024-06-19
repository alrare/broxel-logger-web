using Broxel.Logger.Web.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Broxel.Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExceptionController : ControllerBase
    {
        
        private readonly ILogger<ExceptionController> _logger;

        public ExceptionController(ILogger<ExceptionController> logger)
        {
            _logger = logger;
        }



        [TypeFilter(typeof(FilterLogger))]
        [HttpGet(Name = "GetException")]
        //public void GetException()
        public void GetException(int param1, string param2, bool param3, decimal param4)
        {
            throw new NotImplementedException();
        }
    }
}
