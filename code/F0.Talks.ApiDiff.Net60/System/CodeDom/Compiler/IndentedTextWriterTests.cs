namespace F0.System.CodeDom.Compiler;

public class IndentedTextWriterTests
{
	[Fact]
	public async Task IndentedTextWriter_Async()
	{
		await using StringWriter writer = new(CultureInfo.InvariantCulture);

		await using IndentedTextWriter textWriter = new(writer);

		await textWriter.WriteAsync("Text");

		writer.ToString().Should().Be("Text");
	}
}
