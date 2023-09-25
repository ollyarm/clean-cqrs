namespace SimpleSetup.CoreUnitTests;

using Core.Examples;

[TestFixture]
public class ExampleQueryTests : TestBase
{
    [Test]
    public async Task ExampleQuery_when_run_with_empty_store_then_empty_string_is_returned()
    {
        FakeExampleStore.LastMessage = null!;

        var query = new ExampleQuery();

        var result = await Uow.Run(query, CancellationToken.None);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task ExampleQuery_when_run_with_value_in_store_then_value_is_returned()
    {
        FakeExampleStore.LastMessage = "Expected";

        var query = new ExampleQuery();

        var result = await Uow.Run(query, CancellationToken.None);

        Assert.That(result, Is.EqualTo("Expected"));
    }
}
