namespace IsolatedSetup.CoreUnitTests;

using Core.Examples;
using IsolatedSetup.Core.Exceptions;

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

    [TestCase("bad")]
    [TestCase("BAD")]
    [TestCase("Bad")]
    public void ExampleCommand_when_run_with_bad_massage_expect_BadRequestException(string message)
    {

        var command = new ExampleCommand()
        {
            Message = message
        };

        Assert.That(() =>  Uow.Run(command, CancellationToken.None), Throws.InstanceOf<BadRequestException>().With.Message.Match($"'{message}' is not permitted."));

    }
}
