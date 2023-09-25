using SimpleSetup.Web;
using SimpleSetup.Core;
using SimpleSetup.Core.Interfaces;
using SimpleSetup.Core.Examples;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<ExampleStore>()
    .AddTransient<IExampleStore>(c => c.GetRequiredService<ExampleStore>())
    .AddTransient<IExampleAsyncStore>(c => c.GetRequiredService<ExampleStore>())
    .AddCore()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/example", async (IUnitOfWorkProvider provider, CancellationToken cancellationToken) =>
{
    using (var uow = provider.Start())
    {
        var query = new ExampleQuery();
        var result = await uow.Run(query, cancellationToken);
        return Results.Ok(result);
    }
});

app.MapPut("/example", async ([FromBody] RequestDto dto, IUnitOfWorkProvider provider, CancellationToken cancellationToken) =>
{
    using (var uow = provider.Start())
    {
        var query = new ExampleCommand()
        {
            Message = dto?.message
        };
        await uow.Run(query, cancellationToken);
        return Results.Ok();
    }
});

app.MapGet("/example2", async (IUnitOfWorkProvider provider, CancellationToken cancellationToken) =>
{
    using (var uow = provider.Start())
    {
        var query = new ExampleQueryWithAsyncDependency();
        var result = await uow.Run(query, cancellationToken);
        return Results.Ok(result);
    }
});

app.MapPut("/example2", async ([FromBody] RequestDto dto, IUnitOfWorkProvider provider, CancellationToken cancellationToken) =>
{
    using (var uow = provider.Start())
    {
        var query = new ExampleCommandWithAsyncDependency()
        {
            Message = dto?.message
        };
        await uow.Run(query, cancellationToken);
        return Results.Ok();
    }
});

app.Run();

internal record RequestDto(string? message)
{
}