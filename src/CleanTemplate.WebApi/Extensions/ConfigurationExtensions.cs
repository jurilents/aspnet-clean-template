using CleanTemplate.WebApi.Configuration;

namespace CleanTemplate.WebApi.Extensions;

public static class ConfigurationExtensions
{
	public static SwaggerConfigurationSettings GetSwaggerSettings(this IConfiguration configuration)
	{
		var settings = new SwaggerConfigurationSettings();
		configuration.Bind("Swagger", settings);
		return settings;
	}
}