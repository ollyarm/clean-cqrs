namespace SimpleSetup.CoreUnitTests;

using Core.Examples;

[TestFixture]
public class ExampleCommandWithAsyncDependencyTests : TestBase
{
    [Test]
    public async Task ExampleCommandWithAsyncDependency_when_run_with_message_store_is_updated_with_new_message()
    {
        var command = new ExampleCommandWithAsyncDependency()
        {
            Message = "The new message",
        };

        await Uow.Run(command, CancellationToken.None);

        Assert.That(FakeExampleStore.LastMessage, Is.EqualTo("Last run with async message: 'The new message'"));
    }

    [Test]
    public async Task ExampleCommandWithAsyncDependency_when_run_with_no_message_store_is_updated_with_new_message()
    {
        var command = new ExampleCommandWithAsyncDependency()
        {
            Message = ""
        };

        await Uow.Run(command, CancellationToken.None);

        Assert.That(FakeExampleStore.LastMessage, Is.EqualTo("Last run with async message: 'No message provided'"));
    }
}
