using MediatR;
using Microsoft.Extensions.Logging;

namespace AShop.Order.Application.Behaviour;

public class UnhandledExceptionBehaviour<TRequst ,TResponse> : IPipelineBehavior<TRequst , TResponse> where TRequst : IRequest<TResponse>
{
    private readonly ILogger<TRequst> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequst> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequst request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            var requstName = typeof(TRequst).Name;
            _logger.LogError(e,$"Unhandled Exception Requst name : {requstName} , {request}");
            throw;
        }
    }
}