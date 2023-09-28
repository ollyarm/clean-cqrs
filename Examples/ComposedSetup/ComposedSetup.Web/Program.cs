using ComposedSetup.Web;
using ComposedSetup.Core;
using ComposedSetup.Core.Interfaces;
using ComposedSetup.Core.Examples;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ComposedSetup.Core.Exceptions;
using ComposedSetup.Core.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IExampleStore,ExampleStore>()
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
        var result = await uow.Run(query, cancellationToken).Then(r => uow.Map<ResponseDto>(r));
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

internal record ResponseDto(string? message)
{
}