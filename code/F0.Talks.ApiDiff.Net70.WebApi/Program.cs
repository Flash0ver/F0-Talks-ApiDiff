using F0.Data;
using F0.Options;
using F0.Routes;
using F0.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddHostedService<Worker>();

builder.Services.AddSqlite();
builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	app.UseDeveloperExceptionPage();
	app.UseMigrationsEndPoint();
}

app.MapData();
app.MapGC();
app.MapHttp();
app.MapIO();
app.MapTar();
app.MapTypeConverter();

await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
{
	MyDbContext db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
	await db.Database.EnsureDeletedAsync(CancellationToken.None);
	await db.Database.EnsureCreatedAsync(CancellationToken.None);
	await db.ScaffoldAsync();
}

app.Run();
