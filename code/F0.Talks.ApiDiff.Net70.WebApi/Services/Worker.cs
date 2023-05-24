namespace F0.Services;

public sealed class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly PeriodicTimer _timer;

	public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
	{
		_logger = logger;
		_httpClientFactory = httpClientFactory;

		_timer = new PeriodicTimer(TimeSpan.FromMilliseconds(1_000));
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (await _timer.WaitForNextTickAsync(stoppingToken))
		{
			using HttpClient httpClient = _httpClientFactory.CreateClient();
			httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue()
			{
				NoCache = true,
				NoStore = true,
			};
			//HttpRequestHeaders.Protocol
			httpClient.DefaultRequestHeaders.Protocol = "Unknown";

			//StringSyntaxAttribute
			await using Stream response = await httpClient.GetStreamAsync("http://example.org/", stoppingToken);
			await using MemoryStream stream = new(1 * 1_024 * 1_024);
			await response.CopyToAsync(stream, stoppingToken);
			using StreamReader reader = new(stream);
			string message = await reader.ReadToEndAsync(stoppingToken);

			_logger.LogInformation("Response: {response}", message);
			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

			/*
			const string requestUri = "http://example.org/";
			MyRow value = new() { Text = "Text" };
			JsonSerializerOptions? options = new();
			//.NET 5
			MyRow? get = await httpClient.GetFromJsonAsync<MyRow>(requestUri, options, stoppingToken);
			HttpResponseMessage post = await httpClient.PostAsJsonAsync<MyRow>(requestUri, value, options, stoppingToken);
			HttpResponseMessage put = await httpClient.PutAsJsonAsync<MyRow>(requestUri, value, options, stoppingToken);
			// .NET 7
			HttpResponseMessage patch = await httpClient.PatchAsJsonAsync<MyRow>(requestUri, value, options, stoppingToken);
			MyRow? delete = await httpClient.DeleteFromJsonAsync<MyRow>(requestUri, options, stoppingToken);
			*/

			_ = HttpMethod.Connect;
		}
	}

	public override void Dispose()
	{
		_timer.Dispose();
		base.Dispose();
	}
}
