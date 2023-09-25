using CleanCQRS;
using CleanCQRS.Handlers;

namespace SimpleSetup.Core.Common;

using Interfaces;

// use base classs to tie down unint of work and prefered patterns
public abstract class SyncCommandHandler<T> : SyncCommandHandlerBase<IUnitOfWork, T>
    where T : ICommand
{ }

public abstract class SyncCommandHandler<T, TResult> : SyncCommandHandlerBase<IUnitOfWork, T, TResult>
    where T : ICommand<TResult>
{ }

public abstract class AsyncCommandHandler<T> : AsyncCommandHandlerBase<IUnitOfWork, T>
    where T : ICommand
{ }

public abstract class AsyncCommandHandler<T, TResult> : AsyncCommandHandlerBase<IUnitOfWork, T, TResult>
    where T : ICommand<TResult>
{ }

public abstract class SyncQueryHandler<T, TResult> : SyncQueryHandlerBase<IUnitOfWork, T, TResult>
    where T : IQuery<TResult>
{
}

public abstract class AsyncQueryHandler<T, TResult> : AsyncQueryHandlerBase<IUnitOfWork, T, TResult>
    where T : IQuery<TResult>
{
}
