//https://github.com/dotnet/runtime/issues/1946
//https://youtu.be/3s2OAi9EbCA?t=2846

namespace F0.System.Threading.Tasks;

public class ParallelTests
{
	[Fact]
	public async Task ForEachAsync()
	{
		IEnumerable<int> collection = Enumerable.Range(0, 10);
		ConcurrentBag<int> bag = new();
		TaskCompletionSource completion = new();

		using CancellationTokenSource cancellation = new();
		ParallelOptions options = new()
		{
			CancellationToken = cancellation.Token,
		};

		Task operation = Parallel.ForEachAsync(collection, options, async (item, cancellationToken) =>
		{
			await completion.Task;

			bag.Add(++item);
		});

		completion.SetResult();

		await operation;

		bag.Should().HaveCount(10);
		bag.Should().Contain(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
	}
}
