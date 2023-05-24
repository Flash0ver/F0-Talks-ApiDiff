namespace F0.System.Threading.Tasks;

public class TaskAsyncEnumerableExtensionsTests
{
	[Fact]
	public void ToBlockingEnumerable()
	{
		IAsyncEnumerable<int> source = CreateEnumerableAsync();

		IEnumerable<int> enumerable = source.ToBlockingEnumerable(CancellationToken.None);

		Assert.Equal(new int[] { 1, 2, 3 }, enumerable);
		Assert.Equal(6, enumerable.Sum());
	}

	[Fact]
	public async Task System_Linq_Async()
	{
		IAsyncEnumerable<int> source = CreateEnumerableAsync();

		int sum = await source.SumAsync();

		Assert.Equal(6, sum);
	}

	private static async IAsyncEnumerable<int> CreateEnumerableAsync()
	{
		var delay = TimeSpan.FromMilliseconds(10);

		yield return 1;
		await Task.Delay(delay);
		yield return 2;
		await Task.Delay(delay);
		yield return 3;
	}
}
