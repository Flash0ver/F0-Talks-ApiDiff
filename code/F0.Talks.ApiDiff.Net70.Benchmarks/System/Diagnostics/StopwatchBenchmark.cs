namespace F0.System.Diagnostics;

[ShortRunJob]
[MemoryDiagnoser]
public class StopwatchBenchmark
{
	[Benchmark(Baseline = true)]
	public TimeSpan StartNew()
	{
		var stopwatch = Stopwatch.StartNew();
		return stopwatch.Elapsed;
	}

	[Benchmark]
	public TimeSpan GetElapsedTime()
	{
		long startingTimestamp = Stopwatch.GetTimestamp();
		return Stopwatch.GetElapsedTime(startingTimestamp);
	}
}
