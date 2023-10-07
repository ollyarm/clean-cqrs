namespace SyncSetup.UnitTests;

[TestFixture]
public class ExampleQueryTests : TestsBase
{
    [Test]
    public void ExampleQuery_when_store_has_value_then_value_is_returned()
    {
        Store.Message = "Value";

        var result = Run(new ExampleQuery());

        Assert.That(result, Is.EqualTo("Value"));
    }

    [Test]
    public void ExampleQuery_when_store_has_no_value_then_default_message_returned()
    {
        Store.Message = null;

        var result = Run(new ExampleQuery());

        Assert.That(result, Is.EqualTo("No message set"));
    }
}