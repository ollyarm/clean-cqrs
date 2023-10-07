namespace SyncSetup.UnitTests;

[TestFixture]
public class ShouldContinueCommandTests : TestsBase
{
    [TestCase("y", ShouldContinueCommand.Result.Yes)]
    [TestCase("Y", ShouldContinueCommand.Result.Yes)]
    [TestCase("Yes", ShouldContinueCommand.Result.Yes)]
    [TestCase("yes", ShouldContinueCommand.Result.Yes)]
    [TestCase("n", ShouldContinueCommand.Result.No)]
    [TestCase("N", ShouldContinueCommand.Result.No)]
    [TestCase("no", ShouldContinueCommand.Result.No)]
    [TestCase("NO", ShouldContinueCommand.Result.No)]
    [TestCase("", ShouldContinueCommand.Result.Unknown)]
    [TestCase(null, ShouldContinueCommand.Result.Unknown)]
    [TestCase("other", ShouldContinueCommand.Result.Unknown)]
    [TestCase("other value", ShouldContinueCommand.Result.Unknown)]
    public void ShouldContinueCommand_when_input_then_result_expected(string input, ShouldContinueCommand.Result expected)
    {
        var result = Run(new ShouldContinueCommand() { Input = input });
        Assert.That(result, Is.EqualTo(expected));
    }
}