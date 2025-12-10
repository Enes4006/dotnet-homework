using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
options.Filters.Add(typeof(GlobalExceptionFilter));
options.Filters.Add(typeof(ActionTimingFilter));
}).AddJsonOptions(o =>
{
o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();


// ========== MIDDLEWARE ==========

//Middleware, istemciden gelen her HTTP isteðinin ve dönen yanýtýn içinden geçtiði iþleme modülleridir. Ýstek kontrollere ulaþmadan önce veya controllerdan çýktýktan sonra çalýþarak logging, hata yönetimi, doðrulama gibi görevleri yerine getirir.
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    public RequestResponseLoggingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var method = context.Request.Method;
        var path = context.Request.Path;
        var time = DateTime.UtcNow;
        Console.WriteLine($"[Request] {method} {path} at {time:O}");
        await _next(context);
        Console.WriteLine($"[Response] StatusCode={context.Response.StatusCode}");
    }
}

// ========== FILTERS ==========

public class ActionTimingFilter : IActionFilter
{
    private const string StopwatchKey = "__sw__";
    public void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.Items[StopwatchKey] = System.Diagnostics.Stopwatch.StartNew();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var sw = context.HttpContext.Items[StopwatchKey] as System.Diagnostics.Stopwatch;
        if (sw != null)
        {
            sw.Stop();
            Console.WriteLine($"Action {context.ActionDescriptor.DisplayName} took {sw.ElapsedMilliseconds} ms");
        }
    }
}

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Console.WriteLine("GlobalExceptionFilter caught: " + context.Exception.Message);
        var problem = new
        {
            Title = "An error occurred",
            Detail = context.Exception.Message,
            Status = 500
        };
        context.Result = new ObjectResult(problem) { StatusCode = 500 };
        context.ExceptionHandled = true;
    }
}

