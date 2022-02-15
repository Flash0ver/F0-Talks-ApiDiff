//DateOnly
//https://github.com/dotnet/runtime/issues/49036
//https://youtu.be/UF3eu1WBRrc?t=41

//https://devblogs.microsoft.com/dotnet/date-time-and-time-zone-enhancements-in-net-6/#timezoneinfo-adjustmentrule-improvements

namespace F0.System;

public class DateOnlyTests
{
	[Fact]
	public void DateOnly_Net60()
	{
		DateOnly date = new(2022, 02, 15);

		date.Year.Should().Be(2022);
		date.Month.Should().Be(2);
		date.Day.Should().Be(15);
		date.DayOfWeek.Should().Be(DayOfWeek.Tuesday);
		date.DayOfYear.Should().Be(31 + 15);
		date.DayNumber.Should().Be(738_200);

		DateOnly.MinValue.Should().Be(new DateOnly(0001, 01, 01));
		DateOnly.MaxValue.Should().Be(new DateOnly(9999, 12, 31));

		date
			.AddYears(20)
			.AddMonths(0)
			.AddDays(-2)
			.Should().Be(new DateOnly(2042, 02, 13));

		date.ToDateTime(new TimeOnly(19, 00, 00))
			.Should().Be(new DateTime(2022, 02, 15, 19, 00, 00));

		DateOnly.FromDateTime(new DateTime(2022, 02, 15, 19, 00, 00))
			.Should().Be(date);

		bool success = DateOnly.TryParseExact("2022-02-15", "yyyy-MM-dd", out DateOnly result);
		success.Should().BeTrue();
		result.Should().Be(date);
	}
}
