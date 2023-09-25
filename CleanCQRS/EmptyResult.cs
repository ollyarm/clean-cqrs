namespace CleanCQRS;

public class EmptyResult
{
    private EmptyResult()
    {

    }

    public static EmptyResult Value => new EmptyResult();
}
