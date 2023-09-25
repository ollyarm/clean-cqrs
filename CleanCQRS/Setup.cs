using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanCQRS;

using Handlers;

public static class Setup
{
    public static IServiceCollection AddCleanCQRS(this IServiceCollection services, params Assembly[] assemblies)
        => services
            .AddTransient<ICQRSRequestHandler, CQRSRequestHandler>()
            .AddTransient<Func<ICQRSRequestHandler>>(ctx => () => ctx.GetRequiredService<ICQRSRequestHandler>())
            .AddHandlers(assemblies)
            ;

    private static IServiceCollection AddHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var handlers = assemblies.SelectMany(assembly =>  assembly.DefinedTypes)
            .Where(x => !x.IsAbstract && !x.IsInterface)
            .SelectMany(x => x.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,,>)).Select(i => new { handlerType = x, handlerInterface = i }))
            .ToArray()
            ;

        foreach (var handler in handlers)
        {
            services.AddTransient(handler.handlerInterface, handler.handlerType);
        }

        return services;
    }
}
