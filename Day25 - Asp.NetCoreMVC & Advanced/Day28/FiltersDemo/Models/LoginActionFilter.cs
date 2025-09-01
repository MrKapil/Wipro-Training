using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

public class LoginActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"[Log] Executing: {context.ActionDescriptor.DisplayName}");
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"[Log] Executed: {context.ActionDescriptor.DisplayName}");
    }
}