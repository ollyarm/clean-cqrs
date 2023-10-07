namespace SyncSetup.UnitTests;

[TestFixture]
public class ExampleCommandTests : TestsBase
{
    [Test]
    public void ExampleCommand_when_valid_then_input_is_stored()
    {
        var command = new ExampleCommand()
        {
            Input = "Test Input"
        };

        Run(command);

        Assert.That(Store.Message, Is.EqualTo("Last run with message: 'Test Input'"));
    }

    [TestCase((string?)null)]
    [TestCase("")]
    [TestCase("\n")]
    [TestCase(" ")]
    [TestCase("Bad")]
    [TestCase("bad")]
    public void ExampleCommand_when_invalid_then_input_is_not_stored(string value)
    {
        Store.Message = "Original";
        var command = new ExampleCommand()
        {
            Input = value
        };

        Run(command);

        Assert.That(Store.Message, Is.EqualTo("Original"));
    }
}