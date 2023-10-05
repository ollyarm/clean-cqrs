namespace Barebones.Console;

public class ExampleStore
{
    private int? count;
   
    public int? GetCount() => count;

    public void Init(int initalValue)
    {
        if (count != null)
        {
            throw new InvalidOperationException("Already Initalised");
        }

        System.Console.WriteLine($"Initalising to {initalValue}");

        count = initalValue;
    }

    public void Increment()
    {
        count = (count ??= 0) + 1;
        if(count % 5 == 0) 
        { 
            System.Console.WriteLine($"Incremented to {count}");
        }
    }
}
