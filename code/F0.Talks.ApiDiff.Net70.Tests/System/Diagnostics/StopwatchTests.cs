namespace F0.System.Diagnostics;

public class StopwatchTests
{
	[Fact]
	public async Task GetElapsedTime_Int64()
	{
		long start = Stopwatch.GetTimestamp();

		await Task.Delay(TimeSpan.FromMilliseconds(10));

		TimeSpan elapsed = Stopwatch.GetElapsedTime(start);

		Assert.InRange(elapsed.TotalMilliseconds, 10, 50);
	}

	[Fact]
	public async Task GetElapsedTime_Int64_Int64()
	{
		long start = Stopwatch.GetTimestamp();

		await Task.Delay(TimeSpan.FromMilliseconds(10));

		long end = Stopwatch.GetTimestamp();

		TimeSpan elapsed = Stopwatch.GetElapsedTime(start, end);

		Assert.InRange(elapsed.TotalMilliseconds, 10, 50);
	}
}
