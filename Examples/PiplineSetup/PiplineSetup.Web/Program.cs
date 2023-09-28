using PiplineSetup.Web;
using PiplineSetup.Core;
using PiplineSetup.Core.Interfaces;
using PiplineSetup.Core.Examples;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PiplineSetup.Core.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton(new ExampleStore(TimeSpan.FromMilliseconds(500)))
    .AddTransient<IExampleStore>(c => c.GetRequiredService<ExampleStore>())
    .AddCore()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch(BadRequestException ex)
    {
        var problem = new ProblemDetails()
        {
            Title = "Bad Request",
            Detail = ex.Message,
        };
        ctx.Response.StatusCode = 400;
        await ctx.Response.WriteAsJsonAsync(problem);
    }
});

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

app.Run();

internal record RequestDto(string? message)
{
}