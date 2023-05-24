namespace F0.Options;

internal static class ServiceCollectionExtensions
{
	public static void Configure(this IServiceCollection services, ConfigurationManager configuration)
	{
		services.Configure<MyConfig>(configuration.GetSection(nameof(MyConfig)));
	}
}
