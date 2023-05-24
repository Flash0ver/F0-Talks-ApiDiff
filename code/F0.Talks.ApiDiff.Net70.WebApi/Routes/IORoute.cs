namespace F0.Routes;

internal static class IORoute
{
	public static void MapIO(this IEndpointRouteBuilder endpoints)
	{
		RouteGroupBuilder builder = endpoints.MapGroup("/io");

		builder.MapPost("/directory/create", CreateDirectory)
			.WithName("IO Directory Create")
			.WithOpenApi();
		builder.MapPost("/file/create", CreateFileAsync)
			.WithName("IO File Create")
			.WithOpenApi();
		builder.MapGet("/file/read/1", Read1Async)
			.WithName("IO File Read 1")
			.WithOpenApi();
		builder.MapGet("/file/read/2", Read2Async)
			.WithName("IO File Read 2")
			.WithOpenApi();
		builder.MapGet("/file/update", UpdateFileAsync)
			.WithName("IO File Update")
			.WithOpenApi();
		builder.MapDelete("/file/delete", DeleteFile)
			.WithName("IO File Delete")
			.WithOpenApi();
	}

	//CreateDirectory(String, UnixFileMode)
	//Directory.CreateTempSubdirectory(String)
	private static IStatusCodeHttpResult CreateDirectory(IWebHostEnvironment env)
	{
		IFileInfo fileInfo = env.ContentRootFileProvider.GetFileInfo("MyDirectory");

		DirectoryInfo directoryInfo;
		if (!OperatingSystem.IsWindows())
		{
			//UnixFileMode
			directoryInfo = Directory.CreateDirectory(fileInfo.PhysicalPath!, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
		}
		else
		{
			directoryInfo = Directory.CreateTempSubdirectory("prefix");
		}

		if (directoryInfo.Exists)
		{
			Directory.Delete(directoryInfo.FullName);
		}

		return TypedResults.Ok(Format(directoryInfo));

		static object Format(DirectoryInfo directoryInfo)
		{
			return new
			{
				directoryInfo.Name,
				directoryInfo.FullName,
				directoryInfo.Exists,
				directoryInfo.UnixFileMode,
				directoryInfo.LastAccessTimeUtc,
				directoryInfo.LastWriteTimeUtc,
			};
		}
	}

	private static async Task<IStatusCodeHttpResult> CreateFileAsync(IWebHostEnvironment env, string content, CancellationToken cancellationToken)
	{
		IFileInfo fileInfo = env.ContentRootFileProvider.GetFileInfo("MyFile.txt");

		if (fileInfo.PhysicalPath is null)
		{
			return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
		}
		//Path.Exists
		if (Path.Exists(fileInfo.PhysicalPath))
		{
			return TypedResults.Conflict();
		}

		string[] contents = new string[3];
		contents[0] = "Hello, World!";
		contents[1] = $"{nameof(DateTimeOffset.Now)}: {DateTimeOffset.Now.ToString("s", DateTimeFormatInfo.InvariantInfo)}";
		contents[2] = $"Content: {content}";
		await File.WriteAllLinesAsync(fileInfo.PhysicalPath, contents, Encoding.Unicode, cancellationToken);

		return TypedResults.Created("/io/file/read");
	}

	private static async Task<IStatusCodeHttpResult> Read1Async(IWebHostEnvironment env, CancellationToken cancellationToken)
	{
		IFileInfo fileInfo = env.ContentRootFileProvider.GetFileInfo("MyFile.txt");
		//Path.Exists
		if (!Path.Exists(fileInfo.PhysicalPath))
		{
			return TypedResults.NotFound();
		}

		//FileAttributes attributes = File.GetAttributes((SafeFileHandle)null);
		//DateTime creation = File.GetCreationTime((SafeFileHandle)null);
		//DateTime creationUtc = File.GetCreationTimeUtc((SafeFileHandle)null);
		//DateTime lastAccess = File.GetLastAccessTime((SafeFileHandle)null);
		//DateTime lastAccessUtc = File.GetLastAccessTimeUtc((SafeFileHandle)null);
		//DateTime lastWrite = File.GetLastWriteTime((SafeFileHandle)null);
		//DateTime lastWriteUtc = File.GetLastWriteTimeUtc((SafeFileHandle)null);
		//UnixFileMode unixFileMode = File.GetUnixFileMode((SafeFileHandle)null);
		//UnixFileMode unixFileMode = File.GetUnixFileMode(fileInfo.PhysicalPath);

		int length = checked((int)fileInfo.Length);
		byte[] rented = ArrayPool<byte>.Shared.Rent(length);
		byte[] array = GC.AllocateUninitializedArray<byte>(2 * 10, false);
		Memory<byte> atLeastBuffer = rented.AsMemory();
		Memory<byte> exactlyBuffer = array.AsMemory();

		//ReadAtLeast / ReadAtLeastAsync
		using FileStream stream = File.OpenRead(fileInfo.PhysicalPath);
		int totalNumberOfBytesRead = await stream.ReadAtLeastAsync(atLeastBuffer, 2 * 10, true, cancellationToken);

		//ReadExactly / ReadExactlyAsync (byte[] + offset + count & Span<byte>)
		stream.Seek(0, SeekOrigin.Begin);
		await stream.ReadExactlyAsync(exactlyBuffer, cancellationToken);

		_ = Encoding.Unicode.Preamble;
		string atLeastText = Encoding.Unicode.GetString(atLeastBuffer.Span.Slice(0, totalNumberOfBytesRead));
		string exactlyText = Encoding.Unicode.GetString(exactlyBuffer.Span);

		ArrayPool<byte>.Shared.Return(rented, false);

		//ReadLinesAsync
		IAsyncEnumerable<string> lines = File.ReadLinesAsync(fileInfo.PhysicalPath, Encoding.Unicode, cancellationToken);

		return TypedResults.Ok(new { totalNumberOfBytesRead, atLeastText, exactlyText, lines });
	}

	private static async Task<IStatusCodeHttpResult> Read2Async(IWebHostEnvironment env, CancellationToken cancellationToken)
	{
		IFileInfo fileInfo = env.ContentRootFileProvider.GetFileInfo("MyFile.txt");
		//Path.Exists
		if (!Path.Exists(fileInfo.PhysicalPath))
		{
			return TypedResults.NotFound();
		}

		//StreamReader & StringReader & TextReader
		StreamReader reader = File.OpenText(fileInfo.PhysicalPath);
		//ReadLineAsync(CancellationToken)
		string? firstLine = await reader.ReadLineAsync(cancellationToken);
		//ReadToEndAsync(CancellationToken)
		string rest = await reader.ReadToEndAsync(cancellationToken);

		return TypedResults.Ok(new { firstLine, rest });
	}

	private static async Task<IStatusCodeHttpResult> UpdateFileAsync(IWebHostEnvironment env, string contents, CancellationToken cancellationToken)
	{
		IFileInfo fileInfo = env.ContentRootFileProvider.GetFileInfo("MyFile.txt");
		//Path.Exists
		if (!Path.Exists(fileInfo.PhysicalPath))
		{
			return TypedResults.NotFound();
		}

		//File.SetAttributes((SafeFileHandle)null, FileAttributes.Normal);
		//File.SetCreationTime((SafeFileHandle)null, DateTime.Now);
		//File.SetCreationTimeUtc((SafeFileHandle)null, DateTime.UtcNow);
		//File.SetLastAccessTime((SafeFileHandle)null, DateTime.Now);
		//File.SetLastAccessTimeUtc((SafeFileHandle)null, DateTime.UtcNow);
		//File.SetLastWriteTime((SafeFileHandle)null, DateTime.Now);
		//File.SetLastWriteTimeUtc((SafeFileHandle)null, DateTime.UtcNow);
		//File.SetUnixFileMode((SafeFileHandle)null, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
		//File.SetUnixFileMode(fileInfo.PhysicalPath, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);

		await File.AppendAllTextAsync(fileInfo.PhysicalPath, contents, Encoding.Unicode, cancellationToken);

		return TypedResults.Ok();
	}

	private static IStatusCodeHttpResult DeleteFile(IWebHostEnvironment env, CancellationToken cancellationToken)
	{
		IFileInfo fileInfo = env.ContentRootFileProvider.GetFileInfo("MyFile.txt");
		//Path.Exists
		if (!Path.Exists(fileInfo.PhysicalPath))
		{
			return TypedResults.NotFound();
		}

		File.Delete(fileInfo.PhysicalPath);

		return TypedResults.Ok();
	}
}
