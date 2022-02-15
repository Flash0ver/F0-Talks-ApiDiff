//TimeOnly
//https://github.com/dotnet/runtime/issues/49036
//https://youtu.be/UF3eu1WBRrc?t=41

//https://devblogs.microsoft.com/dotnet/date-time-and-time-zone-enhancements-in-net-6/#timezoneinfo-adjustmentrule-improvements

namespace F0.System;

public class TimeOnlyTests
{
	[Fact]
	public void TimeOnly_Net60()
	{
		TimeOnly time = new(18, 45, 00, 0);

		time.Hour.Should().Be(18);
		time.Minute.Should().Be(45);
		time.Second.Should().Be(0);
		time.Millisecond.Should().Be(0);

		TimeOnly.MinValue.Should().Be(new TimeOnly(00, 00, 00, 000));
		//TimeOnly.MaxValue.Should().Be(new TimeOnly(23, 59, 59, 999));

		time
			.AddHours(2)
			.AddMinutes(30)
			.Add(TimeSpan.Zero)
			.Should().Be(new TimeOnly(21, 15, 00));

		TimeOnly.FromDateTime(new DateTime(2022, 02, 15, 18, 45, 00))
			.Should().Be(time);

		TimeSpan span = new(18, 45, 00);
		TimeOnly.FromTimeSpan(span).Should().Be(time);
		time.ToTimeSpan().Should().Be(span);

		bool success = TimeOnly.TryParseExact("18:45:00", "HH:mm:ss", out TimeOnly result);
		success.Should().BeTrue();
		result.Should().Be(time);
	}
}
