namespace F0.System;

public class DateTimeTests
{
	private static readonly string Now = "2023-05-24T11:40:00.0000000";

	[Fact]
	public void Ctor_Microsecond()
	{
		DateTime dateTime = new(1, 2, 3, 4, 5, 6, 7, 8);
		//Calendar

		Assert.Equal(1, dateTime.Year);
		Assert.Equal(2, dateTime.Month);
		Assert.Equal(3, dateTime.Day);
		Assert.Equal(4, dateTime.Hour);
		Assert.Equal(5, dateTime.Minute);
		Assert.Equal(6, dateTime.Second);
		Assert.Equal(7, dateTime.Millisecond);
		Assert.Equal(8, dateTime.Microsecond);
		Assert.Equal(0, dateTime.Nanosecond);
		Assert.Equal(DateTimeKind.Unspecified, dateTime.Kind);
	}

	[Fact]
	public void Ctor_Microsecond_DateTimeKind()
	{
		DateTime dateTime = new(1, 2, 3, 4, 5, 6, 7, 8, DateTimeKind.Local);
		//Calendar, DateTimeKind

		Assert.Equal(1, dateTime.Year);
		Assert.Equal(2, dateTime.Month);
		Assert.Equal(3, dateTime.Day);
		Assert.Equal(4, dateTime.Hour);
		Assert.Equal(5, dateTime.Minute);
		Assert.Equal(6, dateTime.Second);
		Assert.Equal(7, dateTime.Millisecond);
		Assert.Equal(8, dateTime.Microsecond);
		Assert.Equal(0, dateTime.Nanosecond);
		Assert.Equal(DateTimeKind.Local, dateTime.Kind);
	}

	[Fact]
	public void Microsecond()
	{
		DateTime dateTime = new(1, 2, 3, 4, 5, 6, 7, 8);

		Assert.Equal(8, dateTime.Microsecond);
	}

	[Fact]
	public void Nanosecond()
	{
		DateTime dateTime = new(1, 2, 3, 4, 5, 6, 7, 8);

		Assert.Equal(0, dateTime.Nanosecond);
	}

	[Fact]
	public void AddMicroseconds()
	{
		DateTime dateTime = new(1, 2, 3, 4, 5, 6, 7, 8);

		dateTime = dateTime.AddMicroseconds(2.5d);

		Assert.Equal(10, dateTime.Microsecond);
		Assert.Equal(500, dateTime.Nanosecond);
	}

	[Fact]
	public void IParsable_Parse()
	{
		var dateTime = DateTime.Parse(Now, DateTimeFormatInfo.InvariantInfo);

		Assert.Equal(new(2023, 05, 24, 11, 40, 00), dateTime);
	}

	[Fact]
	public void IParsable_TryParse()
	{
		bool success = DateTime.TryParse(Now, DateTimeFormatInfo.InvariantInfo, out DateTime result);

		Assert.True(success);
		Assert.Equal(new(2023, 05, 24, 11, 40, 00), result);
	}

	[Fact]
	public void ISpanParsable_Parse()
	{
		ReadOnlySpan<char> span = Now;

		var dateTime = DateTime.Parse(span, DateTimeFormatInfo.InvariantInfo);

		Assert.Equal(new(2023, 05, 24, 11, 40, 00), dateTime);
	}

	[Fact]
	public void ISpanParsable_TryParse()
	{
		ReadOnlySpan<char> span = Now;

		bool success = DateTime.TryParse(span, DateTimeFormatInfo.InvariantInfo, out DateTime result);

		Assert.True(success);
		Assert.Equal(new(2023, 05, 24, 11, 40, 00), result);
	}
}
