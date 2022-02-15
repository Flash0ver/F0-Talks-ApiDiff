namespace F0.System.Text.Json.Serialization;

public class SourceGenerationTests
{
	[Fact]
	public void SourceGeneration()
	{
		Entity entity = new()
		{
			Name = "Generation",
			Number = 1,
		};

		string json = JsonSerializer.Serialize(entity, SerializerContext.Default.Entity);
		Entity? roundtrip = JsonSerializer.Deserialize<Entity>(json, SerializerContext.Default.Entity);

		roundtrip.Should().Be(entity);
	}
}

public record class Entity
{
	public string? Name { get; set; }
	public int Number { get; set; }
}

[JsonSerializable(typeof(Entity))]
[JsonSourceGenerationOptions(WriteIndented = true)]
public partial class SerializerContext : JsonSerializerContext
{
}
