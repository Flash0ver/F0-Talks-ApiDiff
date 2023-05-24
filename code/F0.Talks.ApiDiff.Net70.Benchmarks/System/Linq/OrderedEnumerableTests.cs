namespace F0.System.Linq;

[ShortRunJob]
[MemoryDiagnoser]
public class OrderedEnumerableTests
{
	private IEnumerable<float> _source = null!;

	[Params(4, 1_024)]
	public int Count { get; set; }

	[GlobalSetup]
	public void GlobalSetup()
	{
		Random random = new(0x_F0);
		var array = new float[Count];
		for (int i = 0; i < Count; i++)
		{
			array[i] = random.NextSingle();
		}

		_source = array;
	}

	[Benchmark]
	public void Order()
	{
		foreach (float item in _source.Order())
		{
			_ = item;
		}
	}

	[Benchmark]
	public void OrderBy()
	{
		foreach (float item in _source.OrderBy(static element => element))
		{
			_ = item;
		}
	}

	[Benchmark]
	public void OrderDescending()
	{
		foreach (float item in _source.OrderDescending())
		{
			_ = item;
		}
	}

	[Benchmark]
	public void OrderByDescending()
	{
		foreach (float item in _source.OrderByDescending(static element => element))
		{
			_ = item;
		}
	}
}
