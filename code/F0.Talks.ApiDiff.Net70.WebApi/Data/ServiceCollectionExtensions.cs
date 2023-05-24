namespace F0.Data;

internal static class ServiceCollectionExtensions
{
	public static void AddSqlite(this IServiceCollection services)
	{
		DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", SqliteFactory.Instance);
		services.AddSingleton(static services => DbProviderFactories.GetFactory("Microsoft.Data.Sqlite"));

		services.AddScoped(static services =>
		{
			var configuration = services.GetRequiredService<IConfiguration>();

			string? connectionString = configuration.GetConnectionString("MyDbContext");
			Debug.Assert(connectionString is not null);

			var factory = services.GetRequiredService<DbProviderFactory>();
			DbDataSource dataSource = factory.CreateDataSource(connectionString);
			return dataSource;
		});
	}
}
