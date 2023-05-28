namespace F0.System.Diagnostics.CodeAnalysis;

public class StringSyntaxAttributeTests
{
	[Fact]
	public void StringSyntax_Attribute()
	{
		var attribute = new StringSyntaxAttribute("C#");

		Assert.Equal("C#", attribute.Syntax);
		Assert.Empty(attribute.Arguments);
	}

	[Fact]
	public void StringSyntax_Attribute_Array()
	{
		var attribute = new StringSyntaxAttribute("Visual Basic", new object[] { 0x_F0 });

		Assert.Equal("Visual Basic", attribute.Syntax);
		Assert.Equal(new object[] { 240 }, attribute.Arguments);
	}

	[Fact]
	public void CompositeFormat()
	{
		var text = String.Format("{0}, {1:n}, {2:x}", 1, 2, 3);

		Assert.Equal("1, 2,00, 3", text);
	}

	[Fact]
	public void DateOnlyFormat()
	{
		DateOnly date = new(2023, 05, 24);

		string text = date.ToString("r");

		Assert.Equal("Wed, 24 May 2023", text);
	}

	[Fact]
	public void DateTimeFormat()
	{
		DateTime dateTime = new(2023, 05, 24, 09, 40, 00, DateTimeKind.Utc);

		string text = dateTime.ToString("o");

		Assert.Equal("2023-05-24T09:40:00.0000000Z", text);
	}

	[Fact]
	public void EnumFormat()
	{
		var value = StringComparison.Ordinal;

		string text = value.ToString("x");

		Assert.Equal("00000004", text);
	}

	[Fact]
	public void GuidFormat()
	{
		var guid = Guid.Empty;

		string text = guid.ToString("d");

		Assert.Equal("00000000-0000-0000-0000-000000000000", text);
	}

	[Fact]
	public void Json()
	{
		JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
		MyClass? value = JsonSerializer.Deserialize<MyClass>("""{ "number": 240, "text": "@0x_F0" }""", options);

		Assert.NotNull(value);
		Assert.Equal(0x_F0, value.Number);
		Assert.Equal("@0x_F0", value.Text);
	}

	[Fact]
	public void NumericFormat()
	{
		BigInteger integer = new(0x_F0);

		string text = integer.ToString("g");

		Assert.Equal("240", text);
	}

	[Fact]
	public void Regex()
	{
		bool isMatch = global::System.Text.RegularExpressions.Regex.IsMatch("@0x_F0", "Flash(Over|0ver|OWare)");

		Assert.False(isMatch);
	}

	[Fact]
	public void TimeOnlyFormat()
	{
		TimeOnly time = new(11, 40, 00);

		string text = time.ToString("r");

		Assert.Equal("11:40:00", text);
	}

	[Fact]
	public void TimeSpanFormat()
	{
		TimeSpan timeSpan = new(240L);

		string text = timeSpan.ToString("c");

		Assert.Equal("00:00:00.0000240", text);
	}

	[Fact]
	public void Uri()
	{
		var uri = new Uri("http://flashoware.net/");

		Assert.Equal("flashoware.net", uri.Host);
	}

	[Fact]
	public void Xml()
	{
		XmlDocument xml = new();

		xml.LoadXml("""
			<Project Sdk="Microsoft.NET.Sdk.Web">
				<PropertyGroup>
					<TargetFramework>net7.0</TargetFramework>
				</PropertyGroup>
			</Project>
			""");

		Assert.NotNull(xml.FirstChild);
		Assert.Equal("net7.0", xml.FirstChild.InnerText);
	}
}

file sealed class MyClass
{
	public required int Number { get; init; }
	public required string Text { get; init; }
}
