namespace F0.System.Threading.Tasks.Dataflow;

public class TransformManyBlockTests
{
	private readonly List<string> _result = new();

	[Fact]
	public async Task TransformManyBlock_2_IAsyncEnumerable_1()
	{
		var timeout = TimeSpan.FromMilliseconds(1_000);
		CancellationTokenSource cts = new();
		cts.CancelAfter(timeout);

		ExecutionDataflowBlockOptions dataflowBlockOptions = new()
		{
			CancellationToken = cts.Token,
			EnsureOrdered = true,
			MaxDegreeOfParallelism = 1,
		};

		var tb = new TransformBlock<int[], ImmutableArray<int>>(Transform);
		var tmb = new TransformManyBlock<ImmutableArray<int>, string>(TransformAsync, dataflowBlockOptions);
		var ab = new ActionBlock<string>(Output);

		DataflowLinkOptions linkOptions = new()
		{
			PropagateCompletion = true,
		};
		using IDisposable linked1 = tb.LinkTo(tmb, linkOptions);
		using IDisposable linked2 = tmb.LinkTo(ab, linkOptions);

		Assert.True(tb.Post(new[] { 1 }));
		Assert.True(tb.Post(new[] { 2, 3 }));
		Assert.True(tb.Post(new[] { 4, 5, 6 }));
		tb.Complete();
		await ab.Completion.WaitAsync(timeout);

		Assert.Equal(new[] { "1", "2", "3", "4", "5", "6" }, _result);
	}

	private static ImmutableArray<int> Transform(int[] input)
	{
		return ImmutableArray.Create(input);
	}

	private static async IAsyncEnumerable<string> TransformAsync(ImmutableArray<int> input)
	{
		foreach (int number in input)
		{
			string text = await FormatAsync(number);
			yield return text;
		}
	}

	private static async Task<string> FormatAsync<TNumber>(TNumber number)
		where TNumber : INumber<TNumber>
	{
		await Task.Yield();
		return number.ToString(null, NumberFormatInfo.InvariantInfo);
	}

	private void Output(string input)
	{
		_result.Add(input);
	}
}
