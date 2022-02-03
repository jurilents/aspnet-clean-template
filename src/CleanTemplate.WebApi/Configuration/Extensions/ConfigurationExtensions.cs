namespace CleanTemplate.WebApi.Configuration.Extensions;

public static class ConfigurationExtensions
{
	public static SwaggerConfigurationSettings GetSwaggerSettings(this IConfiguration configuration)
	{
		var settings = new SwaggerConfigurationSettings();
		configuration.Bind("Swagger", settings);
		return settings;
	}
}