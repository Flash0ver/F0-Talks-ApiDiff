//https://devblogs.microsoft.com/dotnet/date-time-and-time-zone-enhancements-in-net-6/

//https://github.com/dotnet/runtime/issues/49407
//https://youtu.be/vNPybpatlUU?t=210

namespace F0.System;

public class TimeZoneInfoTests
{
	[Fact]
	public void FindSystemTimeZoneById_Net50()
	{
		TimeZoneInfo info;

		if (OperatingSystem.IsWindows())
		{
			info = TimeZoneInfo.FindSystemTimeZoneById(GetWindowsId());
			info.HasIanaId.Should().BeFalse();
		}
		else
		{
			info = TimeZoneInfo.FindSystemTimeZoneById(GetIanaId());
			info.HasIanaId.Should().BeTrue();
		}

		info.BaseUtcOffset.Should().Be(TimeSpan.Zero);
		info.SupportsDaylightSavingTime.Should().BeTrue();
	}

	[Fact]
	public void TryConvertIanaIdToWindowsId()
	{
		bool success = TimeZoneInfo.TryConvertIanaIdToWindowsId(GetIanaId(), out string? windowsId);

		success.Should().BeTrue();
		windowsId.Should().Be(GetWindowsId());
	}

	[Fact]
	public void TryConvertWindowsIdToIanaId()
	{
		bool success = TimeZoneInfo.TryConvertWindowsIdToIanaId(GetWindowsId(), out string? ianaId);

		success.Should().BeTrue();
		ianaId.Should().Be(GetIanaId());
	}

	[Fact]
	public void TryConvertWindowsIdToIanaId_Region()
	{
		bool success = TimeZoneInfo.TryConvertWindowsIdToIanaId(GetWindowsId(), "PT", out string? ianaId);

		success.Should().BeTrue();
		ianaId.Should().Be("Europe/Lisbon");
	}

	[SupportedOSPlatform("windows")]
	private static string GetWindowsId()
	{
		return "GMT Standard Time";
	}

	[UnsupportedOSPlatform("windows")]
	private static string GetIanaId()
	{
		return "Europe/London";
	}
}
