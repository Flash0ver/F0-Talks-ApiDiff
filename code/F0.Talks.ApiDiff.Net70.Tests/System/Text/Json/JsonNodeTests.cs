using System.Text.Json.Nodes;

namespace F0.System.Text.Json;

public class JsonNodeTests
{
	[Fact]
	public void Parse_StringSyntaxAttribute()
	{
		var node = JsonNode.Parse("""{ "Key": "Value" }""");

		Assert.NotNull(node);

		MyClass? value = node.Deserialize<MyClass>();

		Assert.NotNull(value);

		Assert.Equal("Value", value.Key);
	}
}

file class MyClass
{
	public required string Key { get; set; }
}
