namespace ComposedSetup.Core.Interfaces;

public interface IUnitOfWork : CleanCQRS.IUnitOfWorkCommand, CleanCQRS.IUnitOfWorkQuery, IDisposable
{
    IClock Clock { get; }

    IExampleStore ExampleStore { get; }

    TDestination Map<TDestination>(object source) where TDestination : notnull;
    TDestination Map<TSource, TDestination>(TSource source) where TDestination : notnull;
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination) where TDestination : notnull;
    TDestination? MapOrNull<TDestination>(object? source) where TDestination : notnull;
    TDestination? MapOrNull<TSource, TDestination>(TSource? source) where TDestination : notnull;
    TDestination? MapOrNull<TSource, TDestination>(TSource? source, TDestination destination) where TDestination : notnull;
}
