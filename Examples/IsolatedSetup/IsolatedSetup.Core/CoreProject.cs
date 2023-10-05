using CleanCQRS;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IsolatedSetup.Core;

using Common;
using Interfaces;

public static class CoreProject
{
    public static Assembly Assembly => typeof(CoreProject).Assembly;

    public static IServiceCollection AddCore(this IServiceCollection services) => services
        .AddTransient<IClock, SystemClock>()
        .AddTransient<IUnitOfWorkProvider, UnitOfWorkProvider>()
        .AddTransient(typeof(IPipeline<,,>), typeof(Pipline<,,>))
        .AddCleanCQRS(Assembly)
        .AddAutoMapper(Assembly);
}
