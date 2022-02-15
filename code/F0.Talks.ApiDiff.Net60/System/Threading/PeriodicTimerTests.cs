//PeriodicTimer
//https://github.com/dotnet/runtime/issues/31525
//https://youtu.be/_Svjl1-jauY?t=2213

namespace F0.System.Threading;

public class PeriodicTimerTests
{
	[Fact]
	public async Task WaitForNextTickAsync()
	{
		using PeriodicTimer timer = new(TimeSpan.FromMilliseconds(64));

		ValueTask<bool> task = timer.WaitForNextTickAsync();
		task.IsCompletedSuccessfully.Should().BeFalse();

		await Task.Delay(TimeSpan.FromMilliseconds(32));
		task.IsCompletedSuccessfully.Should().BeFalse();

		bool result = await task;
		result.Should().BeTrue();
	}

	[Fact]
	public void WaitForNextTickAsync_Dispose()
	{
		using PeriodicTimer timer = new(TimeSpan.FromMilliseconds(64));

		ValueTask<bool> task = timer.WaitForNextTickAsync();
		task.IsCompletedSuccessfully.Should().BeFalse();

		timer.Dispose();

		task.IsCompletedSuccessfully.Should().BeTrue();
		task.Result.Should().BeFalse();

		task = timer.WaitForNextTickAsync();
		task.IsCompletedSuccessfully.Should().BeTrue();
		task.Result.Should().BeFalse();
	}
}
