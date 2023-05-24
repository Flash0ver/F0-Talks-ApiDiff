namespace F0.Routes;

internal static class HttpRoute
{
	public static void MapHttp(this IEndpointRouteBuilder endpoints)
	{
		RouteGroupBuilder builder = endpoints.MapGroup("/http");

		builder.MapGet("/", GetAsync)
			.WithName("HTTP")
			.WithOpenApi();
		builder.MapGet("/quic", QuicAsync)
			.WithName("QUIC")
			.WithOpenApi();
		builder.MapGet("/websocket", WebSocketAsync)
			.WithName("WebSocket")
			.WithOpenApi();
	}

	private static async Task<IStatusCodeHttpResult> GetAsync(IHttpClientFactory httpClientFactory, CancellationToken cancellationToken)
	{
		using HttpClient httpClient = httpClientFactory.CreateClient();

		try
		{
			//StringSyntaxAttribute
			using HttpRequestMessage request = new(HttpMethod.Get, "http://example.org/");
			request.Version = new Version(3, 0);
			request.VersionPolicy = HttpVersionPolicy.RequestVersionExact;
			using HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
			return TypedResults.Ok(await response.Content.ReadAsStringAsync(cancellationToken));
		}
		catch (HttpProtocolException hpe)
		{
			return TypedResults.Problem($"{nameof(hpe.ErrorCode)}: {hpe.ErrorCode}", null, StatusCodes.Status500InternalServerError);
		}
		catch (HttpRequestException hre) when (hre.InnerException is HttpProtocolException hpe)
		{
			return TypedResults.Problem($"{nameof(hpe.ErrorCode)}: {hpe.ErrorCode}", null, StatusCodes.Status500InternalServerError);
		}
	}

	private static async Task<IStatusCodeHttpResult> QuicAsync()
	{
		await Task.Yield();

		string response = $"""
			{nameof(QuicListener)} {nameof(QuicListener.IsSupported)} {QuicListener.IsSupported}
			{nameof(QuicConnection)} {nameof(QuicConnection.IsSupported)} {QuicConnection.IsSupported}
			{QuicStream.Null}
			""";

		return TypedResults.Ok(response);
	}


	private static async Task<IStatusCodeHttpResult> WebSocketAsync(CancellationToken cancellationToken)
	{
		HttpClientHandler handler = new();
		//handler.CookieContainer = new CookieContainer();
		//handler.UseCookies = true;
		//handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
		//handler.Credentials = CredentialCache.DefaultCredentials;
		//handler.UseProxy = true;
		//handler.Proxy = new WebProxy();
		//handler.ClientCertificates.Add();
		HttpMessageInvoker invoker = new(handler);

		ClientWebSocket webSocket = new();
		webSocket.Options.CollectHttpResponseDetails = true;
		webSocket.Options.HttpVersion = HttpVersion.Version20;
		webSocket.Options.HttpVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;

		try
		{
			await webSocket.ConnectAsync(new Uri("ws://example.org/"), invoker, cancellationToken);
			var details = webSocket.HttpResponseHeaders;
			webSocket.HttpResponseHeaders = null;
			return TypedResults.Ok($"{details}");
		}
		catch (WebSocketException wse)
		{
			return TypedResults.Problem($"{nameof(wse.WebSocketErrorCode)}: {wse.WebSocketErrorCode}", null, StatusCodes.Status500InternalServerError);
		}
	}
}
