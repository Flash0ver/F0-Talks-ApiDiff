namespace F0.System.Diagnostics.Metrics;

internal class MetricsExample : IDisposable
{
	private readonly Meter _meter;
	private readonly MeterListener _listener;

	private readonly Counter<int> _iterations;
	private readonly Histogram<int> _numbers;
	private readonly UpDownCounter<int> _random;
	private readonly ObservableUpDownCounter<int> _observable;

	private int _diff;

	public MetricsExample()
	{
		_meter = new Meter("Example", "1.0.0");
		_listener = new MeterListener();

		_iterations = _meter.CreateCounter<int>("Iterations");
		_numbers = _meter.CreateHistogram<int>("Random Numbers");

		_random = _meter.CreateUpDownCounter<int>("Random Diff");
		_observable = _meter.CreateObservableUpDownCounter<int>("Random Diff Observable", () => _diff);

		_listener.InstrumentPublished = (Instrument instrument, MeterListener listener) =>
		{
			if (instrument.Name.Contains("Observable", StringComparison.InvariantCultureIgnoreCase))
			{
				listener.EnableMeasurementEvents(instrument);
			}
		};
		_listener.SetMeasurementEventCallback<int>((Instrument instrument, int measurement, ReadOnlySpan<KeyValuePair<string, object?>> tags, object? state) =>
		{
			MyConsole.WriteLineCyan($"{instrument.Name}: {measurement}");
		});
		_listener.Start();
	}

	public void Run()
	{
		Console.WriteLine("Press any key to exit");

		var random = Random.Shared.Next(10);
		_random.Add(random);

		while (!Console.KeyAvailable)
		{
			Thread.Sleep(1_000);
			int current = Random.Shared.Next(10);
			_diff = current - random;

			_iterations.Add(1);
			_numbers.Record(random);
			_random.Add(_diff);

			random = current;

			_listener.RecordObservableInstruments();
		}
	}

	public void Dispose()
	{
		_listener.DisableMeasurementEvents(_observable);
		_listener.Dispose();
		_meter.Dispose();
	}
}
