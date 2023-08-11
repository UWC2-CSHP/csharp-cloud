using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HelloWorldService
{
    public class LoggingActionFilter : IActionFilter
    {
        private System.Diagnostics.Stopwatch? stopwatch;

        private readonly IWebHostEnvironment env;

        public LoggingActionFilter(IWebHostEnvironment env)
        {
            this.env = env;
        }

        // Called before the action method
        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        // Called after the action method
        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            stopwatch.Stop();

            var webroot = env.ContentRootPath;

            var controllerName = actionExecutedContext.Controller.ToString();
            var controller = (ControllerBase)actionExecutedContext.Controller;
            var actionName = controller.Request.Method;
            
            var filepath = Path.Combine(webroot, "logger.txt");

            var logline = string.Format("{0} : {1} {2} Elapsed={3}\n",
                System.DateTime.Now, controllerName, actionName, stopwatch.Elapsed);

            File.AppendAllText(filepath, logline);
        }
    }
}
