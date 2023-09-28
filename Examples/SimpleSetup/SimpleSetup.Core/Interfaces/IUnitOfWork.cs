namespace SimpleSetup.Core.Interfaces;

public interface IUnitOfWork : CleanCQRS.IUnitOfWorkCommand, CleanCQRS.IUnitOfWorkQuery, IDisposable
{
}
