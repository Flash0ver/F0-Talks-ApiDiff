//https://www.youtube.com/live/viY9djCf4pg?feature=share&t=625

namespace F0.System.ComponentModel;

public class TypeConverterTests
{
	[Fact]
	public void New_TypeConverter()
	{
		Assert.True(typeof(DateOnlyConverter).BaseType == typeof(TypeConverter), nameof(DateOnly));
		Assert.True(typeof(TimeOnlyConverter).BaseType == typeof(TypeConverter), nameof(TimeOnly));
		Assert.True(typeof(HalfConverter).BaseType == typeof(BaseNumberConverter), nameof(Half));
		Assert.True(typeof(Int128Converter).BaseType == typeof(BaseNumberConverter), nameof(Int128));
		Assert.True(typeof(UInt128Converter).BaseType == typeof(BaseNumberConverter), nameof(UInt128));
	}

	[Fact]
	public void Convert()
	{
		MyBinding value = new()
		{
			DateOnly = new DateOnly(2023, 05, 24),
			TimeOnly = new TimeOnly(11, 40),
			Half = Half.CreateChecked(1),
			Int128 = new Int128(0, 2),
			UInt128 = new UInt128(0, 3),
		};

		JsonSerializerOptions options = new() { WriteIndented = true };
		string json = JsonSerializer.Serialize(value, options);

		const string Indent = "  ";
		Assert.Equal($$"""
			{
			{{Indent}}"{{nameof(DateOnly)}}": "2023-05-24",
			{{Indent}}"{{nameof(TimeOnly)}}": "11:40:00",
			{{Indent}}"{{nameof(Half)}}": {},
			{{Indent}}"{{nameof(Int128)}}": {},
			{{Indent}}"{{nameof(UInt128)}}": {}
			}
			""", json);

		var roundTrip = JsonSerializer.Deserialize<MyBinding>(json);

		MyBinding expected = value with
		{
			Half = default,
			Int128 = default,
			UInt128 = default,
		};
		Assert.Equal(expected, roundTrip);
	}
}

internal sealed record class MyBinding
{
	public required DateOnly DateOnly { get; set; }
	public required TimeOnly TimeOnly { get; set; }
	public required Half Half { get; set; }
	public required Int128 Int128 { get; set; }
	public required UInt128 UInt128 { get; set; }
}
