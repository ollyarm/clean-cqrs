using AutoMapper;
using CleanCQRS;

namespace IsolatedSetup.Core.Common;

using Interfaces;
using Microsoft.Extensions.DependencyInjection;

public class UnitOfWork : IUnitOfWork
{
    private ICQRSRequestHandler _requestHandler;

    public UnitOfWork(ICQRSRequestHandler requestHandler, IClock clock, IMapper mapper, IExampleReadStore readStore, IExampleWriteStore writeStore)
    {
        _requestHandler = requestHandler;
        Clock = clock;
        ExampleReadStore = readStore;
        ExampleWriteStore = writeStore;
        _mapper = mapper;
    }

    #region Clock
    public IClock Clock { get; }
    #endregion Clock

    #region ExampleStore
    public IExampleReadStore ExampleReadStore { get; }
    public IExampleWriteStore ExampleWriteStore { get; }
    #endregion ExampleStore

    #region Mapper
    private IMapper _mapper;
    public TDestination Map<TDestination>(object source) where TDestination : notnull => _mapper.Map<TDestination>(source);
    public TDestination Map<TSource, TDestination>(TSource source) where TDestination : notnull => _mapper.Map<TSource, TDestination>(source);
    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) where TDestination : notnull => _mapper.Map<TSource, TDestination>(source, destination);
    public TDestination? MapOrNull<TDestination>(object? source) where TDestination : notnull => _mapper.Map<TDestination?>(source);
    public TDestination? MapOrNull<TSource, TDestination>(TSource? source) where TDestination : notnull => _mapper.Map<TSource?, TDestination?>(source);
    public TDestination? MapOrNull<TSource, TDestination>(TSource? source, TDestination destination) where TDestination : notnull => _mapper.Map<TSource?, TDestination?>(source, destination);
    #endregion Mapper

    public Task Run(ICommand command, CancellationToken cancellationToken) => _requestHandler.HandleCommand<ICommandUnitOfWork>(this, command, cancellationToken);

    public Task<T> Run<T>(ICommand<T> command, CancellationToken cancellationToken) => _requestHandler.HandleCommand<ICommandUnitOfWork, T>(this, command, cancellationToken);

    public Task<T> Run<T>(IQuery<T> query, CancellationToken cancellationToken) => _requestHandler.HandleQuery<IQueryUnitOfWork, T>(this, query, cancellationToken);
    public void Dispose() 
    {
    }
}