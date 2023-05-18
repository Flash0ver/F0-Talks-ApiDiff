//https://github.com/dotnet/runtime/issues/47525
//https://youtu.be/MFUXZLyDqbU?t=4978
//https://youtu.be/8kDxa8BfQmw?t=3442
//https://youtu.be/_Svjl1-jauY?t=196

namespace F0.System.Threading.Tasks;

public class TaskTests
{
	[Fact]
	public async Task WaitAsync_Timeout()
	{
		TaskCompletionSource<int> completion = new();

		Func<Task<int>> act = () => completion.Task.WaitAsync(TimeSpan.FromMilliseconds(10));

		await act.Should().ThrowExactlyAsync<TimeoutException>();

		completion.Task.IsCompleted.Should().BeFalse();
	}

	[Fact]
	public async Task WaitAsync_Cancellation()
	{
		TaskCompletionSource completion = new();

		Func<Task> act = () => completion.Task.WaitAsync(new CancellationToken(true));

		await act.Should().ThrowExactlyAsync<TaskCanceledException>();

		completion.Task.IsCompleted.Should().BeFalse();
	}
}
