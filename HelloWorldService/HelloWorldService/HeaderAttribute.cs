using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace HelloWorldService
{
    public class HeaderAttribute : Attribute, IActionConstraint
    {
        private readonly string _headerName;
        private readonly string _headerValue;

        public HeaderAttribute(string headerName, string headerValue)
        {
            _headerName = headerName;
            _headerValue = headerValue;
        }

        public bool Accept(ActionConstraintContext context)
        {
            if (!context.RouteContext.HttpContext.Request.Headers.ContainsKey(_headerName))
            {
                return false;
            }

            var headerValue = context.RouteContext.HttpContext.Request.Headers[_headerName];
            return headerValue.Contains(_headerValue);
        }

        public int Order => 0;
    }
}
