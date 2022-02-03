using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanTemplate.WebApi.Configuration.Extensions;

public static class MvcBuilderExtensions
{
	public static IMvcBuilder ConfigureJsonSerializerConventions(this IMvcBuilder builder)
	{
		return builder.AddJsonOptions(options =>
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)));
	}
}