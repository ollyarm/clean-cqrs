using CleanCQRS;
using Microsoft.Extensions.Logging;
using IsolatedSetup.Core.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace IsolatedSetup.Core.Common;

public class Pipline<TUnitOfWork, TRequest, TResponse> : IPipeline<TUnitOfWork, TRequest, TResponse>
    where TUnitOfWork : ICommonUnitOfWork
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
        var prefix = "IsolatedSetup.Core.";
        return fullName.StartsWith(prefix) ? fullName[prefix.Length..] : fullName;
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
            switch(request)
            {
                case IQuery<TResponse>:
                    _logger.LogInformation("Running Query {Request} with handler {Handler} at {Start}", scope.request, scope.handler, start);
                    _logger.LogInformation("Query Body {Query}", JsonSerializer.Serialize(request));
                    break;
                case ICommand<TResponse>:
                    _logger.LogInformation("Running Command {Request} with handler {Handler} at {Start}", scope.request, scope.handler, start);
                    break;
            }
            try
            {
                return await handler.Run(uow, request, cancellationToken);
            }
            catch (Exception ex)
            {

                _logger.LogWarning(ex, "Error running Request {Request} with handler {Handler}", scope.request, scope.handler);
                throw;
            }
            finally
            {
                var finish = uow.Clock.UtcNow;
                _logger.LogInformation("Finished Request {Request} with handler {Handler} at {Finish} taking {Duration}", scope.request, scope.handler, finish, (finish - start).TotalMilliseconds);
            }
        }
    }
}
