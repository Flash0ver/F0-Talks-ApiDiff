namespace F0.System.Threading.Tasks;

[Collection(NoParallelizationCollection.Name)]
public class TaskTests
{
	private readonly TaskCompletionSource _tcs = new();
	private readonly CancellationTokenSource _cts = new();

	[Fact(Timeout = 1_000)]
	public void Wait_TimeSpan_CancellationToken_Timeout()
	{
		var timeout = TimeSpan.FromMilliseconds(10);

		long start = Stopwatch.GetTimestamp();
		bool completed = _tcs.Task.Wait(timeout, _cts.Token);
		TimeSpan elapsed = Stopwatch.GetElapsedTime(start);

		Assert.InRange(elapsed.TotalMilliseconds, 10, 50);
		Assert.False(completed);
	}

	[Fact(Timeout = 1_000)]
	public void Wait_TimeSpan_CancellationToken_Cancel()
	{
		Action act = () => _tcs.Task.Wait(TimeSpan.FromMilliseconds(1_000), _cts.Token);

		_cts.CancelAfter(TimeSpan.FromMilliseconds(10));

		long start = Stopwatch.GetTimestamp();
		var exception = Assert.Throws<OperationCanceledException>(act);
		TimeSpan elapsed = Stopwatch.GetElapsedTime(start);

		Assert.InRange(elapsed.TotalMilliseconds, 5, 50);
		Assert.Equal(_cts.Token, exception.CancellationToken);
	}
}
