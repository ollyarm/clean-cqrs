using CleanCQRS;
using Microsoft.Extensions.Logging;
using ComposedSetup.Core.Interfaces;

namespace ComposedSetup.Core.Common;

public class Pipline<TUnitOfWork, TRequest, TResponse> : IPipeline<TUnitOfWork, TRequest, TResponse>
    where TUnitOfWork : IUnitOfWork
    where TRequest : notnull
{
    private readonly ILogger _logger;

    public Pipline(ILogger<Pipline<TUnitOfWork, TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    private string GetName(Type type)
    {
        var fullName = type.FullName ?? "";
        var prefix = "PiplineSetup.Core.";
        return fullName.StartsWith(prefix) ? fullName[prefix.Length ..] : fullName;
    }

    public async Task<TResponse> Run(TUnitOfWork uow, TRequest request, IRequestHandler<TUnitOfWork, TRequest, TResponse> handler, CancellationToken cancellationToken)
    {
        var start = uow.Clock.UtcNow;
        var scope = new 
        {
            request = GetName(request.GetType()),
            handler = GetName(handler.GetType()),
        };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Running request {Request} with handler {Handler} at {Start}", scope.request, scope.handler, start);
            try
            {
                return await handler.Run(uow, request, cancellationToken);
            }
            catch(Exception ex)
            {

                _logger.LogWarning(ex, "Error running request {Request} with handler {Handler}", scope.request, scope.handler);
                throw;
            }
            finally
            {
                var finish = uow.Clock.UtcNow;
                _logger.LogInformation("Finished request {Request} with handler {Handler} at {Finish} taking {Duration}", scope.request, scope.handler, finish, (finish - start).TotalMilliseconds);
            }
        }
    }
}
