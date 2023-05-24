using F0.Data;

namespace F0.Routes;

internal static class DataRoute
{
	public static void MapData(this IEndpointRouteBuilder endpoints)
	{
		RouteGroupBuilder builder = endpoints.MapGroup("/data");

		builder.MapGet("/", GetAllAsync)
			.WithName("Data Read All")
			.WithOpenApi();
		builder.MapGet("/id/{id:int}", GetOneAsyncById)
			.WithName("Data Read One by ID")
			.WithOpenApi();
		builder.MapGet("/value/{value}", GetOneAsyncByValue)
			.WithName("Data Read One by Value")
			.WithOpenApi();
		builder.MapPost("/", PostAsync)
			.WithName("Data Create")
			.WithOpenApi();
		builder.MapPut("/{id:int}/{text}", PutAsync)
			.WithName("Data Update")
			.WithOpenApi();
		builder.MapDelete("/{id:int}", DeleteAsync)
			.WithName("Data Delete")
			.WithOpenApi();
	}

	private static async Task<IStatusCodeHttpResult> GetAllAsync(MyDbContext db, CancellationToken cancellationToken)
	{
		//Order<T>(IQueryable<T>)
		//Order<T>(IQueryable<T>, IComparer<T>)
		//OrderDescending<T>(IQueryable<T>)
		//OrderDescending<T>(IQueryable<T>, IComparer<T>)
		MyRow[] entities = await db.MyTable.ToArrayAsync(cancellationToken);

		return TypedResults.Ok(entities);
	}

	private static async Task<IStatusCodeHttpResult> GetOneAsyncById(MyDbContext db, int id, CancellationToken cancellationToken)
	{
		MyRow entity = await db.MyTable.FirstAsync(entity => entity.MyRowId == id, cancellationToken);

		return TypedResults.Ok(entity);
	}

	//DbDataSource
	private static async Task<IStatusCodeHttpResult> GetOneAsyncByValue(DbDataSource db, string value, CancellationToken cancellationToken)
	{
		string connectionString = db.ConnectionString;
		//_ = db.CreateBatch();
		//_ = db.CreateConnection();
		//_ = db.OpenConnection();
		//_ = db.OpenConnectionAsync(cancellationToken);

		await using DbCommand command = db.CreateCommand(null);
		command.CommandText = $"""
			SELECT *
			FROM MyTable
			WHERE Text IN ($value);
			""";

		DbParameter parameter = command.CreateParameter();
		parameter.ParameterName = "$value";
		parameter.Value = value;
		parameter.DbType = DbType.String;
		parameter.Direction = ParameterDirection.Input;
		command.Parameters.Add(parameter);

		List<MyRow> result = new();

		await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
		if (reader.HasRows)
		{
			while (await reader.ReadAsync(cancellationToken))
			{
				MyRow record = new()
				{
					MyRowId = reader.GetInt32(0),
					Text = reader.GetString(1),
				};
				result.Add(record);
			}
		}

		await db.DisposeAsync();

		return TypedResults.Ok(new { connectionString, result });
	}

	private static async Task<IStatusCodeHttpResult> PostAsync(MyDbContext db, string text, CancellationToken cancellationToken)
	{
		var entity = new MyRow
		{
			Text = text,
		};

		_ = await db.AddAsync(entity, cancellationToken);
		_ = await db.SaveChangesAsync(cancellationToken);

		return TypedResults.Created($"{entity.MyRowId}", entity);
	}

	private static async Task<IStatusCodeHttpResult> PutAsync(MyDbContext db, int id, string text, CancellationToken cancellationToken)
	{
		MyRow entity = await db.MyTable.FirstAsync(entity => entity.MyRowId == id, cancellationToken);

		entity.Text = text;
		_ = await db.SaveChangesAsync(cancellationToken);

		return TypedResults.Ok(entity);
	}

	private static async Task<IStatusCodeHttpResult> DeleteAsync(MyDbContext db, int id, CancellationToken cancellationToken)
	{
		MyRow entity = await db.MyTable.FirstAsync(entity => entity.MyRowId == id, cancellationToken);

		_ = db.Remove(entity);
		_ = await db.SaveChangesAsync(cancellationToken);

		return TypedResults.NoContent();
	}
}
