namespace F0.Data;

public sealed class MyDbContext : DbContext
{
	private readonly IFileInfo _databaseFile;

	public MyDbContext(DbContextOptions<MyDbContext> options, IWebHostEnvironment env)
		: base(options)
	{
		IFileProvider fileProvider = env.ContentRootFileProvider;

		_databaseFile = fileProvider.GetFileInfo("my.database.net70.db");
	}

	public DbSet<MyRow> MyTable { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		string? path = _databaseFile.PhysicalPath;
		Debug.Assert(path is not null);
		optionsBuilder.UseSqlite($"Data Source={path}");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<MyRow>().ToTable("MyTable");
	}
}

public class MyRow
{
	public int MyRowId { get; set; }

	public required string Text { get; set; }
}
