namespace F0.System;

public class DateTimeOffsetTests
{
	private static readonly string Now = "2023-05-24T11:40:00.0000000+02:00";
	private static readonly TimeSpan Offset = TimeSpan.FromHours(2);

	[Fact]
	public void Ctor_Microsecond()
	{
		DateTimeOffset dateTimeOffset = new(1, 2, 3, 4, 5, 6, 7, 8, Offset);
		//Calendar, TimeSpan

		Assert.Equal(1, dateTimeOffset.Year);
		Assert.Equal(2, dateTimeOffset.Month);
		Assert.Equal(3, dateTimeOffset.Day);
		Assert.Equal(4, dateTimeOffset.Hour);
		Assert.Equal(5, dateTimeOffset.Minute);
		Assert.Equal(6, dateTimeOffset.Second);
		Assert.Equal(7, dateTimeOffset.Millisecond);
		Assert.Equal(8, dateTimeOffset.Microsecond);
		Assert.Equal(0, dateTimeOffset.Nanosecond);
		Assert.Equal(Offset, dateTimeOffset.Offset);
	}

	[Fact]
	public void Microsecond()
	{
		DateTimeOffset dateTimeOffset = new(1, 2, 3, 4, 5, 6, 7, 8, Offset);

		Assert.Equal(8, dateTimeOffset.Microsecond);
	}

	[Fact]
	public void Nanosecond()
	{
		DateTimeOffset dateTimeOffset = new(1, 2, 3, 4, 5, 6, 7, 8, Offset);

		Assert.Equal(0, dateTimeOffset.Nanosecond);
	}

	[Fact]
	public void AddMicroseconds()
	{
		DateTimeOffset dateTimeOffset = new(1, 2, 3, 4, 5, 6, 7, 8, Offset);

		dateTimeOffset = dateTimeOffset.AddMicroseconds(2.5d);

		Assert.Equal(10, dateTimeOffset.Microsecond);
		Assert.Equal(500, dateTimeOffset.Nanosecond);
	}

	[Fact]
	public void IParsable_Parse()
	{
		var dateTimeOffset = DateTimeOffset.Parse(Now, DateTimeFormatInfo.InvariantInfo);

		Assert.Equal(new(2023, 05, 24, 11, 40, 00, Offset), dateTimeOffset);
	}

	[Fact]
	public void IParsable_TryParse()
	{
		bool success = DateTimeOffset.TryParse(Now, DateTimeFormatInfo.InvariantInfo, out DateTimeOffset result);

		Assert.True(success);
		Assert.Equal(new(2023, 05, 24, 11, 40, 00, Offset), result);
	}

	[Fact]
	public void ISpanParsable_Parse()
	{
		ReadOnlySpan<char> span = Now;

		var dateTimeOffset = DateTimeOffset.Parse(span, DateTimeFormatInfo.InvariantInfo);

		Assert.Equal(new(2023, 05, 24, 11, 40, 00, Offset), dateTimeOffset);
	}

	[Fact]
	public void ISpanParsable_TryParse()
	{
		ReadOnlySpan<char> span = Now;

		bool success = DateTimeOffset.TryParse(span, DateTimeFormatInfo.InvariantInfo, out DateTimeOffset result);

		Assert.True(success);
		Assert.Equal(new(2023, 05, 24, 11, 40, 00, Offset), result);
	}
}
