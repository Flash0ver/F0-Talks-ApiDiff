namespace F0.Routes;

internal static class TarRoute
{
	public static void MapTar(this IEndpointRouteBuilder endpoints)
	{
		RouteGroupBuilder builder = endpoints.MapGroup("/tar");

		builder.MapGet("/test", Test)
			.WithName("tar test")
			.WithOpenApi();
		builder.MapGet("/read", ReadAsync)
			.WithName("tar read")
			.WithOpenApi();
		builder.MapPost("/create", CreateAsync)
			.WithName("tar create")
			.WithOpenApi();
		builder.MapDelete("/delete", Delete)
			.WithName("tar delete")
			.WithOpenApi();
	}

	private static IStatusCodeHttpResult Test()
	{
		Type type = typeof(TarEntry);
		if (typeof(GnuTarEntry).IsAssignableTo(type) &&
			typeof(PaxGlobalExtendedAttributesTarEntry).IsAssignableTo(type) &&
			typeof(PaxTarEntry).IsAssignableTo(type) &&
			typeof(PosixTarEntry).IsAssignableTo(type) &&
			typeof(UstarTarEntry).IsAssignableTo(type) &&
			typeof(V7TarEntry).IsAssignableTo(type))
		{
			return TypedResults.Ok();
		}

		return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
	}

	private static async Task<IStatusCodeHttpResult> ReadAsync(IWebHostEnvironment env, CancellationToken cancellationToken)
	{
		IFileInfo fileInfo = env.ContentRootFileProvider.GetFileInfo("MyArchive.tar");

		if (!Path.Exists(fileInfo.PhysicalPath))
		{
			return TypedResults.NotFound();
		}

		await using FileStream file = new(fileInfo.PhysicalPath, FileMode.Open, FileAccess.Read, FileShare.Write);
		await using TarReader tar = new(file);
		using StreamReader reader = new(file);

		List<string> content = new();

		while (await reader.ReadLineAsync(cancellationToken) is { } line)
		{
			content.Add(line);
		}

		return TypedResults.Ok(content);
	}

	private static async Task<IStatusCodeHttpResult> CreateAsync(IWebHostEnvironment env, string message, CancellationToken cancellationToken)
	{
		IFileInfo fileInfo = env.ContentRootFileProvider.GetFileInfo("MyArchive.tar");

		if (fileInfo.PhysicalPath is null)
		{
			return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
		}
		if (Path.Exists(fileInfo.PhysicalPath))
		{
			return TypedResults.Conflict();
		}

		await using FileStream file = new(fileInfo.PhysicalPath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
		await using TarWriter tar = new(file);
		await using StreamWriter writer = new(file);

		await writer.WriteLineAsync("Hello, World!");
		await writer.WriteLineAsync($"{nameof(DateTimeOffset.Now)}: {DateTimeOffset.Now.ToString("s", DateTimeFormatInfo.InvariantInfo)}");
		await writer.WriteLineAsync($"Message: {message}");
		await writer.FlushAsync();

		return TypedResults.Created("/tar/read");
	}

	private static IStatusCodeHttpResult Delete(IWebHostEnvironment env, CancellationToken cancellationToken)
	{
		IFileInfo fileInfo = env.ContentRootFileProvider.GetFileInfo("MyArchive.tar");

		if (!Path.Exists(fileInfo.PhysicalPath))
		{
			return TypedResults.NotFound();
		}

		File.Delete(fileInfo.PhysicalPath);

		return TypedResults.Ok();
	}

	public static async Task MoreAsync(Stream stream, CancellationToken cancellationToken = default)
	{
		PaxTarEntry entry = new(TarEntryType.Directory, "directory");
		_ = entry.Name;
		_ = entry.Format == TarEntryFormat.Pax;

		_ = TarFile.CreateFromDirectoryAsync("sourceDirectoryName", "destinationFileName", true, cancellationToken);
		_ = TarFile.ExtractToDirectoryAsync("sourceFileName", "destinationDirectoryName", true, cancellationToken);

		await using TarWriter writer = new(stream, TarEntryFormat.Pax, true);
		await writer.WriteEntryAsync(entry, cancellationToken);
		_ = writer.Format;

		await using TarReader reader = new(stream, true);
		while (await reader.GetNextEntryAsync(false, cancellationToken) is { } next)
		{
			_ = next.Name;
		}
	}
}
