namespace PiplineSetup.Core.Interfaces;

public interface IUnitOfWork : CleanCQRS.IUnitOfWorkCommand, CleanCQRS.IUnitOfWorkQuery, IDisposable
{
    IClock Clock { get; }
}
