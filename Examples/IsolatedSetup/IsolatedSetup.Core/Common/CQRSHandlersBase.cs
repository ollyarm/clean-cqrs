using CleanCQRS;
using CleanCQRS.Handlers;

namespace IsolatedSetup.Core.Common;

using Interfaces;

// use base classs to tie down unint of work and prefered patterns
public abstract class AsyncCommandHandler<T> : AsyncCommandHandlerBase<ICommandUnitOfWork, T>
    where T : ICommand
{ }

public abstract class AsyncCommandHandler<T, TResult> : AsyncCommandHandlerBase<ICommandUnitOfWork, T, TResult>
    where T : ICommand<TResult>
{ }

public abstract class AsyncQueryHandler<T, TResult> : AsyncQueryHandlerBase<IQueryUnitOfWork, T, TResult>
    where T : IQuery<TResult>
{
}
