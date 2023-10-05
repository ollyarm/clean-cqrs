namespace CleanCQRS;

public class EmptyResult
{
    private EmptyResult()
    {

    }

    public static EmptyResult Value => new EmptyResult();
    public static Task<EmptyResult> TaskValue => Task.FromResult(Value);
}
