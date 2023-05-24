namespace F0.System.Text.Json;

public class SerializationTests
{
	private const string Indent = "  ";
	private readonly JsonSerializerOptions _options;

	public SerializationTests()
	{
		_options = new JsonSerializerOptions
		{
			WriteIndented = true,
		};
	}

	[Fact]
	public void Serialization()
	{
		ReadOnlySpan<byte> jsonData = """
			{
				"Text": "\\Hello\\",
				"Number": 123,
			}
			"""u8;

		JsonReaderOptions options = new()
		{
			AllowTrailingCommas = true,
		};
		Utf8JsonReader reader = new(jsonData, options);

		Assert.True(reader.Read());
		Assert.Equal(JsonTokenType.StartObject, reader.TokenType);

		Assert.True(reader.Read());
		Assert.Equal(JsonTokenType.PropertyName, reader.TokenType);

		Assert.True(reader.Read());
		Assert.Equal(JsonTokenType.String, reader.TokenType);
		Assert.True(reader.ValueIsEscaped);
		Assert.True(reader.ValueSpan.SequenceEqual("""\\Hello\\"""u8));

		Span<byte> utf8Destination = stackalloc byte[8];
		int bytesWritten = reader.CopyString(utf8Destination);
		Assert.Equal(7, bytesWritten);
		Assert.True(utf8Destination.Slice(0, bytesWritten).SequenceEqual("""\Hello\"""u8));
	}

	[Fact]
	public void PolymorphicSerialization()
	{
		string utf8 = $$"""
			{
			{{Indent}}"Time": "11:40:00"
			}
			""";

		MyBase value = new MyDerived1()
		{
			Date = new DateOnly(2023, 05, 24),
			Time = new TimeOnly(11, 40),
		};

		//JsonSerializerOptions.Default
		string json = JsonSerializer.Serialize<MyBase>(value, _options);
		MyBase? roundTrip = JsonSerializer.Deserialize<MyBase>(json);

		Assert.Equal($$"""
			{
			{{Indent}}"$type": "derived1",
			{{Indent}}"Time": "11:40:00",
			{{Indent}}"Date": "2023-05-24"
			}
			""", json);
		Assert.Equal(value, roundTrip);
	}
}

//[JsonDerivedType(typeof(MyDerived1))]
[JsonDerivedType(typeof(MyBase), "base")]
[JsonDerivedType(typeof(MyDerived1), "derived1")]
[JsonDerivedType(typeof(MyDerived2), "derived2")]
//[JsonPolymorphic(TypeDiscriminatorPropertyName = "$discriminator", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
//[JsonDerivedType(typeof(MyDerived1), 1)]
//[JsonDerivedType(typeof(MyDerived2), 2)]
//alternative: Contract Model
file record class MyBase
{
	[JsonRequired]
	public DateOnly Date { get; set; }
}

file record class MyDerived1 : MyBase
{
	public TimeOnly Time { get; set; }
}

file record class MyDerived2 : MyBase
{
	public string? Text { get; set; }
}
