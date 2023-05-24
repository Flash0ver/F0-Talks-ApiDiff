namespace F0.System.Linq;

[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[MemoryDiagnoser]
public class EnumerableBenchmarks
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
	public float Min()
	{
		return _source.Min();
	}

	[Benchmark]
	public float Max()
	{
		return _source.Max();
	}

	[Benchmark]
	public float Sum()
	{
		return _source.Sum();
	}

	[Benchmark]
	public float Average()
	{
		return _source.Average();
	}
}
