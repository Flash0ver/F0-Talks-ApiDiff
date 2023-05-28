namespace F0.System;

public class TimeOnlyTests
{
	private static readonly string Now = "11:40:00";

	[Fact]
	public void Ctor()
	{
		TimeOnly time = new(1, 2, 3, 4, 5);

		Assert.Equal(1, time.Hour);
		Assert.Equal(2, time.Minute);
		Assert.Equal(3, time.Second);
		Assert.Equal(4, time.Millisecond);
		Assert.Equal(5, time.Microsecond);
		Assert.Equal(0, time.Nanosecond);
	}

	[Fact]
	public void Microsecond()
	{
		TimeOnly time = new(1, 2, 3, 4, 5);

		Assert.Equal(5, time.Microsecond);
	}

	[Fact]
	public void Nanosecond()
	{
		TimeOnly time = new(1, 2, 3, 4, 5);

		Assert.Equal(0, time.Nanosecond);
	}

	[Fact]
	public void IParsable_Parse()
	{
		var time = TimeOnly.Parse(Now, DateTimeFormatInfo.InvariantInfo);

		Assert.Equal(new(11, 40, 00), time);
	}

	[Fact]
	public void IParsable_TryParse()
	{
		bool success = TimeOnly.TryParse(Now, DateTimeFormatInfo.InvariantInfo, out TimeOnly result);

		Assert.True(success);
		Assert.Equal(new(11, 40, 00), result);
	}

	[Fact]
	public void ISpanParsable_Parse()
	{
		ReadOnlySpan<char> span = Now;

		var time = TimeOnly.Parse(span, DateTimeFormatInfo.InvariantInfo);

		Assert.Equal(new(11, 40, 00), time);
	}

	[Fact]
	public void ISpanParsable_TryParse()
	{
		ReadOnlySpan<char> span = Now;

		bool success = TimeOnly.TryParse(span, DateTimeFormatInfo.InvariantInfo, out TimeOnly result);

		Assert.True(success);
		Assert.Equal(new(11, 40, 00), result);
	}
}
