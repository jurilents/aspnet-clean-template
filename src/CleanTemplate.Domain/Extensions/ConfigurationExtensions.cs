using Microsoft.Extensions.Configuration;

namespace CleanTemplate.Domain.Extensions;

public static class ConfigurationExtensions
{
	public static bool IsTesting(this IConfiguration configuration)
	{
		return configuration["TestingMode"] == "true";
	}
}