namespace ebay.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

public class XssProtectionFilter : ActionFilterAttribute
{
    // Regex detect <script> + các lệnh nguy hiểm
    private static readonly Regex _xssRegex = new(
        @"<\s*script[^>]*>(.*?)<\s*/\s*script\s*>",
        RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline
    );

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // 1. Check Query String
        foreach (var q in context.HttpContext.Request.Query)
        {
            if (IsDangerous(q.Value)) 
                Block(context, q.Key);
        }

        // 2. Check Route Values
        foreach (var routeVal in context.RouteData.Values)
        {
            if (routeVal.Value != null && IsDangerous(routeVal.Value.ToString()))
                Block(context, routeVal.Key);
        }

        // 3. Check Headers
        foreach (var header in context.HttpContext.Request.Headers)
        {
            if (IsDangerous(header.Value))
                Block(context, header.Key);
        }

        // 4. Check Body (FromBody data)
        foreach (var arg in context.ActionArguments)
        {
            if (arg.Value is string s)
            {
                if (IsDangerous(s))
                    Block(context, arg.Key);
            }
            else if (arg.Value != null)
            {
                var json = System.Text.Json.JsonSerializer.Serialize(arg.Value);
                if (IsDangerous(json))
                    Block(context, arg.Key);
            }
        }

        base.OnActionExecuting(context);
    }

    private bool IsDangerous(string? input)
    {
        if (string.IsNullOrEmpty(input)) return false;

        // Match <script>... AND window.location / eval / onerror ...
        if (_xssRegex.IsMatch(input)) return true;

        var lower = input.ToLower();

        if (lower.Contains("window.location") ||
            lower.Contains("javascript:") ||
            lower.Contains("onerror=") ||
            lower.Contains("<iframe") ||
            lower.Contains("document.cookie"))
        {
            return true;
        }

        return false;
    }

    private void Block(ActionExecutingContext context, string field)
    {
        context.Result = new BadRequestObjectResult(new
        {
            message = $"Dữ liệu bị từ chối vì có dấu hiệu tấn công XSS tại: {field}"
        });
    }
}
