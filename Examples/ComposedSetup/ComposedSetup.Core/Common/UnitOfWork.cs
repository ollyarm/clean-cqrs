using AutoMapper;
using CleanCQRS;

namespace ComposedSetup.Core.Common;

using ComposedSetup.Core.Examples;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;

public class UnitOfWork : IUnitOfWork
{
    private ICQRSRequestHandler<IUnitOfWork> _requestHandler;

    public UnitOfWork(IServiceProvider serviceProvider)
    {
        _requestHandler = serviceProvider.GetRequiredService<ICQRSRequestHandler<IUnitOfWork>>();
        Clock = serviceProvider.GetRequiredService<IClock>();
        _exampleStoreFactory = () => serviceProvider.GetRequiredService<IExampleStore>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }

    #region Clock
    public IClock Clock { get; }
    #endregion Clock

    #region ExampleStore
    private readonly Func<IExampleStore> _exampleStoreFactory;
    private IExampleStore? _exampleStore;
    public IExampleStore ExampleStore => _exampleStore ??= _exampleStoreFactory();
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

    public Task Run(ICommand command, CancellationToken cancellationToken) => _requestHandler.HandleCommand(this, command, cancellationToken);

    public Task<T> Run<T>(ICommand<T> command, CancellationToken cancellationToken) => _requestHandler.HandleCommand(this, command, cancellationToken);

    public Task<T> Run<T>(IQuery<T> query, CancellationToken cancellationToken) => _requestHandler.HandleQuery(this, query, cancellationToken);
    public void Dispose() 
    {
    }
}