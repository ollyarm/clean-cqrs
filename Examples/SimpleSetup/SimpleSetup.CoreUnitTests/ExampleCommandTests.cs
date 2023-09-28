namespace SimpleSetup.CoreUnitTests;

using Core.Examples;

[TestFixture]
public class ExampleCommandTests : TestBase
{
    [Test]
    public async Task ExampleCommand_when_run_with_message_store_is_updated_with_new_message()
    {
        var command = new ExampleCommand()
        {
            Message = "The new message",
        };

        await Uow.Run(command, CancellationToken.None);

        Assert.That(FakeExampleStore.LastMessage, Is.EqualTo("Last run with message: 'The new message'"));
    }

    [Test]
    public async Task ExampleCommand_when_run_with_no_message_store_is_updated_with_new_message()
    {
        var command = new ExampleCommand()
        {
            Message = ""
        };

        await Uow.Run(command, CancellationToken.None);

        Assert.That(FakeExampleStore.LastMessage, Is.EqualTo("Last run with message: 'No message provided'"));
    }
}
