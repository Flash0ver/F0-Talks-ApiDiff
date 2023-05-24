using F0.System.Diagnostics.Metrics;
using F0.System.Numerics;
using F0.System.Runtime.InteropServices;

MyConsole.WriteLineRed("Hello, World!");

switch (args)
{
	case ["sum", ..]:
		RunGenericMathExample(args.AsSpan().Slice(1));
		break;
	case ["metrics"]:
		RunMetricsExample();
		break;
	case ["pinvoke", "il" or "jit"]:
		RunPInvokeGeneratorExample(false);
		break;
	case ["pinvoke", "gen"]:
		RunPInvokeGeneratorExample(true);
		break;
	default:
		MyConsole.WriteLineGreen("* sum [<numbers>]");
		MyConsole.WriteLineGreen("* metrics");
		MyConsole.WriteLineGreen("* pinvoke il|jit");
		MyConsole.WriteLineGreen("* pinvoke gen");
		break;
}

static void RunGenericMathExample(ReadOnlySpan<string> args)
{
	long ticks = Stopwatch.GetTimestamp();
	long bytes = GC.GetAllocatedBytesForCurrentThread();

	//var sum = args.Select(static int (string arg) => Int32.Parse(arg)).Sum();
	var sum = GenericMathExample.Sum<int>(args);

	TimeSpan elapsed = Stopwatch.GetElapsedTime(ticks);
	long allocated = GC.GetAllocatedBytesForCurrentThread() - bytes;

	MyConsole.WriteLineGreen($"Sum: {sum.ToString(NumberFormatInfo.InvariantInfo)}");
	MyConsole.WriteLineCyan($"Elapsed Milliseconds: {elapsed.TotalMilliseconds.ToString(NumberFormatInfo.InvariantInfo)}");
	MyConsole.WriteLineCyan($"Bytes Allocated: {allocated}");
}

static void RunMetricsExample()
{
	using MetricsExample example = new();
	example.Run();
}

static void RunPInvokeGeneratorExample(bool useGenerator)
{
	if (useGenerator)
	{
		PInvokeGeneratorExample.RunGenerator();
	}
	else
	{
		PInvokeGeneratorExample.RunILJit();
	}
}
