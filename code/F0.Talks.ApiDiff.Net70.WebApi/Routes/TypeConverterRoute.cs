using F0.Options;

namespace F0.Routes;

internal static class TypeConverterRoute
{
	public static void MapTypeConverter(this IEndpointRouteBuilder endpoints)
	{
		RouteGroupBuilder builder = endpoints.MapGroup("/type-converter");

		builder.MapGet("/config", Config)
			.WithName("Get Config")
			.WithOpenApi();
	}

	private static IStatusCodeHttpResult Config(IOptionsMonitor<MyConfig> options)
	{
		return TypedResults.Ok(options.CurrentValue);
	}
}
