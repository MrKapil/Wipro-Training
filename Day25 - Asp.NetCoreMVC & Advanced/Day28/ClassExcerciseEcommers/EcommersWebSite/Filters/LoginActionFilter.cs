using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace EcommerceApp.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Session.GetString("username") ?? "anonymous";
            Debug.WriteLine($"[Log] ▶ Executing: {context.ActionDescriptor.DisplayName} (user: {user})");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var user = context.HttpContext.Session.GetString("username") ?? "anonymous";
            Debug.WriteLine($"[Log] ◀ Executed: {context.ActionDescriptor.DisplayName} (user: {user})");
        }
    }
}
