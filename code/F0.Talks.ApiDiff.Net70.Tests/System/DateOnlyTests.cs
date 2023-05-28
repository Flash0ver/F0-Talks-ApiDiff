namespace F0.System;

public class DateOnlyTests
{
	private static readonly string Today = "2023-05-24";

	[Fact]
	public void IParsable_Parse()
	{
		var date = DateOnly.Parse(Today, DateTimeFormatInfo.InvariantInfo);

		Assert.Equal(new(2023, 05, 24), date);
	}

	[Fact]
	public void IParsable_TryParse()
	{
		bool success = DateOnly.TryParse(Today, DateTimeFormatInfo.InvariantInfo, out DateOnly result);

		Assert.True(success);
		Assert.Equal(new(2023, 05, 24), result);
	}

	[Fact]
	public void ISpanParsable_Parse()
	{
		ReadOnlySpan<char> span = Today;

		var date = DateOnly.Parse(span, DateTimeFormatInfo.InvariantInfo);

		Assert.Equal(new(2023, 05, 24), date);
	}

	[Fact]
	public void ISpanParsable_TryParse()
	{
		ReadOnlySpan<char> span = Today;

		bool success = DateOnly.TryParse(span, DateTimeFormatInfo.InvariantInfo, out DateOnly result);

		Assert.True(success);
		Assert.Equal(new(2023, 05, 24), result);
	}
}
