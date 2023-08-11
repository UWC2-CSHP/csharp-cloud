using Microsoft.AspNetCore.Mvc;

namespace HelloWorldService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("{code}")]
        public IActionResult Error(int code) => new ObjectResult(new ApiResponse(code));

        //[Route("{code}")]
        //public IActionResult Error2(int code)
        //{
        //    return new ObjectResult(new ApiResponse(code));
        //}
    }
}