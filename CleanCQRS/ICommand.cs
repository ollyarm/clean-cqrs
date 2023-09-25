namespace CleanCQRS;

public interface ICommand<T>
{
}

public interface ICommand : ICommand<EmptyResult>
{
}
