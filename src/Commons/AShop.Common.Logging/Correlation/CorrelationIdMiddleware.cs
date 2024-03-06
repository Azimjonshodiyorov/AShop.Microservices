namespace AShop.Common.Logging.Correlation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string _correlationIdHeader = "X-Correlation-Id";
    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        var correlationId = GetCorrelationId(context, correlationIdGenerator);
        await _next(context);
    }


    private static StringValues GetCorrelationId(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        if (context.Request.Headers.TryGetValue(_correlationIdHeader, out var correlationId))
        {
            correlationIdGenerator.Get();
            return correlationId;
        }
        else
            return correlationIdGenerator.Get();
        
    }


    private static void AddcorrelationIdHeader(HttpContext context, StringValues values)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Add(_correlationIdHeader , new[] {values.ToString()});
            return Task.CompletedTask;
        });
    }
}