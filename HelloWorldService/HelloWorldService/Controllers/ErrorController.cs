using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("/error/{code}")]
        public IActionResult Error(int code)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return new ObjectResult(new ApiResponse(code, feature.Error.Message));
        }


        //[Route("{code}")]
        //public IActionResult Error2(int code)
        //{
        //    return new ObjectResult(new ApiResponse(code));
        //}
    }
}