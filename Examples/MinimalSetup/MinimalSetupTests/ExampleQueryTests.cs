using MinimalSetup;

namespace MinimalSetupTests;

[TestFixture]
public class ExampleQueryTests : TestBase
{
    [Test]
    public async Task ExampleQuery_when_run_with_empty_store_then_empty_string_is_returned()
    {
        TestStore.LastMessage = null!;

        var query = new ExampleQuery();

        var result = await Uow.Run(query);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task ExampleQuery_when_run_with_value_in_store_then_value_is_returned()
    {
        TestStore.LastMessage = "Expected";

        var query = new ExampleQuery();

        var result = await Uow.Run(query);

        Assert.That(result, Is.EqualTo("Expected"));
    }
}