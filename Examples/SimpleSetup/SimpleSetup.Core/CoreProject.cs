using CleanCQRS;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SimpleSetup.Core;

using Common;
using Interfaces;

public static class CoreProject
{
    public static Assembly Assembly => typeof(CoreProject).Assembly;

    public static IServiceCollection AddCore(this IServiceCollection services) => services
        .AddTransient<IUnitOfWorkProvider, UnitOfWorkProvider>()
        .AddCleanCQRS(Assembly)
        ;
}
