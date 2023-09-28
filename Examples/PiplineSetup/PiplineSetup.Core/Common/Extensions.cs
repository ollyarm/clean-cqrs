namespace PiplineSetup.Core.Common;

public static class Extensions
{
    public static string PrepareMessage(this string? message) => string.IsNullOrWhiteSpace(message) ? "No message provided" : message;
 
    public static async Task<TResult> Then<TSource, TResult>(this Task<TSource> source, Func<TSource, TResult> next)
    {
        var result = await source;
        return next(result);
    }

    public static async Task<TResult> Then<TResult>(this Task source, Func<TResult> next)
    {
        await source;
        return next();
    }

    public static Task<T> AsTask<T>(this T source) => Task.FromResult(source);
}
