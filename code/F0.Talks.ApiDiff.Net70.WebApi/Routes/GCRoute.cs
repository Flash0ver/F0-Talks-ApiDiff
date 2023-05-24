namespace F0.Routes;

internal static class GCRoute
{
	public static void MapGC(this IEndpointRouteBuilder endpoints)
	{
		RouteGroupBuilder builder = endpoints.MapGroup("/gc");

		builder.MapGet("/new", GetNew)
			.WithName("GC Get new")
			.WithOpenApi();
		builder.MapGet("/old", GetOld)
			.WithName("GC Get old")
			.WithOpenApi();
		builder.MapPut("/collect", PutCollect)
			.WithName("GC Put Collect")
			.WithOpenApi();
	}

	//GC.GetConfigurationVariables
	//GC.GetTotalPauseDuration
	private static IStatusCodeHttpResult GetNew()
	{
		IReadOnlyDictionary<string, object> configurations = GC.GetConfigurationVariables();
		TimeSpan duration = GC.GetTotalPauseDuration();

		Dictionary<string, object> dictionary = new()
		{
			{ nameof(duration), duration.ToString("G", DateTimeFormatInfo.InvariantInfo) },
			{ nameof(configurations), configurations },
		};
		return TypedResults.Ok(dictionary);
	}

	private static IStatusCodeHttpResult GetOld()
	{
		Dictionary<string, object> dictionary = new()
		{
			{ nameof(GC.GetTotalMemory), GC.GetTotalMemory(false).ToString("N0", NumberFormatInfo.InvariantInfo) },
			{ nameof(GC.GetTotalAllocatedBytes), GC.GetTotalAllocatedBytes().ToString("N0", NumberFormatInfo.InvariantInfo) },
			{ nameof(GC.MaxGeneration), GC.MaxGeneration },
		};
		for (int i = 0; i <= GC.MaxGeneration; i++)
		{
			dictionary.Add($"{nameof(GC.CollectionCount)} {i}", GC.CollectionCount(i));
		}
		return TypedResults.Ok(dictionary);
	}

	//GCCollectionMode.Aggressive
	private static IStatusCodeHttpResult PutCollect()
	{
		GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true, true);
		return TypedResults.Ok();
	}
}
