namespace F0.System;

public class TimeSpanTests
{
	[Fact]
	public void NanosecondsPerTick()
	{
		Assert.Equal(100L, TimeSpan.NanosecondsPerTick);
	}

	[Fact]
	public void TicksPerMicrosecond()
	{
		Assert.Equal(10L, TimeSpan.TicksPerMicrosecond);
	}

	[Fact]
	public void Ctor()
	{
		TimeSpan duration = new(1, 2, 3, 4, 5, 6);

		Assert.Equal(1, duration.Days);
		Assert.Equal(2, duration.Hours);
		Assert.Equal(3, duration.Minutes);
		Assert.Equal(4, duration.Seconds);
		Assert.Equal(5, duration.Milliseconds);
		Assert.Equal(6, duration.Microseconds);
		Assert.Equal(0, duration.Nanoseconds);
	}

	[Fact]
	public void FromMicroseconds()
	{
		var duration = TimeSpan.FromMicroseconds(2.5d);

		Assert.Equal(0, duration.Days);
		Assert.Equal(0, duration.Hours);
		Assert.Equal(0, duration.Minutes);
		Assert.Equal(0, duration.Seconds);
		Assert.Equal(0, duration.Milliseconds);
		Assert.Equal(2, duration.Microseconds);
		Assert.Equal(500, duration.Nanoseconds);
	}

	[Fact]
	public void Microseconds()
	{
		TimeSpan duration = new(1, 2, 3, 4, 5, 6);

		Assert.Equal(6, duration.Microseconds);
		Assert.Equal(93_784_005_006.0d, duration.TotalMicroseconds);
	}

	[Fact]
	public void Nanoseconds()
	{
		TimeSpan duration = new(1, 2, 3, 4, 5, 6);

		Assert.Equal(0, duration.Nanoseconds);
		Assert.Equal(93_784_005_006_000.0d, duration.TotalNanoseconds);
	}
}
