namespace IsolatedSetup.Core.Interfaces;

public interface ICommonUnitOfWork
{
    IClock Clock { get; }
    TDestination Map<TDestination>(object source) where TDestination : notnull;
    TDestination Map<TSource, TDestination>(TSource source) where TDestination : notnull;
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination) where TDestination : notnull;
    TDestination? MapOrNull<TDestination>(object? source) where TDestination : notnull;
    TDestination? MapOrNull<TSource, TDestination>(TSource? source) where TDestination : notnull;
    TDestination? MapOrNull<TSource, TDestination>(TSource? source, TDestination destination) where TDestination : notnull;

}

public interface ICommandUnitOfWork : CleanCQRS.IUnitOfWorkCommand, ICommonUnitOfWork
{
    IExampleReadStore ExampleReadStore { get; }
    IExampleWriteStore ExampleWriteStore { get; }
}

public interface IQueryUnitOfWork : CleanCQRS.IUnitOfWorkQuery, ICommonUnitOfWork
{
    IExampleReadStore ExampleReadStore { get; }
}



public interface IUnitOfWork : ICommonUnitOfWork, ICommandUnitOfWork, IQueryUnitOfWork, IDisposable
{
}
